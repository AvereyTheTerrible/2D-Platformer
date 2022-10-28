using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SMS.AI
{
    public class AIMeleeAttackDetector : MonoBehaviour
    {
        public bool PlayerDetected { get; private set; }
        [SerializeField]
        private LayerMask targetLayer;

        [field: SerializeField]
        private UnityEvent<GameObject> OnPlayerDetected;

        [Range(.1f, 1f)]
        [SerializeField]
        private float radius;

        [Header("Gizmo Parameters: ")]
        [SerializeField]
        private Color gizmoColor = Color.green;
        public bool showGizmos = true;

        private void Update()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
            PlayerDetected = collider;

            if (PlayerDetected)
                OnPlayerDetected?.Invoke(collider.gameObject);
        }

        private void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = gizmoColor;
                Gizmos.DrawWireSphere(transform.position, radius);
            }
        }
    }
}