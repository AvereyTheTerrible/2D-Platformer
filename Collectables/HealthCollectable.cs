using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.Collectable
{
    public class HealthCollectable : Collectable
    {
        [SerializeField]
        private int healthAmountToAdd;

        public override void PickUp(Agent agent)
        {
            HealthSystem healthSystem = agent.GetComponent<HealthSystem>();
            if (!healthSystem)
                return;
            healthSystem.AddHealth(healthAmountToAdd);
        }
    }
}