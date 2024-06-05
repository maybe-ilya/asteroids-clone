using UnityEngine.EventSystems;

namespace MIG.API
{
    public interface IGlobalEventSystem : IService, IInitializableService
    {
        BaseInputModule InputModule { get; }
    }
}