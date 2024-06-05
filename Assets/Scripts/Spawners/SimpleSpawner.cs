using MIG.API;
using MIG.Utils;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MIG.Spawners
{
    public sealed class SimpleSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField] private Vector2 _spawnerHalfSize;
        [SerializeField] private Color _gizmoColor;

        public Vector3 GetRandomPoint()
        {
            return transform.position + new Vector3(
                Random.Range(-_spawnerHalfSize.x, _spawnerHalfSize.x),
                Random.Range(-_spawnerHalfSize.y, _spawnerHalfSize.y),
                0
            );
        }

        private void OnValidate()
        {
            _spawnerHalfSize = new Vector2(
                math.max(_spawnerHalfSize.x, 0),
                math.max(_spawnerHalfSize.y, 0));
        }

        private void OnDrawGizmosSelected()
        {
            using var colorScope = new GizmosColorScope(_gizmoColor);
            using var matrixScope = new GizmosMatrixScope(transform.localToWorldMatrix);

            Gizmos.DrawWireCube(Vector3.zero, _spawnerHalfSize * 2);
        }
    }
}