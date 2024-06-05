using System.IO;
using UnityEditorInternal;

namespace MIG.Editor
{
    public static class GameConfigSaver
    {
        public static void Save(GameConfig config)
        {
            var type = config.GetType();
            var savePath = GameConfigDataProvider.GetConfigSavePath(type);

            var folder = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            InternalEditorUtility.SaveToSerializedFileAndForget(new[] { config }, savePath, true);
        }
    }
}