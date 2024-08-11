using System;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Infrastructure.Services.Input
{
    public class KeyboardInputService : IInputService
    {
        private readonly PlayerInputActions _inputActions = new PlayerInputActions();

        public event Action OnInteractionButtonPressed;
        public event Action OnJumpButtonPressed;
        public Vector2 Axis => _inputActions.KeyboardAndMouse.Movement.ReadValue<Vector2>().normalized;

        public KeyboardInputService()
        {
            _inputActions.KeyboardAndMouse.Interaction.performed += OnInteractionPerformed;
            _inputActions.KeyboardAndMouse.Jump.performed += OnJumpPerformed;
            _inputActions.KeyboardAndMouse.Interaction.performed += OnInteractionPerformed;
        }

        public void Enable() => _inputActions.Enable();

        public void Disable() => _inputActions.Disable();

        private void OnInteractionPerformed(CallbackContext context) => 
            OnInteractionButtonPressed?.Invoke();

        private void OnJumpPerformed(CallbackContext context) => 
            OnJumpButtonPressed?.Invoke();
    }
}