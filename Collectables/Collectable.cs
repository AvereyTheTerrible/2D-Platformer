using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.Collectable
{
    public abstract class Collectable : MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        [SerializeField]
        BoxCollider2D collectableCollider;
        [SerializeField]
        protected Color gizmoColor = Color.magenta;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            collectableCollider = GetComponent<BoxCollider2D>();
        }

        public abstract void PickUp(Agent agent);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Hero"))
            {
                PickUp(collision.GetComponent<Agent>());
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;

            Gizmos.DrawWireCube(collectableCollider.bounds.center, collectableCollider.bounds.size);
        }
    }
}