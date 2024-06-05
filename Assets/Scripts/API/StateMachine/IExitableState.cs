namespace MIG.API
{
    public interface IExitableState : IState
    {
        void Exit();
    }
}