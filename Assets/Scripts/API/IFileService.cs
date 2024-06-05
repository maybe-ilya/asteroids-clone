namespace MIG.API
{
    public interface IFileService : IService
    {
        bool DoesFileExist(string filePath);
        string ReadText(string filePath);
        byte[] ReadBytes(string filePath);
        void WriteText(string filePath, string content);
        void WriteBytes(string filePath, byte[] content);
    }
}
