using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.AI
{
    public abstract class AIEnemy : MonoBehaviour, IAgentInput
    {
        public Vector2 MovementVector { get; set; }

        public event Action OnAttack;
        public event Action OnJumpPressed;
        public event Action OnJumpReleased;
        public event Action OnWeaponChange;
        public event Action<Vector2> OnMovement;

        public void CallOnAttack()
        {
            OnAttack?.Invoke();
        }

        public void CallOnJumpPressed()
        {
            OnJumpPressed?.Invoke();
        }

        public void CallOnJumpReleased()
        {
            OnJumpReleased?.Invoke();
        }
        public void CallOnMovement(Vector2 input)
        {
            OnMovement?.Invoke(input);
        }

        public void CallAttack()
        {
            OnAttack?.Invoke();
        }
    }
}