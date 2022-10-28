using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SMS.Animations
{
    public class HoverAnimation : MonoBehaviour
    {
        [SerializeField]
        private float movementDistance = 0.5f, animationDuration = 1;
        [SerializeField]
        private Ease animationEase;

        private void Start()
        {
            transform
                .DOMoveY(transform.position.y + movementDistance, animationDuration)
                .SetEase(animationEase)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}