using JetBrains.Annotations;
using MIG.API;
using UnityEngine.InputSystem.UI;

namespace MIG.Player.Input
{
    [UsedImplicitly]
    public sealed class LocalPlayerFactory : ILocalPlayerFactory
    {
        private readonly IGlobalEventSystem _globalEventSystem;

        public LocalPlayerFactory(IGlobalEventSystem globalEventSystem)
        {
            _globalEventSystem = globalEventSystem;
        }

        public ILocalPlayer Create()
        {
            return new LocalPlayer(new GameInputHandler(_globalEventSystem.InputModule as InputSystemUIInputModule));
        }
    }
}