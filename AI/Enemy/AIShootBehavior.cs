using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.AI
{
    public class AIShootBehavior : AIBehavior
    {
        [SerializeField]
        private AIHeroDetector heroDetector;

        [SerializeField]
        private bool isWaiting;
        [SerializeField]
        private float attackDelay = 1f;

        public override void PerformAction(AIEnemy enemyAI)
        {
            if (isWaiting)
                return;
            if (heroDetector.HeroDetected)
            {
                isWaiting = true;
                enemyAI.CallOnMovement(heroDetector.DirectionToTarget);
                enemyAI.CallAttack();
                StartCoroutine(AttackDelayCoroutine());
            }
        }

        private IEnumerator AttackDelayCoroutine()
        {
            yield return new WaitForSecondsRealtime(attackDelay);
            isWaiting = false;
        }
    }
}