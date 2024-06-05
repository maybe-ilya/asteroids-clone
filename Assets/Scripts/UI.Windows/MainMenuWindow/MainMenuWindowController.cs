using JetBrains.Annotations;
using MIG.API;

namespace MIG.UI.Windows
{
    [UsedImplicitly]
    public sealed class MainMenuWindowController : AbstractWindowController<IMainMenuWindow>
    {
        private readonly IAppStateService _appStateService;

        public MainMenuWindowController(IAppStateService appStateService)
        {
            _appStateService = appStateService;
        }

        protected override void OnWindowSet()
        {
            Window.OnPlayGameClick += OnPlayGameButtonClick;
            Window.OnQuitGameClick += OnQuitGameButtonClick;
        }

        protected override void BeforeWindowClear()
        {
            Window.OnPlayGameClick -= OnPlayGameButtonClick;
            Window.OnQuitGameClick -= OnQuitGameButtonClick;
        }

        private void OnPlayGameButtonClick()
            => _appStateService.PlayGame();

        private void OnQuitGameButtonClick()
            => _appStateService.QuitApp();
    }
}