using MIG.API;
using UnityEngine;

namespace MIG.Main
{
    public abstract class AbstractSceneScope : MonoBehaviour, ISceneScope
    {
        public ISceneEntryPoint SceneEntryPoint { get; protected set; }
        public abstract void Init(IAppScope appScope);
    }
}