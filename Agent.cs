using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

public class Agent : MonoBehaviour
{
    public AgentDataSO agentData;
    public Rigidbody2D rigidBody;
    public IAgentInput agentInput;
    public AgentAnimation animationManager;
    private AgentRenderer agentRenderer;


    public CapsuleCollider2D bodyCollider;
    public GroundDetector groundDetector;
    public ClimbDetector climbDetector;

    private bool isGrounded;

    public State currentState = null, previousState = null;

    [SerializeField]
    private State IdleState;

    [HideInInspector]
    public AgentWeaponManager agentWeapon;

    [SerializeField]
    public StateFactory stateFactory;

    private HealthSystem healthSystem;

    [SerializeField]
    [Header("State Debugging")]
    private string stateName = "";

    [field: SerializeField]
    private UnityEvent OnRespawnRequired { get; set; }
    [field: SerializeField]
    public UnityEvent OnAgentDie { get; set; }

    public void AgentDie()
    {
        if (healthSystem.CurrentHealth > 0)
        {
            OnRespawnRequired?.Invoke();
        }
        else
        {
            currentState.Die();
        }
    }

    public void GetHit()
    {
        currentState.GetHit();
    }

    private void Awake()
    {
        agentInput = GetComponentInParent<IAgentInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        groundDetector = GetComponentInChildren<GroundDetector>();
        climbDetector = GetComponentInChildren<ClimbDetector>();
        agentWeapon = GetComponentInChildren<AgentWeaponManager>();
        stateFactory = GetComponentInChildren<StateFactory>();
        healthSystem = GetComponent<HealthSystem>();
        bodyCollider = GetComponentInChildren<CapsuleCollider2D>();
        stateFactory.InitializeStates(this);
    }

    private void Start()
    {
        agentInput.OnMovement += agentRenderer.FaceDirection;
        InitializeAgent();

        agentInput.OnWeaponChange += SwapWeapon;
    }

    private void SwapWeapon()
    {
        if (!agentWeapon)
            return;
        agentWeapon.SwapWeapon();
    }

    private void InitializeAgent()
    {
        TransitionToState(IdleState);
        healthSystem.Initialize(agentData.Health);
    }

    internal void TransitionToState(State targetState)
    {
        if (!targetState) return;
        if (currentState) currentState.Exit();

        previousState = currentState;
        currentState = targetState;
        currentState.Enter();

        DisplayState();
    }

    private void DisplayState()
    {
        if (!previousState || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString();
        }
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        isGrounded = groundDetector.IsGrounded();

        currentState.StateFixedUpdate();
    }
    public void PickUp(WeaponData weaponData)
    {
        if (!agentWeapon)
            return;
        agentWeapon.PickupWeapon(weaponData);
    }
}
