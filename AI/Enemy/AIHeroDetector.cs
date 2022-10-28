using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.AI
{
    public class AIHeroDetector : MonoBehaviour
    {
        [field: SerializeField]
        public bool HeroDetected { get; set; }

        [SerializeField]
        private Transform detectorCenter;
        [SerializeField]
        private Vector2 detectorSize = Vector2.one;
        [SerializeField]
        private Vector2 detectorCenterOffset = Vector2.zero;

        [SerializeField]
        private float detectionDelay = 0.3f;

        [SerializeField]
        private LayerMask detectionLayerMask;

        private GameObject target;

        public GameObject Target
        {
            get => target;
            private set
            {
                target = value;
                HeroDetected = target != null;
            }
        }

        public Vector2 DirectionToTarget => new Vector2(target.transform.position.x - detectorCenter.position.x, 0f);

        [Header("Gizmo Parameters: ")]
        [SerializeField]
        private Color gizmoIdleColor = Color.green;
        [SerializeField]
        private Color gizmoDetectedColor = Color.red;
        [SerializeField]
        private bool showGizmos = true;

        private void Start()
        {
            StartCoroutine(DetectionCoroutine());
        }

        private IEnumerator DetectionCoroutine()
        {
            yield return new WaitForSecondsRealtime(detectionDelay);
            Collider2D collider = Physics2D.OverlapBox(
                (Vector2)detectorCenter.position + detectorCenterOffset,
                detectorSize,
                0,
                detectionLayerMask);

            if (collider)
            {
                Target = collider.gameObject;
            }

            else
            {
                Target = null;
            }
            StartCoroutine(DetectionCoroutine());
        }

        private void OnDrawGizmos()
        {
            if (showGizmos && detectorCenter)
            {
                Gizmos.color = gizmoIdleColor;
                if (HeroDetected)
                    Gizmos.color = gizmoDetectedColor;
                Gizmos.DrawCube(detectorCenter.position, detectorSize);
            }
        }
    }
}