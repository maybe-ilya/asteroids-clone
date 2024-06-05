using System.Collections.Generic;

namespace MIG.API
{
    public interface ILeaderboard
    {
        IReadOnlyList<LeaderboardEntry> Entries { get; }
    }
}