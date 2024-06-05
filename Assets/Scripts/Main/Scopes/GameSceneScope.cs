using MIG.API;
using MIG.Asteroids;
using MIG.Damage;
using MIG.Game;
using MIG.Game.States;
using MIG.GameActors;
using MIG.GameMode;
using MIG.PlayerAttacks;
using MIG.PlayerShip;
using MIG.Scores;
using MIG.ScreenWrapping;
using MIG.Spawners;
using MIG.StateMachine;
using MIG.UFO;
using UnityEngine;

namespace MIG.Main
{
    internal sealed class GameSceneScope : AbstractSceneScope
    {
        [SerializeField] [CheckObject] private ScreenWrapper _screenWrapper;
        [SerializeField] [CheckObject] private InnerOuterSpawner _spawner;

        [Header("Game Actors Dependencies")] [SerializeField] [CheckObject]
        private PlayerShipActorFactorySettings _playerShipActorFactorySettings;

        [SerializeField] [CheckObject] private AsteroidActorFactorySettings _bigAsteroidFactorySettings;
        [SerializeField] [CheckObject] private AsteroidActorFactorySettings _smallAsteroidFactorySettings;
        [SerializeField] [CheckObject] private UFOActorFactorySettings _ufoActorFactorySettings;

        [Space] [SerializeField] [CheckObject] private ProjectileFactorySettings _projectileFactorySettings;
        [SerializeField] [CheckObject] private ProjectileAttackModuleSettings _projectileAttackModuleSettings;

        [SerializeField] [CheckObject] private LaserAttackModuleSettings _laserAttackModuleSettings;
        [SerializeField] [CheckObject] private LaserViewFactorySettings _laserViewFactorySettings;

        [Header("Game Mode Dependencies")] [SerializeField] [CheckObject]
        private DamageScoreSystemSettings _damageScoreSystemSettings;

        [SerializeField] [CheckObject] private SmallAsteroidSpawnSystemSettings _smallAsteroidSpawnSystemSettings;
        [SerializeField] [CheckObject] private BigAsteroidSpawnSystemSettings _bigAsteroidSpawnSystemSettings;
        [SerializeField] [CheckObject] private UFOSpawnSystemSettings _ufoSpawnSystemSettings;

        private IScreenWrapService _screenWrapService;
        private IGameActorService _gameActorService;
        private IDamageService _damageService;
        private IGameModeService _gameModeService;
        private IScoreService _scoreService;

        public override void Init(IAppScope appScope)
        {
            _screenWrapService = new ScreenWrapService(
                _screenWrapper,
                new MainCameraScreenWrapDataProvider());
            ResolveServices(appScope);
            InitGameStateService(appScope);
        }

        private void InitGameStateService(IAppScope appScope)
        {
            var stateMachine = new StateMachine<IGameState>();
            var gameStateService = new GameStateService(stateMachine);

            stateMachine.AddState<IStartGameState>(new StartGameState(gameStateService,
                _screenWrapService,
                appScope.PlayerService,
                appScope.UIService,
                _gameActorService,
                _gameModeService,
                _scoreService));
            stateMachine.AddState<IPlayGameState>(new PlayGameState(gameStateService, _gameModeService));
            stateMachine.AddState<IFinishGameState>(new FinishGameState(gameStateService, appScope.UIService,
                _scoreService, appScope.PlayerService, appScope.LeaderboardService));

            SceneEntryPoint = gameStateService;
        }

        private void ResolveServices(IAppScope appScope)
        {
            var gameActorService = new GameActorService();
            var damageService = new DamageService(gameActorService);
            _scoreService = new ScoreService(appScope.ScoreChangePropagator);

            var survivalModeSystemFactories = new ISurvivalModeSystemFactory[]
            {
                new DamageScoreSystemFactory(damageService, _scoreService, _damageScoreSystemSettings),
                new SmallAsteroidSpawnSystemFactory(damageService, gameActorService, _smallAsteroidSpawnSystemSettings),
                new BigAsteroidSpawnSystemFactory(gameActorService, appScope.TimerService,
                    _bigAsteroidSpawnSystemSettings),
                new UFOSpawnSystemFactory(gameActorService, appScope.TimerService, _ufoSpawnSystemSettings)
            };

            var survivalModeFactory = new SurvivalModeFactory(damageService, survivalModeSystemFactories);
            var gameModeService = new GameModeService(survivalModeFactory);

            var projectileAttackModuleFactory =
                new ProjectileAttackModuleFactory(new ProjectileFactory(_projectileFactorySettings, damageService),
                    appScope.TimerService, _projectileAttackModuleSettings);
            var laserAttackModuleFactory = new LaserAttackModuleFactory(damageService,
                new LaserViewFactory(_laserViewFactorySettings), _laserAttackModuleSettings, appScope.TimerService,
                appScope.LaserAttackDataUpdater);

            var actorFactories = new IGameActorFactory[]
            {
                new PlayerShipActorFactory(
                    appScope.PlayerService,
                    appScope.PlayerTransformDataCollector,
                    _playerShipActorFactorySettings,
                    projectileAttackModuleFactory,
                    laserAttackModuleFactory),
                new BigAsteroidActorFactory(_bigAsteroidFactorySettings, _spawner, damageService),
                new SmallAsteroidActorFactory(_smallAsteroidFactorySettings, _spawner, damageService),
                new UFOActorFactory(_ufoActorFactorySettings, _spawner, damageService)
            };

            gameActorService.SetupFactories(actorFactories);

            _damageService = damageService;
            _gameActorService = gameActorService;
            _gameModeService = gameModeService;
        }
    }
}