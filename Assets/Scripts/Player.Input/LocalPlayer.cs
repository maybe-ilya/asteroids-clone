using JetBrains.Annotations;
using MIG.API;

namespace MIG.Player.Input
{
    [UsedImplicitly]
    public sealed class LocalPlayer : ILocalPlayer
    {
        private readonly GameInputHandler _inputHandler;

        public LocalPlayer(GameInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public IGameInputHandler InputHandler => _inputHandler;

        public void SetGameInputMode() => _inputHandler.SetGameInputMode();

        public void SetUIInputMode() => _inputHandler.SetUIInputMode();
    }
}