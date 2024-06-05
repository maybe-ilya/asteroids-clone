namespace MIG.API
{
    public interface IScoreChangePropagator : IService
    {
        void PropagateScoreChange(int newScore);
    }
}