using MIG.API;
using Unity.Mathematics;
using UnityEngine;

namespace MIG.ScreenWrapping
{
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class ScreenWrapper : MonoBehaviour, IScreenWrapper
    {
        [SerializeField] [CheckObject] private BoxCollider2D _boxCollider;

        private Vector2 Size => _boxCollider.size;
        private float Width => Size.x;
        private float Height => Size.y;

        public void Setup(float height, float width)
        {
            _boxCollider.size = new Vector2(width, height);
        }

        private void OnTriggerExit2D(Collider2D other)
            => Transfer(other.transform);

        private void Transfer(Transform transferTransform)
        {
            var resultPoint = transferTransform.position;

            var xDistance = Width * 0.5f - math.abs(resultPoint.x);
            var yDistance = Height * 0.5f - math.abs(resultPoint.y);

            if (xDistance < yDistance)
            {
                resultPoint.x *= -1;
            }
            else
            {
                resultPoint.y *= -1;
            }

            transferTransform.position = resultPoint;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }
#endif
    }
}