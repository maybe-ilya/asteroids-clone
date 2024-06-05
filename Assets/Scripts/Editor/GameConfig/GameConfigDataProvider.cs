using System;
using System.Reflection;
using UnityEngine;

namespace MIG.Editor
{
    public static class GameConfigDataProvider
    {
        public static string GetConfigSavePath<T>() where T : GameConfig
            => GetConfigSavePathInternal(typeof(T));

        public static string GetConfigSavePath(Type type)
            => GetConfigSavePathInternal(type);

        public static string GetConfigSettingsPath<T>() where T : GameConfig
            => GetConfigSettingsPathInternal(typeof(T));

        public static string GetConfigSettingsPath(Type type)
            => GetConfigSettingsPathInternal(type);

        public static bool IsConfigPreference<T>() where T : GameConfig
            => IsConfigPreferenceInternal(typeof(T));

        public static bool IsConfigPreference(Type type)
            => IsConfigPreferenceInternal(type);

        public static HideFlags GetConfigHideFlags<T>() where T : GameConfig
            => GetConfigHideFlagsInternal(typeof(T));

        public static HideFlags GetConfigHideFlags(Type type)
            => GetConfigHideFlagsInternal(type);

        private static string GetConfigSavePathInternal(Type type)
        {
            CheckIsTypeValid(type);
            var attr = GetGameConfigAttribute(type);
            var path = attr.SavePath;

            if (string.IsNullOrEmpty(path))
            {
                throw new Exception($"SavePath of {type.FullName} is empty");
            }

            return path;
        }

        private static string GetConfigSettingsPathInternal(Type type)
        {
            CheckIsTypeValid(type);
            var attr = GetGameConfigAttribute(type);
            var path = attr.SettingsPath;

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new Exception($"SettingsPath of {type.FullName} is empty");
            }

            return path;
        }

        private static bool IsConfigPreferenceInternal(Type type)
        {
            CheckIsTypeValid(type);
            var attr = GetGameConfigAttribute(type);
            return attr.IsPreference;
        }

        private static HideFlags GetConfigHideFlagsInternal(Type type)
        {
            CheckIsTypeValid(type);
            var attr = GetGameConfigAttribute(type);
            return attr.HideFlags;
        }

        private static void CheckIsTypeValid(Type type)
        {
            if (type.IsSubclassOf(typeof(GameConfig))) return;

            throw new ArgumentException($"Incorrect type {type.FullName}");
        }

        private static GameConfigAttribute GetGameConfigAttribute(Type type)
        {
            var attr = type.GetCustomAttribute(typeof(GameConfigAttribute)) as GameConfigAttribute;
            if (attr == null)
            {
                throw new Exception($"Type {type.FullName} is missing {nameof(GameConfigAttribute)}");
            }

            return attr;
        }
    }
}