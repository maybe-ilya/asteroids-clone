namespace MIG.API
{
    public interface ILogService : IService
    {
        void Info(string message);
        void Info(LogChannel channel, string message);
        void Warning(string message);
        void Warning(LogChannel channel, string message);
        void Error(string message);
        void Error(LogChannel channel, string message);
    }
}