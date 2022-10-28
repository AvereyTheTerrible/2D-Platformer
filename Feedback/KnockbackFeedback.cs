using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace SMS.Feedback
{
    public class KnockbackFeedback : MovementState, IHittable
    {
        [SerializeField]
        private Rigidbody2D rigidBody;
        [SerializeField]
        private float knockbackForce = 7;

        public void GetHit(GameObject opponent, int weaponDamage)
        {
            Vector2 direction = new Vector2(transform.position.x - opponent.transform.position.x, 0.1f * (transform.position.y - opponent.transform.position.y));
            rigidBody.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
        }
    }
}