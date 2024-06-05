using MIG.API;
using MIG.GameActors;
using UnityEngine;

namespace MIG.PlayerShip
{
    [RequireComponent(typeof(PlayerShipMovement))]
    public sealed class PlayerShipActor : MonoBehaviour,
        IPlayerShipActor,
        IPlayerPawn,
        IDestroyableActor
    {
        [SerializeField] [CheckObject] private PlayerShipMovement _movement;

        private IGameInputHandler _inputHandler;
        private IPlayerTransformDataCollector _transformDataCollector;
        private IAttackModule _primaryAttackModule, _secondaryAttackModule;

        public int Id { get; private set; }
        public Transform Transform => transform;

        public void SetId(int newId)
            => Id = newId;

        public void SetupDependencies(
            IPlayerTransformDataCollector transformDataCollector,
            IAttackModule primaryAttackModule,
            IAttackModule secondaryAttackModule
        )
        {
            _transformDataCollector = transformDataCollector;
            _primaryAttackModule = primaryAttackModule;
            _secondaryAttackModule = secondaryAttackModule;
        }

        public void SetupInput(IGameInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _inputHandler.OnMove += OnMove;
            _inputHandler.OnTurn += OnTurn;
            _inputHandler.OnPrimaryAttackStart += OnPrimaryAttackStart;
            _inputHandler.OnPrimaryAttackFinish += OnPrimaryAttackFinish;
            _inputHandler.OnSecondaryAttackStart += OnSecondaryAttackStart;
            _inputHandler.OnSecondaryAttackFinish += OnSecondaryAttackFinish;
        }

        public void ClearInput()
        {
            _inputHandler.OnMove -= OnMove;
            _inputHandler.OnTurn -= OnTurn;
            _inputHandler.OnPrimaryAttackStart -= OnPrimaryAttackStart;
            _inputHandler.OnPrimaryAttackFinish -= OnPrimaryAttackFinish;
            _inputHandler.OnSecondaryAttackStart -= OnSecondaryAttackStart;
            _inputHandler.OnSecondaryAttackFinish -= OnSecondaryAttackFinish;
            _inputHandler = null;
        }

        public void Destroy()
        {
            ClearInput();
            Destroy(gameObject);
        }

        private void Update()
            => UpdateTransformData();

        private void OnMove(float value)
            => _movement.ApplyMoveInput(value);

        private void OnTurn(float value)
            => _movement.ApplyTurnInput(value);

        private void OnPrimaryAttackStart()
            => _primaryAttackModule.StartAttack();

        private void OnPrimaryAttackFinish()
            => _primaryAttackModule.StopAttack();

        private void OnSecondaryAttackStart()
            => _secondaryAttackModule.StartAttack();

        private void OnSecondaryAttackFinish()
            => _secondaryAttackModule.StopAttack();

        private void UpdateTransformData()
        {
            if (_transformDataCollector is null)
            {
                return;
            }

            _transformDataCollector.UpdatePosition(_movement.CurrentPosition);
            _transformDataCollector.UpdateSpeed(_movement.CurrentSpeed);
            _transformDataCollector.UpdateTurnAngle(_movement.CurrentTurnAngle);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _movement = GetComponent<PlayerShipMovement>();
        }
#endif
    }
}