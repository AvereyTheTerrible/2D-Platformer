using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.UI
{
    public class ScaleElementUI : MonoBehaviour
    {
        private Sequence sequence;

        [SerializeField]
        private RectTransform element;
        [SerializeField]
        private float animationEndScale;
        [SerializeField]
        private float animationTime;
        private float baseScaleValue;
        private Vector3 baseScale, endScale;
        [SerializeField]
        private bool playConstantly = false;
        [SerializeField]
        private bool playAtStart = false;

        private void Start()
        {
            if(playAtStart)
            {
                PlayAnimation();
            }
        }

        public void PlayAnimation()
        {
            baseScale = element.localScale;
            endScale = Vector3.one * animationEndScale;

            if (playConstantly)
            {
                sequence = DOTween.Sequence()
                    .Append(element.DOScale(baseScale, animationTime))
                    .Append(element.DOScale(endScale, animationTime));
                sequence.SetLoops(-1, LoopType.Yoyo);
                sequence.Play();
            }

            else
            {
                sequence = DOTween.Sequence()
                    .Append(element.DOScale(baseScale, animationTime))
                    .Append(element.DOScale(endScale, animationTime))
                    .Append(element.DOScale(baseScale, animationTime));
                sequence.Play();
            }
        }

        private void OnDestroy()
        {
            if (sequence != null)
                sequence.Kill();
        }
    }
}