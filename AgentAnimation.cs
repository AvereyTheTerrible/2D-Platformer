using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimation : MonoBehaviour
{
    private Animator animator;

    public UnityEvent OnAnimationAction;
    public UnityEvent OnAnimationEnd;
    [SerializeField]
    public UnityEvent OnAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.die:
                Play("HeroDie");
                break;
            case AnimationType.hit:
                Play("HeroHit");
                break;
            case AnimationType.idle:
                Play("HeroIdle");
                break;
            case AnimationType.attack:
                Play("HeroAttack");
                break;
            case AnimationType.run:
                Play("HeroRun");
                break;
            case AnimationType.jump:
                Play("HeroJump");
                break;
            case AnimationType.fall:
                Play("HeroFall");
                break;
            case AnimationType.climb:
                Play("HeroClimb");
                break;
            case AnimationType.land:
                Play("HeroLand");
                break;
            default:
                break;
        }
    }

    internal void StartAnimation(bool val)
    {
        animator.enabled = val;
    }
    public void Play(string name)
    {
        animator.Play(name, -1, 0f);
    }

    public void ResetEvents()
    {
        OnAnimationAction.RemoveAllListeners();
        OnAnimationEnd.RemoveAllListeners();
    }

    public void InvokeAnimationAction()
    {
        OnAnimationAction?.Invoke();
    }

    public void InvokeAttackAction()
    {
        OnAttack?.Invoke();
    }

    public void InvokeAnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }
}

public enum AnimationType
{
    die,
    hit,
    idle,
    attack,
    run,
    jump,
    fall,
    climb,
    land
}
