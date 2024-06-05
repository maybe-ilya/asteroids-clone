using System;
using JetBrains.Annotations;
using MIG.API;

namespace MIG.Main
{
    [UsedImplicitly]
    public sealed class ScoreChangeNotificationHandler :
        IScoreChangePropagator,
        IScoreChangeNotifier
    {
        public event Action<int> ScoreChanged;

        public void PropagateScoreChange(int newScore)
            => ScoreChanged?.Invoke(newScore);
    }
}