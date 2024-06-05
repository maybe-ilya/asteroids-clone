using System.Linq;
using UnityEditor;
using UObject = UnityEngine.Object;

namespace MIG.Editor
{
    public static class AssetFinder
    {
        private const string TYPE_SEARCH_FORMAT = "t:{0}";
        private const string TYPE_SEARCH_WITH_NAME_FORMAT = "t:{0} {1}";

        public static T FindSingle<T>() where T : UObject
            => FindSingleInternal<T>(GetTypeFilter<T>());

        public static T FindSingle<T>(string name) where T : UObject
            => FindSingleInternal<T>(GetTypeFilterWithName<T>(name));

        public static T[] FindMultiple<T>() where T : UObject
            => FindMultipleInternal<T>(GetTypeFilter<T>());

        public static T[] FindMultiple<T>(string name) where T : UObject
            => FindMultipleInternal<T>(GetTypeFilterWithName<T>(name));

        private static string GetTypeFilter<T>() where T : UObject
            => string.Format(TYPE_SEARCH_FORMAT, typeof(T).Name);

        private static string GetTypeFilterWithName<T>(string name) where T : UObject
            => string.Format(TYPE_SEARCH_WITH_NAME_FORMAT, typeof(T).Name, name);

        private static T FindSingleInternal<T>(string filter) where T : UObject
        {
            var assetGuid = AssetDatabase.FindAssets(filter).FirstOrDefault();

            return !string.IsNullOrWhiteSpace(assetGuid)
                ? AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assetGuid))
                : null;
        }

        private static T[] FindMultipleInternal<T>(string filter) where T : UObject
            => AssetDatabase.FindAssets(filter)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToArray();
    }
}