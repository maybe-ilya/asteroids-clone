using MIG.API;
using MIG.Utils;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MIG.Spawners
{
    public sealed class InnerOuterSpawner : MonoBehaviour, IInnerOuterSpawner
    {
        [SerializeField] private Vector2 _innerZoneHalfSize, _outerZoneHalfSize;
        [SerializeField] private Color _innerZoneColor, _outetZoneColor;

        public Vector3 GetRandomPoint()
        {
            var point = GetRandomPointInZone(_outerZoneHalfSize);
            point = ProjectPointToInnerZone(point, _innerZoneHalfSize);
            return transform.position + (Vector3)point;
        }

        public Vector3 GetRandomInnerPoint()
            => GetRandomWorldPointInZone(_innerZoneHalfSize);

        public Vector3 GetRandomOuterPoint()
            => GetRandomWorldPointInZone(_outerZoneHalfSize);

        private Vector2 GetRandomPointInZone(Vector2 zoneHalfSize)
        {
            return new Vector2(
                Random.Range(-zoneHalfSize.x, zoneHalfSize.x),
                Random.Range(-zoneHalfSize.y, zoneHalfSize.y)
            );
        }

        private Vector3 GetRandomWorldPointInZone(Vector2 zoneHalfSize)
            => transform.position + (Vector3)GetRandomPointInZone(zoneHalfSize);

        private Vector2 ProjectPointToInnerZone(Vector2 input, Vector2 zoneHalfSize)
        {
            var xAbs = math.abs(input.x);
            var yAbs = math.abs(input.y);

            if (xAbs > zoneHalfSize.x || yAbs > zoneHalfSize.y)
            {
                return input;
            }

            return zoneHalfSize.x - xAbs < zoneHalfSize.y - yAbs
                ? new Vector2(zoneHalfSize.x * math.sign(input.x), input.y)
                : new Vector2(input.x, zoneHalfSize.y * math.sign(input.y));
        }

        private void OnValidate()
        {
            _outerZoneHalfSize = new Vector2(
                math.max(_outerZoneHalfSize.x, 0),
                math.max(_outerZoneHalfSize.y, 0)
            );

            _innerZoneHalfSize = new Vector2(
                math.min(math.max(_innerZoneHalfSize.x, 0), _outerZoneHalfSize.x),
                math.min(math.max(_innerZoneHalfSize.y, 0), _outerZoneHalfSize.y)
            );
        }

        private void OnDrawGizmos()
        {
            using var matrixScope = new GizmosMatrixScope(transform.localToWorldMatrix);

            using (var _ = new GizmosColorScope(_outetZoneColor))
            {
                Gizmos.DrawCube(Vector3.zero, _outerZoneHalfSize * 2);
            }

            using (var _ = new GizmosColorScope(_innerZoneColor))
            {
                Gizmos.DrawCube(Vector3.zero, _innerZoneHalfSize * 2);
            }
        }
    }
}