using UnityEngine;

public class Respawner : MonoBehaviour
{
    GameObject[] respawnLocations;

    void Awake()
    {
        respawnLocations = GameObject.FindGameObjectsWithTag("Respawn");
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.GetComponent<CharacterController>() != null || other.GetComponent<Rigidbody>() != null)
        {
            TeleportToNearestRespawnLocation(other.gameObject);
        }
    }

    void TeleportToNearestRespawnLocation(GameObject objectToTeleport)
    {
        Vector3 closest = respawnLocations[0].transform.position;
        Vector3 objectPosition = objectToTeleport.transform.position;

        for (int i = 0; i < respawnLocations.Length; i++)
        {
            Vector3 current = respawnLocations[i].transform.position;
            float distanceToClosest = Vector3.Distance(closest, objectPosition);
            float distanceToCurrent = Vector3.Distance(current, objectPosition);

            if (distanceToCurrent < distanceToClosest)
                closest = current;
        }

        // Prevent the object from overriding the transform
        objectToTeleport.SetActive(false);
        objectToTeleport.transform.position = closest;
        objectToTeleport.SetActive(true);
    }
}
