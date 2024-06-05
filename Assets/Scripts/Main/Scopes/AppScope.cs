using MIG.API;
using MIG.App;
using MIG.App.States;
using MIG.Files;
using MIG.LoadingScreen;
using MIG.Logging;
using MIG.Player;
using MIG.Player.Input;
using MIG.SceneLoading;
using MIG.Scores;
using MIG.StateMachine;
using MIG.Timers;
using MIG.UI;
using MIG.UI.Windows;
using UnityEngine;

namespace MIG.Main
{
    internal sealed class AppScope : MonoBehaviour, IAppScope
    {
        [SerializeField] [CheckObject] private MainMenuAppStateSettings _menuAppStateSettings;
        [SerializeField] [CheckObject] private PlayGameAppStateSettings _playGameAppStateSettings;
        [SerializeField] [CheckObject] private WindowHandlerSettings _windowHandlerSettings;
        [SerializeField] [CheckObject] private GlobalEventSystemSettings _globalEventSystemSettings;
        [SerializeField] [CheckObject] private SceneLoaderServiceSettings _sceneLoaderServiceSettings;
        [SerializeField] [CheckObject] private LeaderboardServiceSettings _leaderboardServiceSettings;
        [SerializeField] [CheckObject] private JsonLeaderboardHandlerSettings _leaderboardHandlerSettings;


        [Header("UI Dependencies")] [SerializeField] [CheckObject]
        private LoadingScreenFactorySettings _loadingScreenFactorySettings;

        [SerializeField] [CheckObject] private MainMenuWindowFactorySettings _mainMenuWindowSettings;
        [SerializeField] [CheckObject] private PlayerHUDFactorySettings _playerHUDFactorySettings;
        [SerializeField] [CheckObject] private GameOverWindowFactorySettings _gameOverWindowFactorySettings;

        public IAppEntryPoint AppEntryPoint { get; private set; }
        public IAppStateService AppStateService { get; private set; }
        public ISceneLoader SceneLoader { get; private set; }
        public ISceneLoadNotifier SceneLoadNotifier { get; private set; }
        public ILogService LogService { get; private set; }
        public IUIService UIService { get; private set; }
        public IGlobalEventSystem GlobalEventSystem { get; private set; }
        public IPlayerService PlayerService { get; private set; }
        public IPlayerTransformDataNotifier PlayerTransformDataNotifier { get; private set; }
        public IPlayerTransformDataCollector PlayerTransformDataCollector { get; private set; }
        public ITimerService TimerService { get; private set; }
        public ILoadingScreenService LoadingScreenService { get; private set; }
        public IFileService FileService { get; private set; }
        public ILeaderboardService LeaderboardService { get; private set; }
        public IScoreChangeNotifier ScoreChangeNotifier { get; private set; }
        public IScoreChangePropagator ScoreChangePropagator { get; private set; }
        public ILaserAttackDataNotifier LaserAttackDataNotifier { get; private set; }
        public ILaserAttackDataUpdater LaserAttackDataUpdater { get; private set; }

        public void Init()
        {
            ResolveServices();
            InitializeServices();
            DontDestroyOnLoad(gameObject);
        }

        private void ResolveServices()
        {
            LogService = new UnityLogService();
            FileService = new FileService();
            LeaderboardService = new LeaderboardService(_leaderboardServiceSettings,
                new JsonLeaderboardHandlerFactory(FileService, _leaderboardHandlerSettings));
            LoadingScreenService = new LoadingScreenService(new LoadingScreenFactory(_loadingScreenFactorySettings));
            var sceneLoaderService =
                new SceneLoaderService(LoadingScreenService, LogService, _sceneLoaderServiceSettings);
            SceneLoader = sceneLoaderService;
            SceneLoadNotifier = sceneLoaderService;
            GlobalEventSystem = new GlobalEventSystem(_globalEventSystemSettings);
            PlayerService = new PlayerService(new LocalPlayerFactory(GlobalEventSystem));

            var playerTransformDataService = new PlayerTransformDataService();
            PlayerTransformDataNotifier = playerTransformDataService;
            PlayerTransformDataCollector = playerTransformDataService;

            TimerService = new TimerService(new TimerFactory());

            var scoreNotificationHandler = new ScoreChangeNotificationHandler();
            ScoreChangeNotifier = scoreNotificationHandler;
            ScoreChangePropagator = scoreNotificationHandler;

            var laserAttackDataHandler = new LaserAttackDataHandler();
            LaserAttackDataNotifier = laserAttackDataHandler;
            LaserAttackDataUpdater = laserAttackDataHandler;

            var appStateMachine = new StateMachine<IAppState>();
            var appStateService = new AppStateService(appStateMachine);

            var windowControllers = new IWindowController[]
            {
                new MainMenuWindowController(appStateService),
                new PlayerHUDController(PlayerTransformDataNotifier, ScoreChangeNotifier, LaserAttackDataNotifier),
                new GameOverWindowController(appStateService)
            };

            var windowFactories = new IWindowFactory[]
            {
                new MainMenuWindowFactory(_mainMenuWindowSettings),
                new PlayerHUDFactory(_playerHUDFactorySettings),
                new GameOverWindowFactory(_gameOverWindowFactorySettings)
            };

            var windowHandler = new WindowHandler(SceneLoadNotifier, _windowHandlerSettings, windowControllers,
                windowFactories);

            UIService = new UIService(windowHandler);

            appStateMachine.AddState<IMainMenuAppState>(new MainMenuAppState(_menuAppStateSettings, SceneLoader,
                LogService, UIService, PlayerService));
            appStateMachine.AddState<IPlayGameAppState>(new PlayGameAppState(_playGameAppStateSettings, appStateService,
                SceneLoader, LogService));
            appStateMachine.AddState<IQuitAppState>(new QuitAppState(LogService));

            AppStateService = appStateService;
            AppEntryPoint = appStateService;
        }

        private void InitializeServices()
        {
            var initializableServices = new IInitializableService[]
            {
                TimerService,
                GlobalEventSystem,
                PlayerService,
                UIService,
                LoadingScreenService,
                LeaderboardService
            };

            foreach (var service in initializableServices)
            {
                service.Init();
            }
        }
    }
}