using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected override void EnterState()
    {
        if (agent.groundDetector.IsGrounded())
        {
            agent.rigidBody.velocity = Vector2.zero;
        }

        agent.animationManager.PlayAnimation(AnimationType.idle);
    }

    public override void StateUpdate()
    {
        if (TestFallTransition())
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
        if (agent.climbDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Climb));
        }

        else if (Mathf.Abs(agent.agentInput.MovementVector.x) > Mathf.Epsilon)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Move));
        }
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if (agent.groundDetector.IsGrounded())
        {
            agent.rigidBody.velocity = Vector2.zero;
        }
    }
}
