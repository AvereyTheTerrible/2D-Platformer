using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteState : State
{
    float previousGravityScale;
    protected override void EnterState()
    {
        previousGravityScale = agent.rigidBody.gravityScale;
        agent.rigidBody.gravityScale = 0;
        StartCoroutine(TransitionToFallCoroutine());
    }

    public override void StateFixedUpdate()
    {
        
    }

    public override void StateUpdate()
    {
        
    }

    protected override void HandleJumpPressed()
    {
        StopAllCoroutines();
        agent.rigidBody.gravityScale = previousGravityScale;
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Jump));
    }

    private IEnumerator TransitionToFallCoroutine()
    {
        yield return new WaitForSecondsRealtime(agent.agentData.CoyoteTime);
        agent.rigidBody.gravityScale = previousGravityScale;
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));   
    }
}
