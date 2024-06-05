using System;

namespace MIG.API
{
    public interface IScoreChangeNotifier : IService
    {
        event Action<int> ScoreChanged;
    }
}