
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : MovementState
{
    protected override void EnterState()
    {
        Mathf.Clamp(agent.rigidBody.gravityScale += Time.deltaTime * 2f, 0, 3);
        agent.animationManager.PlayAnimation(AnimationType.fall);
    }
    


    public override void StateUpdate()
    {
        movementData.currentVelocity = agent.rigidBody.velocity;
        movementData.currentVelocity.y += agent.agentData.GravityModifier * Physics2D.gravity.y * Time.deltaTime;
                                                                                                                                                                                                                                                                             
       
        agent.rigidBody.velocity = movementData.currentVelocity;


        CalculateVelocity();

        if (agent.groundDetector.IsGrounded() && Mathf.Abs(agent.rigidBody.velocity.x) <= Mathf.Epsilon)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }

        else if (agent.groundDetector.IsGrounded() && Mathf.Abs(agent.rigidBody.velocity.x) > Mathf.Epsilon)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Move));
        }

        else if (agent.climbDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Climb));
        }
    }
}
