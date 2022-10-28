using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementState
{
    protected bool jumpPressed = false;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.jump);
        agent.rigidBody.gravityScale = 2f;

        agent.rigidBody.AddForce(Vector2.up * agent.agentData.JumpVelocity, ForceMode2D.Impulse);

       
        jumpPressed = true;
        agent.rigidBody.gravityScale += Time.deltaTime * 10;
    }

    protected override void HandleJumpPressed()
    {
        jumpPressed = true;
    }

    protected override void HandleJumpReleased()
    {
        jumpPressed = false;
        agent.rigidBody.gravityScale -= Time.deltaTime * 10;
    }

    public override void StateUpdate()
    {
        CalculateVelocity();
        
        if (agent.rigidBody.velocity.y <= 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
        }

        else if (agent.climbDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Climb));
        }
    }

    public override void StateFixedUpdate()
    {
        AddMovementForce();
        ControlJumpHeight();
    }

    public void ControlJumpHeight()
    {
        if (!jumpPressed)
        {
            if (agent.currentState == agent.stateFactory.GetState(StateType.Attack))
            {
                agent.rigidBody.gravityScale = 3.5f;
            }
            agent.rigidBody.AddForce(Vector2.down * agent.rigidBody.velocity.y * (1 - agent.agentData.LowJumpMultiplier) * 5);
        }
    }
}
