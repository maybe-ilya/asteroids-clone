using UnityEngine;

namespace MIG.UFO
{
    internal sealed class UFOMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Transform _followTarget;

        public void SetFollowTarget(Transform newFollowTarget)
        {
            _followTarget = newFollowTarget;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += _speed * Time.deltaTime * GetDirection();
        }

        private Vector3 GetDirection()
        {
            return _followTarget
                ? (_followTarget.position - transform.position).normalized
                : transform.up;
        }
    }
}