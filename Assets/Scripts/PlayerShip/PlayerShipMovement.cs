using Unity.Mathematics;
using UnityEngine;

namespace MIG.PlayerShip
{
    internal sealed class PlayerShipMovement : MonoBehaviour
    {
        [SerializeField] private float _moveMaxSpeed;
        [SerializeField] private float _moveAcceleration;
        [SerializeField] private float _moveBrake;
        [SerializeField] private float _moveDrag;

        [SerializeField] private float _turnMaxSpeed;
        [SerializeField] private float _turnAcceleration;
        [SerializeField] private float _turnDrag;

        private float _moveAmount, _turnAmount, _currentMoveSpeed, _currentTurnSpeed;
        private Vector3 _turnDirection;

        public float CurrentSpeed => _currentMoveSpeed;
        public float CurrentTurnAngle => transform.rotation.eulerAngles.z;
        public Vector2 CurrentPosition => transform.position;

        public void ApplyMoveInput(float value)
        {
            _moveAmount = value;
        }

        public void ApplyTurnInput(float value)
        {
            if ((_turnAmount = math.abs(value)) > math.EPSILON)
            {
                _turnDirection = Vector3.forward * math.sign(value);
            }
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            ProcessMove(deltaTime);
            ProcessTurn(deltaTime);
        }

        private void ProcessMove(float deltaTime)
        {
            _currentMoveSpeed = _currentMoveSpeed +
                                (_moveAmount > 0 ? _moveAcceleration : _moveBrake) * _moveAmount -
                                _moveDrag;
            _currentMoveSpeed = math.clamp(_currentMoveSpeed, 0.0f, _moveMaxSpeed);
            var moveDelta = _currentMoveSpeed * deltaTime * transform.up;
            transform.position += moveDelta;
        }

        private void ProcessTurn(float deltaTime)
        {
            _currentTurnSpeed = _currentTurnSpeed + _turnAcceleration * _turnAmount - _turnDrag;
            _currentTurnSpeed = math.clamp(_currentTurnSpeed, 0.0f, _turnMaxSpeed);
            var turnDelta = _currentTurnSpeed * deltaTime * _turnDirection;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + turnDelta);
        }
    }
}