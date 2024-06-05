using System;
using System.Linq;
using UnityEditor;
using UEditor = UnityEditor.Editor;

namespace MIG.Editor
{
    public sealed class GameConfigSettingsProvider : SettingsProvider
    {
        private GameConfig _gameConfig;
        private UEditor _editor;

        private GameConfigSettingsProvider(
            GameConfig config,
            string path
        )
            : base(path, GetSettingsScope(config))
        {
            _gameConfig = config;
            _editor = UEditor.CreateEditor(_gameConfig);
            keywords = GetSearchKeywordsFromSerializedObject(_editor.serializedObject);
        }

        public override void OnGUI(string searchContext)
        {
            base.OnGUI(searchContext);
            _editor.OnInspectorGUI();
        }

        public override void OnDeactivate()
        {
            GameConfigSaver.Save(_gameConfig);
            base.OnDeactivate();
        }

        private static SettingsScope GetSettingsScope(GameConfig config)
            => GameConfigDataProvider.IsConfigPreference(config.GetType())
                ? SettingsScope.User
                : SettingsScope.Project;

        [SettingsProviderGroup]
        private static SettingsProvider[] GetGameConfigsSettingsProviders()
        {
            var configTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(GameConfig)))
                .ToArray();

            var count = configTypes.Length;
            var result = new SettingsProvider[count];

            for (var i = 0; i < count; ++i)
            {
                var type = configTypes[i];
                var config = GameConfigLocator.Get(type);
                var path = GameConfigDataProvider.GetConfigSettingsPath(type);
                result[i] = new GameConfigSettingsProvider(config, path);
            }

            return result;
        }
    }
}