using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField]
    private string radioTag;

    private Corruption nearbyCorruption = null;
    private Pickup pickup;


    private void Awake()
    {
        pickup = GetComponent<Pickup>();
    }


    private void OnTriggerEnter(Collider other)
    {
        Corruption corruption = other.GetComponent<Corruption>();
        if (corruption!=null && nearbyCorruption==null)
        {
            nearbyCorruption = corruption;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Corruption corruption = other.GetComponent<Corruption>();
        if (corruption != null && corruption == nearbyCorruption)
        {
            nearbyCorruption = null;
        }
    }


    void Update()
    {
        if (!pickup.IsBeingCarried()&&
            nearbyCorruption!=null &&
            nearbyCorruption.IsActivated() &&
            nearbyCorruption.GetRadioTag()==radioTag)
        {
            StartCoroutine(DefeatCorruption());
        }
    }


    private IEnumerator DefeatCorruption()
    {
        nearbyCorruption.DeactivateCorruption();

        yield return new WaitForSeconds(8);
        GetComponent<Respawnable>().Respawn();
    }
}
