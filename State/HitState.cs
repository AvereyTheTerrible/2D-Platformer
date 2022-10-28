using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    [SerializeField]
    private AgentRenderer agentRenderer;
    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.hit);
        if (agent.CompareTag("Hero"))
        {
            agent.agentInput.OnMovement -= agentRenderer.FaceDirection;
        }

        agent.animationManager.OnAnimationEnd.AddListener(TransitionToIdle);
    }

    protected override void HandleAttack()
    {

    }

    protected override void HandleJumpPressed()
    {
        
    }

    public override void StateUpdate()
    {
        
    }

    public override void GetHit()
    {
        
    }

    protected override void HandleMovement(Vector2 input)
    {
        
    }

    private void TransitionToIdle()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToIdle);
        if (agent.CompareTag("Hero"))
        {
            agent.agentInput.OnMovement += agentRenderer.FaceDirection;
        }

        agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
    }
}
