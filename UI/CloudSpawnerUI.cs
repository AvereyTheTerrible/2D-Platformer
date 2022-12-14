using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SMS.UI
{
    public class CloudSpawnerUI : MonoBehaviour
    {
        [SerializeField]
        private float width = 1000, length = 1000;
        [SerializeField]
        private Color gizmoColor = new Color(1, 0, 0, 0.2f);
        [SerializeField]
        private bool showGizmos = true;
        [SerializeField]
        private List<CloudUI> cloudPrefabs = new List<CloudUI>();
        [SerializeField]
        private float cloudSpeed = 50;
        [SerializeField]
        private float scaleModifier = 0.5f;
   
        public Canvas canvas;

        private void Start()
        {
            foreach (Transform item in transform)
            {
                item.GetComponent<CloudUI>().Initialize((width / 2) * canvas.scaleFactor + 50, SpawnClouds);
            }
        }

        private void SpawnClouds()
        {
            Vector3 position = GetRandomPosition();

            int cloudIndex = Random.Range(0, cloudPrefabs.Count);
            CloudUI cloud = cloudPrefabs[cloudIndex];

            float scale = cloud.GetCloudScale() + scaleModifier;

            GameObject cloudObject = Instantiate(cloud.gameObject);

            RectTransform rectTransform = cloudObject.GetComponent<RectTransform>();

            rectTransform.position = position;
            rectTransform.localScale = Vector3.one * scale * canvas.scaleFactor;

            CloudUI newCloud = cloudObject.GetComponent<CloudUI>();

            newCloud.speed = cloudSpeed;

            rectTransform.SetParent(transform);

            newCloud.Initialize(width / 2 * canvas.scaleFactor + 50, SpawnClouds);
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                transform.position.x - (width / 2) * canvas.scaleFactor,
                Random.Range(transform.position.y - length / 2 * canvas.scaleFactor, transform.position.y + length / 2 * canvas.scaleFactor),
                1);    
        }

        private void OnDrawGizmos()
        {
            if (showGizmos && canvas)
            {
                Gizmos.color = gizmoColor;
                Gizmos.DrawCube(transform.position, new Vector2(width, length) * canvas.scaleFactor);
            }
        }
    }
}