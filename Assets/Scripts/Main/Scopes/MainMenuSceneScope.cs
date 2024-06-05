using MIG.API;

namespace MIG.Main
{
    internal sealed class MainMenuSceneScope : AbstractSceneScope, ISceneEntryPoint
    {
        private ILogService _logService;

        public override void Init(IAppScope appScope)
        {
            _logService = appScope.LogService;
            SceneEntryPoint = this;
        }

        public void LaunchScene()
        {
            _logService.Info("You've launched main menu");
        }
    }
}