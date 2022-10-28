using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.AI
{
    public class AIPatrollingEnemyBrain : AIEnemy
    {
        [SerializeField]
        protected GroundDetector agentGroundDetector;

        [SerializeField]
        protected AIBehavior AttackBehavior, PatrolBehavior;

        private void Awake()
        {
            if (!agentGroundDetector)
                agentGroundDetector = GetComponentInChildren<GroundDetector>();
        }

        private void Update()
        {
            if (agentGroundDetector.IsGrounded())
            {
                AttackBehavior.PerformAction(this);
                PatrolBehavior.PerformAction(this);
            }
        }
    }
}