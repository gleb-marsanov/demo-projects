using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class KeyboardInputService : IInputService
    {
        private readonly PlayerInputActions _inputActions = new PlayerInputActions();

        public event Action OnAttackButtonPressed;
        public event Action OnJumpButtonPressed;
        public event Action OnDismountButtonPressed;
        public event Action OnLevelTransferButtonPressed;
        public Vector2 Axis => _inputActions.KeyboardAndMouse.Movement.ReadValue<Vector2>();

        public KeyboardInputService()
        {
            _inputActions.KeyboardAndMouse.Attack.performed += _ => OnAttackButtonPressed?.Invoke();
            _inputActions.KeyboardAndMouse.Jump.performed += _ => OnJumpButtonPressed?.Invoke();
            _inputActions.KeyboardAndMouse.Dismount.performed += _ => OnDismountButtonPressed?.Invoke();
            _inputActions.KeyboardAndMouse.LevelTransfer.performed += _ => OnLevelTransferButtonPressed?.Invoke();
        }

        public void Enable() => _inputActions.Enable();

        public void Disable() => _inputActions.Disable();
    }
}