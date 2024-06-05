namespace MIG.API
{
    public struct GameOverWindowData : IWindowData
    {
        public readonly int Score;

        public GameOverWindowData(int score)
        {
            Score = score;
        }
    }
}