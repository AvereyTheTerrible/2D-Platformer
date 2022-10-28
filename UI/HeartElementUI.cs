using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeartElementUI : MonoBehaviour
{
    private Image image;
    [field: SerializeField]
    public UnityEvent OnSpriteChange { get; set; }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        if (image.sprite != sprite)
        {
            OnSpriteChange?.Invoke();
            image.sprite = sprite;
        }
    }
}
