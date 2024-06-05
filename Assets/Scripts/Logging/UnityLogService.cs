using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using MIG.API;
using UDebug = UnityEngine.Debug;

namespace MIG.Logging
{
    [UsedImplicitly]
    public sealed class UnityLogService : ILogService
    {
        private readonly string LOG_CHANNEL_FORMAT = "{0}:{1}";

        public void Info(string message)
            => UDebug.Log(message);

        public void Info(LogChannel channel, string message)
            => UDebug.Log(CombineWithLogChannel(channel, message));

        public void Warning(string message)
            => UDebug.LogWarning(message);

        public void Warning(LogChannel channel, string message)
            => UDebug.LogWarning(CombineWithLogChannel(channel, message));

        public void Error(string message)
            => UDebug.LogError(message);

        public void Error(LogChannel channel, string message)
            => UDebug.LogError(CombineWithLogChannel(channel, message));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string CombineWithLogChannel(LogChannel channel, string message)
            => string.Format(LOG_CHANNEL_FORMAT, channel, message);
    }
}