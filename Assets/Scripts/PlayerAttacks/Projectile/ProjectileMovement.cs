using UnityEngine;

namespace MIG.PlayerAttacks
{
    public class ProjectileMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private bool _isActive;

        public void Activate()
            => _isActive = true;

        public void Deactivate()
            => _isActive = false;

        private void OnEnable()
            => Deactivate();

        private void Update()
            => Move();

        private void Move()
        {
            if (!_isActive) return;
            transform.position += Time.deltaTime * _speed * transform.up;
        }
    }
}