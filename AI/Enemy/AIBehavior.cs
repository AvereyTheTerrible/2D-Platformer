using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.AI
{
    public abstract class AIBehavior : MonoBehaviour
    {
        public abstract void PerformAction(AIEnemy enemyAI);
    }
}