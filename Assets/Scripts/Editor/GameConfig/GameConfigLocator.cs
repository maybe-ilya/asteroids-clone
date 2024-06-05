using System;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;

namespace MIG.Editor
{
    public sealed class GameConfigLocator
    {
        private static readonly Dictionary<Type, GameConfig> _gameConfigs = new();

        public static T Get<T>() where T : GameConfig
            => GetInternal(typeof(T)) as T;

        public static GameConfig Get(Type type)
            => GetInternal(type);

        private static GameConfig GetInternal(Type type)
        {
            CheckIsTypeValid(type);

            if (!_gameConfigs.TryGetValue(type, out var result) || result == null)
            {
                _gameConfigs[type] = result = CreateOrLoadConfig(type);
            }

            return result;
        }

        private static void CheckIsTypeValid(Type type)
        {
            if (type.IsSubclassOf(typeof(GameConfig))) return;

            throw new ArgumentException($"Incorrect type {type.FullName}");
        }

        private static GameConfig CreateOrLoadConfig(Type type)
        {
            var savePath = GameConfigDataProvider.GetConfigSavePath(type);

            if (File.Exists(savePath))
            {
                return InternalEditorUtility.LoadSerializedFileAndForget(savePath)[0] as GameConfig;
            }
            else
            {
                var newConfig = ScriptableObject.CreateInstance(type) as GameConfig;
                newConfig.hideFlags = GameConfigDataProvider.GetConfigHideFlags(type);
                return newConfig;
            }
        }
    }
}