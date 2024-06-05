using System;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;

namespace MIG.Editor
{
    public static class ProjectWindowUtility
    {
        private const BindingFlags DefaultFlags = BindingFlags.NonPublic | BindingFlags.Static;

        public static string SelectedFolder => typeof(ProjectWindowUtil)
            .GetMethod("GetActiveFolderPath", DefaultFlags)
            ?.Invoke(null, null) as string;

        private static object ProjectWindow => typeof(ProjectWindowUtil)
            .GetMethod("GetProjectBrowserIfExists", DefaultFlags)
            ?.Invoke(null, null);

        public static void SearchByType(params Type[] types)
        {
            var builder = new StringBuilder();
            builder.AppendJoin(' ', types.Select(type => $"t:{type.Name}"));
            SetSearch(ProjectWindow, builder.ToString());
        }

        private static void SetSearch(object windowObj, string filter) =>
            windowObj.GetType()
                .GetMethod("SetSearch", new[] { typeof(string) })
                ?.Invoke(windowObj, new object[] { filter });

        public static void Focus() => (ProjectWindow as EditorWindow)?.Focus();
    }
}