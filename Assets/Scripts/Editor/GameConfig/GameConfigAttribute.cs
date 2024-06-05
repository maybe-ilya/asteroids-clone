using System;
using UnityEngine;

namespace MIG.Editor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class GameConfigAttribute : Attribute
    {
        public readonly string SavePath;
        public readonly string SettingsPath;
        public readonly bool IsPreference;
        public readonly HideFlags HideFlags;

        public GameConfigAttribute(
            string savePath,
            string settingsPath,
            bool isPreference = false,
            HideFlags hideFlags = HideFlags.None)
        {
            SavePath = savePath;
            SettingsPath = settingsPath;
            IsPreference = isPreference;
            HideFlags = hideFlags;
        }
    }
}