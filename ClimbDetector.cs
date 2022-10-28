using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbDetector : MonoBehaviour
{
    [SerializeField]
    private LayerMask climbingLayerMask;
    private Collider2D bodyCollider;
    private bool canClimb;

    public bool CanClimb
    {
        get { return canClimb; }
        private set { canClimb = value; }
    }

    private void Awake()
    {
        if (!bodyCollider)
            bodyCollider = GetComponentInParent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        LayerMask collisionLayerMask = 1 << otherCollider.gameObject.layer;
        if ((collisionLayerMask & climbingLayerMask) != 0)
        {
            CanClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        LayerMask collisionLayerMask = 1 << otherCollider.gameObject.layer;
        if ((collisionLayerMask & climbingLayerMask) != 0)
        {
            CanClimb = false;
        }
    }
}
