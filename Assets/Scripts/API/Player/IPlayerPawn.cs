namespace MIG.API
{
    public interface IPlayerPawn
    {
        void SetupInput(IGameInputHandler inputHandler);
        void ClearInput();
    }
}