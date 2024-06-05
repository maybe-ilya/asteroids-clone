using System;
using MIG.API;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace MIG.Player.Input
{
    public sealed class GameInputHandler :
        IGameInputHandler,
        PlayerControls.IPlayerActions
    {
        private PlayerControls _controls;
        private InputSystemUIInputModule _uiInputModule;

        public event Action<float> OnMove;
        public event Action<float> OnTurn;
        public event Action OnPrimaryAttackStart;
        public event Action OnPrimaryAttackFinish;
        public event Action OnSecondaryAttackStart;
        public event Action OnSecondaryAttackFinish;

        public GameInputHandler(InputSystemUIInputModule inputModule)
        {
            _uiInputModule = inputModule;
            _controls = new PlayerControls();
            SetupPlayerScheme();
            SetupUIScheme();
        }

        public void SetGameInputMode()
        {
            _controls.UI.Disable();
            _controls.Player.Enable();
        }

        public void SetUIInputMode()
        {
            _controls.Player.Disable();
            _controls.UI.Enable();
        }

        private void SetupPlayerScheme()
        {
            _controls.Player.SetCallbacks(this);
        }

        private void SetupUIScheme()
        {
            var uiActions = _controls.UI;
            _uiInputModule.UnassignActions();

            _uiInputModule.submit = uiActions.Submit.GetActionReference();
            _uiInputModule.cancel = uiActions.Cancel.GetActionReference();
            _uiInputModule.move = uiActions.Navigate.GetActionReference();
            _uiInputModule.leftClick = uiActions.Click.GetActionReference();
            _uiInputModule.point = uiActions.Point.GetActionReference();
            _uiInputModule.scrollWheel = uiActions.ScrollWheel.GetActionReference();
        }

        void PlayerControls.IPlayerActions.OnMove(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<float>());
        }

        void PlayerControls.IPlayerActions.OnTurn(InputAction.CallbackContext context)
        {
            OnTurn?.Invoke(context.ReadValue<float>());
        }

        void PlayerControls.IPlayerActions.OnPrimaryAttack(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    OnPrimaryAttackStart?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    OnPrimaryAttackFinish?.Invoke();
                    break;
            }
        }

        void PlayerControls.IPlayerActions.OnSecondaryAttack(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    OnSecondaryAttackStart?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    OnSecondaryAttackFinish?.Invoke();
                    break;
            }
        }
    }
}