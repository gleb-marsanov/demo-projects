using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        event Action OnInteractionButtonPressed;
        event Action OnJumpButtonPressed;
        Vector2 Axis { get; }
        void Enable();
        void Disable();
    }

}