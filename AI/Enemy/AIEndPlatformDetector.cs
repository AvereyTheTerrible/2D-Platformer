using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.AI
{
    public class AIEndPlatformDetector : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D detectorCollider;

        [SerializeField]
        private LayerMask groundLayerMask;

        [SerializeField]
        private float groundRaycastLength = 1;

        [Range(0, 1)]
        [SerializeField]
        private float groundRaycastDelay = 0.1f;

        public bool PathBlocked { get; private set; }

        public event Action OnPathBlocked;

        [Header("Gizmos Parameters: ")]
        [SerializeField]
        private Color colliderColor = Color.magenta;
        [SerializeField]
        private Color groundRaycastColor = Color.blue;
        [SerializeField]
        private bool showGizmos = true;

        private void Start()
        {
            StartCoroutine(CheckGroundCoroutine());
        }

        private IEnumerator CheckGroundCoroutine()
        {
            yield return new WaitForSecondsRealtime(groundRaycastDelay);
            var hit = Physics2D.Raycast(detectorCollider.bounds.center,
                Vector2.down,
                groundRaycastLength,
                groundLayerMask);

            if (!hit.collider)
                OnPathBlocked?.Invoke();

            PathBlocked = hit.collider == null;
            StartCoroutine(CheckGroundCoroutine());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Climbing"))
                return;
            OnPathBlocked?.Invoke();
        }

        private void OnDrawGizmos()
        {
            if (showGizmos && detectorCollider)
            {
                Gizmos.color = groundRaycastColor;
                Gizmos.DrawRay(detectorCollider.bounds.center, Vector2.down * groundRaycastLength);
                Gizmos.color = colliderColor;
                Gizmos.DrawCube(detectorCollider.bounds.center, detectorCollider.bounds.size);
            }
        }
    }
}