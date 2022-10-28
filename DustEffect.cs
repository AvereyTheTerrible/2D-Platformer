using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : MonoBehaviour, IFeedback
{
    [SerializeField]
    private ParticleSystem dustEffect, jumpDustEffect;
    [SerializeField]
    private Transform feetPos;

    public void StartFeedback()
    {
        dustEffect.transform.localScale = feetPos.GetComponentInParent<Agent>().transform.localScale;
        Instantiate(dustEffect, feetPos.position, feetPos.rotation);
    }

    public void JumpFeedback()
    {
        Instantiate(jumpDustEffect, feetPos.position, Quaternion.identity);
    }
}
