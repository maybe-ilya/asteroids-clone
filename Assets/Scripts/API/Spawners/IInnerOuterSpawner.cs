using UnityEngine;

namespace MIG.API
{
    public interface IInnerOuterSpawner : ISpawner
    {
        Vector3 GetRandomInnerPoint();
        Vector3 GetRandomOuterPoint();
    }
}