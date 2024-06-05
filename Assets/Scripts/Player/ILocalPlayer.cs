using MIG.API;

namespace MIG.Player
{
    public interface ILocalPlayer
    {
        void SetGameInputMode();
        void SetUIInputMode();
        IGameInputHandler InputHandler { get; }
    }
}