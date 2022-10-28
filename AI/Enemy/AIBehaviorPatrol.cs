using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.AI
{
    public class AIBehaviorPatrol : AIBehavior
    {
        [SerializeField]
        private AIEndPlatformDetector changeDirectionDetector;
        private Vector2 movementVector = Vector2.zero;

        private void Awake()
        {
            if (!changeDirectionDetector)
                changeDirectionDetector = GetComponentInChildren<AIEndPlatformDetector>();
        }

        private void Start()
        {
            changeDirectionDetector.OnPathBlocked += HandlePathBlocked;
            movementVector = new Vector2(Random.value > 0.5f ? 1 : -1, 0);
        }

        private void HandlePathBlocked()
        {
            movementVector *= new Vector2(-1, 0);
        }

        public override void PerformAction(AIEnemy enemyAI)
        {
            enemyAI.MovementVector = movementVector;
            enemyAI.CallOnMovement(movementVector);
        }
    }
}