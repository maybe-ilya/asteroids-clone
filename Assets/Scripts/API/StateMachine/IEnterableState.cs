namespace MIG.API
{
    public interface IEnterableState : IState
    {
        void Enter();
    }

    public interface IEnterableState<in T> : IState
    {
        void Enter(T data);
    }
}