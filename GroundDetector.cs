using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundDetector : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayerMask;
    [SerializeField]
    private Collider2D agentCollider;

    [Header("Gizmo Parameters: ")]
    [Range(-2f, 2f)]
    [SerializeField]
    private float boxCastYOffset = -0.1f;
    [Range(-2f, 2f)]
    [SerializeField]
    private float boxCastXOffset = -0.1f;
    [Range(0, 2)]
    [SerializeField]
    private float boxCastWidth = 1, boxCastHeight = 1;
    [SerializeField]
    private Color gizmoColorNotGrounded = Color.red, gizmoColorIsGrounded = Color.green;

    public UnityEvent OnConditionValidAction;

    private void Awake()
    {
        if (!agentCollider)
            agentCollider = GetComponent<CapsuleCollider2D>();
    }
    private void OnDrawGizmos()
    {
        if (!agentCollider) return;
        Gizmos.color = gizmoColorNotGrounded;
        if (IsGrounded())
            Gizmos.color = gizmoColorIsGrounded;

        Gizmos.DrawWireCube(agentCollider.bounds.center + new Vector3(boxCastXOffset, boxCastYOffset, 0),
            new Vector3(boxCastWidth, boxCastHeight));
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(agentCollider.bounds.center + new Vector3(boxCastXOffset, boxCastYOffset, 0),
            new Vector3(boxCastWidth, boxCastHeight), 0, Vector2.down, 0, groundLayerMask);

        return raycastHit.collider && raycastHit.collider.IsTouching(agentCollider);

    }



    public void TryPerformingAction()
    {
        if (IsGrounded())
        {
            OnConditionValidAction?.Invoke();
        }
    }
}
