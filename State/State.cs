using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    protected Agent agent;

    public UnityEvent OnEnter, OnExit;

    public void InitializeState(Agent agent)
    {
        this.agent = agent;

    }

    public void Enter()
    {
        this.agent.agentInput.OnAttack += HandleAttack;
        this.agent.agentInput.OnJumpPressed += HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased += HandleJumpReleased;
        this.agent.agentInput.OnMovement += HandleMovement;

        OnEnter?.Invoke();
        EnterState();
    }

    protected virtual void EnterState()
    {
    }

    protected virtual void HandleMovement(Vector2 input)
    {
    }

    protected virtual void HandleJumpReleased()
    {

    }

    protected virtual void HandleJumpPressed()
    {
        TestJumpTransition();
    }

    private void TestJumpTransition()
    {
       agent.TransitionToState(agent.stateFactory.GetState(StateType.JumpBuffer));
    }

    protected virtual void HandleAttack()
    {
        TestAttackTransition();
    }

    public virtual void GetHit()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Hit));
    }

    public virtual void StateUpdate()
    {
        TestFallTransition();
    }
    protected bool TestFallTransition()
    {
        if (!agent.groundDetector.IsGrounded())
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.CoyoteTime));
            return true;
        }

        return false;

    }

    public virtual void StateFixedUpdate()
    {

    }

    public void Exit()
    {
        this.agent.agentInput.OnAttack -= HandleAttack;
        this.agent.agentInput.OnJumpPressed -= HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased -= HandleJumpReleased;
        this.agent.agentInput.OnMovement -= HandleMovement;

        OnExit?.Invoke();
        ExitState();
    }
    protected virtual void ExitState()
    {
    }

    public virtual void Die()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Die));
    }

    protected virtual void TestAttackTransition()
    {
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Attack));
    }
}
