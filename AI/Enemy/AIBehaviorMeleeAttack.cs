using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.AI
{
    public class AIBehaviorMeleeAttack : AIBehavior
    {
        [SerializeField]
        private AIMeleeAttackDetector meleeRangeDetector;

        private bool isWaiting;

        [SerializeField]
        private float delay = 1;

        public override void PerformAction(AIEnemy enemyAI)
        {
            if (isWaiting)
                return;
            if (!meleeRangeDetector.PlayerDetected)
                return;
            enemyAI.CallAttack();
            isWaiting = true;
            StartCoroutine(AttackDelayCoroutine());
        }

        private IEnumerator AttackDelayCoroutine()
        {
            yield return new WaitForSecondsRealtime(delay);
            isWaiting = false;
        }
    }
}