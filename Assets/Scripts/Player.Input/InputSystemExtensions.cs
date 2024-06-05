using UnityEngine.InputSystem;

namespace MIG.Player.Input
{
    public static class InputSystemExtensions
    {
        public static InputActionReference GetActionReference(this InputAction inputAction) =>
            InputActionReference.Create(inputAction);
    }
}