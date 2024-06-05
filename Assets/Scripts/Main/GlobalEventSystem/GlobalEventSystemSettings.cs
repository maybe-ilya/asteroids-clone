using MIG.API;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MIG.Main
{
    [CreateAssetMenu(menuName = AssetConsts.CREATE_ASSET_ROOT_MENU + nameof(GlobalEventSystemSettings))]
    public sealed class GlobalEventSystemSettings : ScriptableObject
    {
        [SerializeField] [CheckObject] private BaseInputModule _baseInputModulePrefab;

        public BaseInputModule BaseInputModulePrefab => _baseInputModulePrefab;
    }
}