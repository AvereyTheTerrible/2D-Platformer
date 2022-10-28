using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    [SerializeField]
    private float timeToWaitBeforeRespawn = 2;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.die);
        agent.animationManager.OnAnimationEnd.AddListener(WaitBeforeDieAction);

    }

    private void WaitBeforeDieAction()
    {
        agent.animationManager.OnAnimationEnd.RemoveListener(WaitBeforeDieAction);
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(timeToWaitBeforeRespawn);
        agent.OnAgentDie?.Invoke();
    }
    protected override void HandleAttack()
    {

    }

    protected override void HandleJumpPressed()
    {

    }

    public override void StateUpdate()
    {
        agent.rigidBody.velocity = new Vector2(0, agent.rigidBody.velocity.y);
    }

    public override void GetHit()
    {

    }

    protected override void HandleMovement(Vector2 input)
    {

    }

    protected override void ExitState()
    {
        StopAllCoroutines();
        agent.animationManager.ResetEvents();
    }
}
