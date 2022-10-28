using RespawnSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitDestroyer : MonoBehaviour
{
    [SerializeField]
    private LayerMask objectsToDestroyLayerMask;

    [SerializeField]
    private Vector2 size;

    [Header("Gizmo Parameters")]
    [SerializeField]
    private Color gizmoColor = Color.red;

    [SerializeField]
    private bool showGizmo = true;

    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, size, 0, objectsToDestroyLayerMask);
        if (collider)
        {
            Agent agent = collider.GetComponent<Agent>();
            if (!agent)
            {
                Destroy(collider.gameObject);
                return;
            }
            var healthSystem = agent.GetComponent<HealthSystem>();
            if (healthSystem)
            {
                healthSystem.GetHit(1);
                if (healthSystem.CurrentHealth == 0 && agent.CompareTag("Player"))
                {
                    agent.GetComponent<RespawnHelper>().RespawnPlayer();
                }
            }

            agent.AgentDie();
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(transform.position, size);
        }
    }
}
