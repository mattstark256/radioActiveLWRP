using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            Respawnable respawnable = other.GetComponent<Respawnable>();
            if (respawnable != null)
            {
                respawnable.SetRespawnPoint(respawnPoint.position);
            }
        }
    }
}
