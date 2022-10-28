using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBufferState : State
{
    protected override void EnterState()
    {
        StartCoroutine(LandCoroutine());
    }

    private IEnumerator LandCoroutine()
    {
        yield return new WaitForSecondsRealtime(agent.agentData.JumpBufferTime);
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
    }

    public override void StateUpdate()
    {
        PerformBufferedJump();
    }

    protected override void HandleJumpReleased()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
    }


    public void PerformBufferedJump()
    {
        if (agent.currentState == agent.stateFactory.GetState(StateType.JumpBuffer) && agent.groundDetector.IsGrounded())
        {
            StopAllCoroutines();
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Jump));
        }
        
    }

    protected override void ExitState()
    {
        
    }
}
