namespace MIG.API
{
    public interface IStateMachine<in TBaseState> where TBaseState : IState
    {
        void ChangeState<TInputState>() where TInputState : TBaseState, IEnterableState;

        void ChangeState<TInputState, TInputData>(TInputData data)
            where TInputState : TBaseState, IEnterableState<TInputData>;
    }
}