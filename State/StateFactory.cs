using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    [SerializeField]
    private State Idle, Move, Fall, Climb, Jump, Attack, Hit, Die, CoyoteTime, JumpBuffer;

    public State GetState(StateType stateType)
        => stateType switch
        {
            StateType.Idle => Idle,
            StateType.Move => Move,
            StateType.Fall => Fall,
            StateType.Climb => Climb,
            StateType.Jump => Jump,
            StateType.Attack => Attack,
            StateType.Hit => Hit,
            StateType.Die => Die,
            StateType.CoyoteTime => CoyoteTime,
            StateType.JumpBuffer => JumpBuffer,
            _  => throw new System.Exception("State not defined for " + stateType.ToString())
	    };

    public void InitializeStates(Agent agent)
    {
        State[] states = GetComponents<State>();
        foreach (var state in states)
        {
            state.InitializeState(agent);
        }
    }
}
public enum StateType
{
    Idle,
    Move,
    Fall,
    Climb,
    Jump,
    Attack,
    Hit,
    Die,
    CoyoteTime,
    JumpBuffer
}
