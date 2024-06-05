using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MIG.Editor
{
    public static class ForceResaveAssetsTool
    {
        private const string MENU_PATH = "Assets/Force Resave Assets %#&F";

        [MenuItem(MENU_PATH, isValidateFunction: true)]
        private static bool IsAnyAssetSelected() => Selection.assetGUIDs.Any();

        [MenuItem(MENU_PATH, isValidateFunction: false)]
        private static void ForceSaveAssets()
        {
            var paths = Selection.assetGUIDs.Select(AssetDatabase.GUIDToAssetPath).ToList();

            foreach (var path in paths)
            {
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
                EditorUtility.SetDirty(asset);
                AssetDatabase.SaveAssetIfDirty(asset);
            }

            paths.Clear();

            Resources.UnloadUnusedAssets();
            AssetDatabase.Refresh();
        }
    }
}