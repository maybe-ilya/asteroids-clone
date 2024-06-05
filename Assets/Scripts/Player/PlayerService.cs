using JetBrains.Annotations;
using MIG.API;

namespace MIG.Player
{
    [UsedImplicitly]
    public sealed class PlayerService : IPlayerService
    {
        private readonly ILocalPlayerFactory _localPlayerFactory;
        private ILocalPlayer _localPlayer;

        public PlayerService(ILocalPlayerFactory localPlayerFactory)
        {
            _localPlayerFactory = localPlayerFactory;
        }

        public void Init()
        {
            _localPlayer = _localPlayerFactory.Create();
        }

        public void SetPlayerGameInput()
        {
            _localPlayer.SetGameInputMode();
        }

        public void SetPlayerUIInput()
        {
            _localPlayer.SetUIInputMode();
        }

        public IGameInputHandler PlayerInputHandler
            => _localPlayer.InputHandler;
    }
}