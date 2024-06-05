using System;
using MIG.API;
using UnityEditorInternal;
using UnityEngine;

namespace MIG.Editor
{
    [GameConfig("ProjectSettings/AsmdefPostprocessorSettings.asset",
        "Project/Asmdef Postprocessor Settings")]
    public sealed class AsmdefPostprocessorSettings : GameConfig
    {
        [SerializeField] private bool _enabled = false;
        [SerializeField] private string _namespacePropertyName = "rootNamespace";
        [SerializeField] private string _asmdefNamePattern = @"^MIG\.(.+)\.asmdef$";

        [SerializeField] private string _referencesPropertyName = "references";

        [SerializeField] [CheckObject]
        private AssemblyDefinitionAsset[] _mandatoryReferences = Array.Empty<AssemblyDefinitionAsset>();

        [SerializeField] private bool _useReferenceGUID;
        [SerializeField] private string _referenceGUIDFormat = "GUID:{0}";

        public bool Enabled => _enabled;

        public string NamespacePropertyName => _namespacePropertyName;

        public string AsmdefNamePattern => _asmdefNamePattern;

        public string ReferencesPropertyName => _referencesPropertyName;

        public AssemblyDefinitionAsset[] MandatoryReferences => _mandatoryReferences;

        public bool UseReferenceGuid => _useReferenceGUID;

        public string ReferenceGuidFormat => _referenceGUIDFormat;
    }
}