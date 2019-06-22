using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Quaternion respawnRotation;

    [FMODUnity.EventRef]
    [SerializeField]
    private string _respawnEvent;

    public void SetRespawnPoint(Vector3 _respawnPoint) { respawnPoint = _respawnPoint; }


    private void Awake()
    {
        respawnPoint = transform.position;
        respawnRotation = transform.rotation;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            Respawn();
        }
    }


    public void Respawn()
    {
        Debug.Log("respawning");
        FMODUnity.RuntimeManager.PlayOneShotAttached(_respawnEvent, this.gameObject);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // If object is a pickup or is carrying a pickup, drop it
        Pickup pickup = GetComponentInChildren<Pickup>();
        if (pickup != null && pickup.IsBeingCarried()) { pickup.DetachFromInteractor(); }


        // Setting the gameObject to inactive overrides character controllers and anything else that affects the position
        gameObject.SetActive(false);
        transform.position = respawnPoint;
        transform.rotation = respawnRotation;
        gameObject.SetActive(true);

    }
}
