using System.Collections.Generic;
using MIG.API;

namespace MIG.Scores
{
    public interface ILeaderboardHandler
    {
        List<LeaderboardEntry> ReadHighScores();
        void SaveHighScores(List<LeaderboardEntry> highScores);
    }
}