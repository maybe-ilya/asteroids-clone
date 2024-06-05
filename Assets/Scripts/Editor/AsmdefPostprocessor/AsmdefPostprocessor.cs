using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using UnityEditor;

namespace MIG.Editor
{
    internal sealed class AsmdefPostprocessor : AssetPostprocessor
    {
        private static AsmdefPostprocessorSettings Settings =>
            GameConfigLocator.Get<AsmdefPostprocessorSettings>();

        private void OnPreprocessAsset()
        {
            if (Settings is null ||
                !Settings.Enabled ||
                !assetImporter.importSettingsMissing ||
                !CanProcessFile(assetPath))
            {
                return;
            }

            ProcessAsmdefFile(assetPath);
            assetImporter.SaveAndReimport();
        }

        private static bool CanProcessFile(string filePath)
        {
            return !string.IsNullOrEmpty(filePath) &&
                   Regex.IsMatch(Path.GetFileName(filePath), Settings.AsmdefNamePattern);
        }

        private static void ProcessAsmdefFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var jObject = JObject.Parse(File.ReadAllText(filePath));

            AddRootNamespaceToAsmdef(jObject, filePath);
            AddMandatoryDependencies(jObject);

            File.WriteAllText(filePath, jObject.ToString());
        }

        private static void AddRootNamespaceToAsmdef(JObject asmdefJObject, string filePath)
        {
            var namespacePropName = Settings.NamespacePropertyName;
            var namespaceValue = Path.GetFileNameWithoutExtension(filePath);

            if (!asmdefJObject.ContainsKey(namespacePropName))
            {
                asmdefJObject.Add(namespacePropName, namespaceValue);
            }
        }

        private static void AddMandatoryDependencies(JObject asmdefJObject)
        {
            var mandatoryReferences = Settings.MandatoryReferences;
            if (!mandatoryReferences.Any())
            {
                return;
            }

            var referencesPropName = Settings.ReferencesPropertyName;
            if (asmdefJObject.ContainsKey(referencesPropName))
            {
                return;
            }

            string[] referencesValues;

            if (Settings.UseReferenceGuid)
            {
                referencesValues = mandatoryReferences
                    .Select(AssetDatabase.GetAssetPath)
                    .Select(AssetDatabase.AssetPathToGUID)
                    .Select(guid => string.Format(Settings.ReferenceGuidFormat, guid))
                    .ToArray();
            }
            else
            {
                referencesValues = mandatoryReferences
                    .Select(AssetDatabase.GetAssetPath)
                    .Select(Path.GetFileNameWithoutExtension)
                    .ToArray();
            }

            asmdefJObject.Add(referencesPropName, JArray.FromObject(referencesValues));
        }
    }
}