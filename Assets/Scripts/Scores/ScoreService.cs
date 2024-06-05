using JetBrains.Annotations;
using MIG.API;

namespace MIG.Scores
{
    [UsedImplicitly]
    public sealed class ScoreService : IScoreService
    {
        private readonly IScoreChangePropagator _scoreChangePropagator;
        private int _currentScore;

        public ScoreService(IScoreChangePropagator scoreChangePropagator)
        {
            _scoreChangePropagator = scoreChangePropagator;
        }

        public int CurrentScore
        {
            get => _currentScore;
            private set
            {
                _currentScore = value;
                _scoreChangePropagator.PropagateScoreChange(value);
            }
        }

        public void AppendScore(int score)
        {
            CurrentScore += score;
        }

        public void Clear()
        {
            CurrentScore = 0;
        }
    }
}