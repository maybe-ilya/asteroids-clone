using MIG.API;
using UnityEngine.EventSystems;
using UObject = UnityEngine.Object;

namespace MIG.Main
{
    public sealed class GlobalEventSystem : IGlobalEventSystem
    {
        private readonly GlobalEventSystemSettings _settings;
        private BaseInputModule _inputModule;

        public GlobalEventSystem(GlobalEventSystemSettings settings)
        {
            _settings = settings;
        }

        public BaseInputModule InputModule => _inputModule;

        public void Init()
        {
            _inputModule = UObject.Instantiate(_settings.BaseInputModulePrefab);
            UObject.DontDestroyOnLoad(_inputModule.gameObject);
        }
    }
}