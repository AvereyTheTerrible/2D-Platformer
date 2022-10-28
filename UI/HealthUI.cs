using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private List<HeartElementUI> healthImages;
    [SerializeField]
    private Sprite fullHeart, emptyHeart;
    [SerializeField]
    private HeartElementUI heartPrefab;

    public void Initialize(int maxHealth)
    {
        healthImages = new List<HeartElementUI>();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxHealth; i++)
        {
            var life = Instantiate(heartPrefab);
            life.transform.SetParent(transform, false);
            healthImages.Add(life);
        }
    }

    public void SetHealth(int currentHealth)
    {
        for (int i = 0; i < healthImages.Count; i++)
        {
            if (i < currentHealth)
            {
                healthImages[i].SetSprite(fullHeart);
            }

            else
            {
                healthImages[i].SetSprite(emptyHeart);
            }
        }

    }
}
