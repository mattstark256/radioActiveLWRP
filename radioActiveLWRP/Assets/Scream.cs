using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scream : MonoBehaviour
{
    [SerializeField]
    private float screamHeight = 5f;
    [SerializeField]
    private float minimumScreamInterval = 5f;

    private float timeSinceLastScream = 0;

    [FMODUnity.EventRef]
    [SerializeField]
    private string _screamEvent;

    void Update()
    {
        timeSinceLastScream += Time.deltaTime;

        if (transform.position.y < screamHeight && timeSinceLastScream > minimumScreamInterval)
        {
            DoScream();
            timeSinceLastScream = 0;
        }
    }

    private void DoScream()
    {
        FMODUnity.RuntimeManager.PlayOneShot(_screamEvent);
    }
}
