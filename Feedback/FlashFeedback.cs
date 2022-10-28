using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.Feedback
{
    public class FlashFeedback : Feedback
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private float feedbackTime = 0.1f;

        public override void CompletePreviousFeedback()
        {
            StopAllCoroutines();
        }

        public override void CreateFeedback()
        {
            if (!spriteRenderer || !spriteRenderer.material.HasProperty("_MakeSolidColor"))
                return;

            ToggleMaterial(1);
            StartCoroutine(ResetColor());
        }

        private IEnumerator ResetColor()
        {
            yield return new WaitForSecondsRealtime(feedbackTime);
            ToggleMaterial(0);
        }

        private void ToggleMaterial(int val)
        {
            val = Mathf.Clamp(val, 0, 1);
            spriteRenderer.material.SetInt("_MakeSolidColor", val);
        }
    }
}