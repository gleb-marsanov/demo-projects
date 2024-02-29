using UnityEngine;

namespace Gameplay.Enemy
{
    internal interface IMovementDirectionProvider
    {
        bool enabled { get; }
        Vector2 GetMovementDirection();
    }
}