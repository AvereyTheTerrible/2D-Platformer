using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : JumpState
{
    [SerializeField]
    protected LayerMask hittableLayerMask;

    protected Vector2 direction;
    [SerializeField]
    protected AgentRenderer agentRenderer;

    public UnityEvent<AudioClip> OnWeaponSound;
    private bool showGizmos = false;

    protected override void EnterState()
    {
        agent.animationManager.ResetEvents();
        agent.animationManager.PlayAnimation(AnimationType.attack);
        agent.animationManager.OnAnimationEnd.AddListener(TransitionToIdleState);
        agent.animationManager.OnAnimationAction.AddListener(PerformAttack);
        agent.agentInput.OnMovement -= agentRenderer.FaceDirection;
        agent.agentWeapon.ToggleWeaponVisibility(true);
        direction = agent.transform.right * (agent.transform.localScale.x > 0 ? 1 : -1);
        if (agent.groundDetector.IsGrounded())
        {
            agent.rigidBody.velocity = Vector2.zero;
        }
    }

    private void PerformAttack()
    {
        OnWeaponSound?.Invoke(agent.agentWeapon.GetCurrentWeapon().weaponUseSound);
        agent.animationManager.OnAnimationAction.RemoveListener(PerformAttack);
        agent.agentWeapon.GetCurrentWeapon().PerformAttack(agent, hittableLayerMask, direction);
    }
   
    private void TransitionToIdleState()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToIdleState);
        if (agent.groundDetector.IsGrounded())
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }

        else
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
        }
    }



    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || !showGizmos)
        {
            return;
        }

        Gizmos.color = Color.red;
        var pos = agent.agentWeapon.transform.position;
        agent.agentWeapon.GetCurrentWeapon().DrawWeaponGizmo(pos, direction);
    }

    protected override void HandleAttack()
    {
        // Prevent attacking
    }

    protected override void HandleJumpPressed()
    {
        // Prevent jump while attacking
    }

    protected override void HandleJumpReleased()
    {
        jumpPressed = false;
    }

    public override void StateUpdate()
    {
        // Prevent Update
        CalculateVelocity();

    }

    public override void StateFixedUpdate()
    {
        ControlJumpHeight();
        if (!agent.groundDetector.IsGrounded())
        {
            AddMovementForce();
        }

        // Prevent FixedUpdate
        if (agent.groundDetector.IsGrounded())
        {
            agent.rigidBody.velocity = Vector2.zero;
        }

    }
    protected override void ExitState()
    {
        agent.agentInput.OnMovement += agentRenderer.FaceDirection;
        agent.animationManager.ResetEvents();
        agent.agentWeapon.ToggleWeaponVisibility(false);
    }
}
