using UnityEngine;

namespace MIG.Asteroids
{
    public sealed class AsteroidMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private float _speedModifier;

        public void SetDirection(Vector3 newDirection)
        {
            transform.up = newDirection;
        }

        public void SetSpeedModifier(float newSpeedModifier)
        {
            _speedModifier = newSpeedModifier;
        }

        private void Awake()
        {
            _speedModifier = 1.0f;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += _speed * _speedModifier * Time.deltaTime * transform.up;
        }
    }
}