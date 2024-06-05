using System.Collections.Generic;
using JetBrains.Annotations;
using MIG.API;

namespace MIG.Scores
{
    [UsedImplicitly]
    public sealed class LeaderboardService : ILeaderboardService
    {
        private readonly LeaderboardServiceSettings _settings;
        private readonly ILeaderboardHandlerFactory _leaderboardHandlerFactory;
        private ILeaderboardHandler _leaderboardHandler;
        private List<LeaderboardEntry> _highScores;

        private int HighScoresLimit => _settings.HighScoresLimit;

        public LeaderboardService(
            LeaderboardServiceSettings settings,
            ILeaderboardHandlerFactory leaderboardHandlerFactory
        )
        {
            _settings = settings;
            _leaderboardHandlerFactory = leaderboardHandlerFactory;
        }

        public void Init()
        {
            _leaderboardHandler = _leaderboardHandlerFactory.Create();
            _highScores = _leaderboardHandler.ReadHighScores();
        }

        public bool TryToSubmitNewHighScore(string name, int score)
        {
            if (!IsScoreNewHighScore(score))
            {
                return false;
            }

            SubmitNewHighScore(new LeaderboardEntry(name, score));
            return true;
        }

        private void SubmitNewHighScore(LeaderboardEntry newEntry)
        {
            _highScores.Add(newEntry);
            _highScores.Sort((lhs, rhs) => rhs.CompareTo(lhs));
            if (_highScores.Count > HighScoresLimit)
            {
                _highScores.RemoveRange(HighScoresLimit, _highScores.Count - HighScoresLimit);
            }

            _leaderboardHandler.SaveHighScores(_highScores);
        }

        private bool IsScoreNewHighScore(int score)
            => score > 0 && (_highScores.Count == 0 || _highScores[^1].Score < score);
    }
}