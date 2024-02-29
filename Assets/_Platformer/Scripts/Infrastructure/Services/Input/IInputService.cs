using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        event Action OnAttackButtonPressed;
        event Action OnJumpButtonPressed;
        event Action OnDismountButtonPressed;
        event Action OnLevelTransferButtonPressed;
        Vector2 Axis { get; }
        void Enable();
        void Disable();
    }
}