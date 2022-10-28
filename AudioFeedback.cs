using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private AudioSource targetAudioSource;
    [Range(0, 1)]
    [SerializeField]
    private float volume = 1;

    public void PlayClip()
    {
        if (!clip)
        {
            return;
        }
        targetAudioSource.volume = this.volume;
        targetAudioSource.PlayOneShot(clip);
    }

    public void PlaySpecificClip(AudioClip clipToPlay = null)
    {
        if (!clipToPlay)
            clipToPlay = clip;
        if (!clipToPlay)
        {
            return;
        }
        targetAudioSource.volume = this.volume;
        targetAudioSource.PlayOneShot(clipToPlay);
    }
}
