using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentData", menuName = "Agent/Data")]
public class AgentDataSO : ScriptableObject
{
    [field: Header("Combat Data")]
    [field: Space]
    [field: SerializeField]
    public int Health;

    [field: Header("Movement Data")]
    [field: Space]
    [field: SerializeField]
    public float MaxSpeed;
    [field: SerializeField]
    public float Acceleration;
    [field: SerializeField]
    public float Deceleration;
    [field: SerializeField]
    public float velocityPower;

    [field: Header("Jump Data")]
    [field: Space]
    [field: SerializeField]
    public float JumpVelocity;
    [field: SerializeField]
    public float LowJumpMultiplier;
    [field: SerializeField]
    public float GravityModifier;
    [field: SerializeField]
    public float CoyoteTime;
    [field: SerializeField]
    public float JumpBufferTime;

    [field: Header("Climb Data")]
    [field: Space]
    [field: SerializeField]
    public float ClimbSpeed;
}