using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClimbState : State
{
    private float previousGravityScale;
    [SerializeField]
    private MovementData movementData;
    [SerializeField]
    private AgentRenderer agentRenderer;


    public bool isClimbing = false;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.climb);
        agent.animationManager.StartAnimation(false);
        agent.agentInput.OnMovement -= agentRenderer.FaceDirection;
        previousGravityScale = agent.rigidBody.gravityScale;
        agent.rigidBody.gravityScale = 0;
        agent.rigidBody.velocity = Vector2.zero;
    }


    protected override void HandleJumpPressed()
    {
        
    }

    public override void StateUpdate()
    {
        agent.animationManager.StartAnimation(Mathf.Abs(agent.agentInput.MovementVector.y) > Mathf.Epsilon);
        if (Mathf.Abs(agent.agentInput.MovementVector.y) > Mathf.Epsilon)
        {
            isClimbing = true;
            agent.rigidBody.velocity = new Vector2(0, agent.agentInput.MovementVector.y * agent.agentData.ClimbSpeed);
        }

        else
        {
            agent.rigidBody.velocity = Vector2.zero;
        }

        if (agent.groundDetector.IsGrounded())
        {
            isClimbing = false;
            agent.rigidBody.velocity = new Vector2(agent.agentInput.MovementVector.x * agent.agentData.MaxSpeed, agent.agentInput.MovementVector.y * agent.agentData.ClimbSpeed);
        }

        if (!agent.climbDetector.CanClimb)
        {
            isClimbing = false;
            agent.rigidBody.AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
    }

    protected override void HandleAttack()
    {
        // Prevent attack
    }

    protected override void HandleMovement(Vector2 input)
    {
        
    }

    protected override void ExitState()
    {
        agent.agentInput.OnMovement += agentRenderer.FaceDirection;
        agent.rigidBody.gravityScale = previousGravityScale;
        agent.animationManager.StartAnimation(true);
    }
}
