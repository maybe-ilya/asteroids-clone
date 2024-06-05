namespace MIG.Timers
{
    public interface ITimer
    {
        int Id { get; }
        float RemainingTime { get; }
        void Update(float deltaTime);
        void Execute();
    }
}