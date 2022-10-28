using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    [Range(0, 1)]
    private float movementSpeed = 0.3f;

    private void Awake()
    {
        mainCamera = mainCamera == null ? Camera.main : mainCamera;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(mainCamera.transform.position.x * movementSpeed, 0);
    }
}
