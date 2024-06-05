using System.IO;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using MIG.API;

namespace MIG.Files
{
    [UsedImplicitly]
    public sealed class FileService : IFileService
    {
        public bool DoesFileExist(string filePath)
            => File.Exists(filePath);

        public string ReadText(string filePath)
        {
            CheckDoesFileExist(filePath);
            return File.ReadAllText(filePath);
        }

        public byte[] ReadBytes(string filePath)
        {
            CheckDoesFileExist(filePath);
            return File.ReadAllBytes(filePath);
        }

        public void WriteText(string filePath, string content)
        {
            CreateDirectoryForFileIfDoesntExist(filePath);
            File.WriteAllText(filePath, content);
        }

        public void WriteBytes(string filePath, byte[] content)
        {
            CreateDirectoryForFileIfDoesntExist(filePath);
            File.WriteAllBytes(filePath, content);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckDoesFileExist(string filePath)
        {
            if (!DoesFileExist(filePath))
            {
                throw new FileNotFoundException($"No file at path {filePath}");
            }
        }

        private bool DoesDirectoryExist(string directoryPath)
            => Directory.Exists(directoryPath);

        private void CreateDirectoryForFileIfDoesntExist(string filePath)
        {
            if (DoesDirectoryExist(filePath))
            {
                return;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }
    }
}