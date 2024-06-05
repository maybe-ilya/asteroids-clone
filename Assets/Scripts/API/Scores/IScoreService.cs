namespace MIG.API
{
    public interface IScoreService : IService
    {
        int CurrentScore { get; }
        void AppendScore(int score);
        void Clear();
    }
}