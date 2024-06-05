using MIG.API;

namespace MIG.Main
{
    public interface IAppScope
    {
        IAppEntryPoint AppEntryPoint { get; }
        IAppStateService AppStateService { get; }
        ISceneLoader SceneLoader { get; }
        ISceneLoadNotifier SceneLoadNotifier { get; }
        ILogService LogService { get; }
        IUIService UIService { get; }
        IGlobalEventSystem GlobalEventSystem { get; }
        IPlayerService PlayerService { get; }
        IPlayerTransformDataNotifier PlayerTransformDataNotifier { get; }
        IPlayerTransformDataCollector PlayerTransformDataCollector { get; }
        ITimerService TimerService { get; }
        ILoadingScreenService LoadingScreenService { get; }
        IFileService FileService { get; }
        ILeaderboardService LeaderboardService { get; }
        IScoreChangeNotifier ScoreChangeNotifier { get; }
        IScoreChangePropagator ScoreChangePropagator { get; }
        ILaserAttackDataNotifier LaserAttackDataNotifier { get; }
        ILaserAttackDataUpdater LaserAttackDataUpdater { get; }
    }
}