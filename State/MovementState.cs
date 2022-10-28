using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementState : State
{
    [SerializeField]
    protected MovementData movementData;

    float movement;

    public UnityEvent OnStep;


    private void Awake()
    {
        movementData = GetComponentInParent<MovementData>();
    }

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.run);
        if (agent.groundDetector.IsGrounded())
        {
            agent.animationManager.OnAnimationAction.AddListener(() => OnStep.Invoke());
        }
        agent.rigidBody.velocity = Vector2.zero;
    }

    public override void StateUpdate()
    {
        if (TestFallTransition()) return;
        CalculateVelocity();

        if (Mathf.Abs(agent.rigidBody.velocity.x) < 0.3f)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
    }

    public override void StateFixedUpdate()
    {

    }

    protected void CalculateVelocity()
    {
        if (agent.currentState == agent.stateFactory.GetState(StateType.Attack) && agent.groundDetector.IsGrounded())
            return;
        float targetSpeed = agent.agentInput.MovementVector.x * agent.agentData.MaxSpeed;
        float speedDifference = targetSpeed - agent.rigidBody.velocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.1f) ? agent.agentData.Acceleration : agent.agentData.Deceleration;
        if (agent.groundDetector.IsGrounded())
        {
            movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, agent.agentData.velocityPower) * Mathf.Sign(speedDifference);
        }

        else
        {
            movement = speedDifference;
        }
        
    }

    protected override void ExitState()
    {
        agent.animationManager.ResetEvents();
    }
}
