using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace SMS.Feedback
{
    public class TempImmortalityFeedback : MonoBehaviour, IHittable
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Collider2D[] colliders = new Collider2D[0];
        [SerializeField]
        private float immortalityTime = 1;

        [SerializeField]
        private float flashDelay = 0.1f;
        
        private void Awake()
        {
            if (colliders.Length == 0)
                colliders = GetComponents<Collider2D>();
        }

        public void GetHit(GameObject opponent, int weaponDamage)
        {
            if (!this.enabled)
                return;
            ToggleColliders(false);
            StartCoroutine(ResetColliders());
            StartCoroutine(Flash(1));
        }

        private IEnumerator Flash(int val)
        {
            spriteRenderer.material.SetInt("_MakeSolidColor", val);
            yield return new WaitForSecondsRealtime(flashDelay);
            StartCoroutine(Flash(val < 1 ? 1 : 0));
        }

        private IEnumerator ResetColliders()
        {
            yield return new WaitForSecondsRealtime(immortalityTime);
            StopAllCoroutines();
            ToggleColliders(true);
            spriteRenderer.material.SetInt("_MakeSolidColor", 0);
        }

        private void ToggleColliders(bool val)
        {
            foreach(var collider in colliders)
            {
                collider.enabled = val;
            }
        }
    }
}