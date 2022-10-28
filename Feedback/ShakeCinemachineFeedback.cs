using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

namespace SMS.Feedback
{
    public class ShakeCinemachineFeedback : Feedback
    {
        [SerializeField]
        private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField]
        [Range(0, 20)]
        private float amplitude, intensity;

        [SerializeField]
        [Range(0, 2)]
        private float duration = 0.1f;

        private CinemachineBasicMultiChannelPerlin noise;
        private void Start()
        {
            noise = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        public override void CompletePreviousFeedback()
        {
            StopAllCoroutines();
            noise.m_AmplitudeGain = 0;
        }

        public override void CreateFeedback()
        {
            noise.m_AmplitudeGain = amplitude;
            noise.m_FrequencyGain = intensity;
            StartCoroutine(ShakeCoroutine());
        }
        
        private IEnumerator ShakeCoroutine()
        {
            for (float i = duration; i > 0; i-=Time.deltaTime)
            {
                noise.m_AmplitudeGain = Mathf.Lerp(0, amplitude, i/duration);
                yield return null;
            }

            noise.m_AmplitudeGain = 0;
        }
    }
}