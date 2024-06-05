using JetBrains.Annotations;
using MIG.API;

namespace MIG.UI.Windows
{
    [UsedImplicitly]
    public sealed class GameOverWindowController : AbstractWindowController<IGameOverWindow>
    {
        private readonly IAppStateService _appStateService;

        public GameOverWindowController(IAppStateService appStateService)
        {
            _appStateService = appStateService;
        }

        protected override void OnWindowSet()
        {
            Window.OnRetryClicked += OnRetryButtonClick;
            Window.OnExitClicked += OnExitButtonClick;
        }

        protected override void BeforeWindowClear()
        {
            Window.OnRetryClicked -= OnRetryButtonClick;
            Window.OnExitClicked -= OnExitButtonClick;
        }

        private void OnRetryButtonClick()
            => _appStateService.PlayGame();

        private void OnExitButtonClick()
            => _appStateService.GoToMainMenu();
    }
}