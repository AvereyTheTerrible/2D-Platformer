using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.AI
{
    public class AIStaticEnemyBrain : AIEnemy
    {
        public AIBehavior AttackBehavior;

        private void Update()
        {
            AttackBehavior.PerformAction(this);
        }
    }
}