namespace MIG.API
{
    public interface IPlayerService : IService, IInitializableService
    {
        void SetPlayerGameInput();
        void SetPlayerUIInput();
        IGameInputHandler PlayerInputHandler { get; }
    }
}