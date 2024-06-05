using System.Collections.Generic;

namespace MIG.API
{
    public interface ILeaderboardService : IService, IInitializableService
    {
        bool TryToSubmitNewHighScore(string name, int score);
    }
}