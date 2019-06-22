using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corruption : MonoBehaviour
{
    [SerializeField]
    private string radioTag;
    public string GetRadioTag() { return radioTag; }

    private ParticleSystem[] particleSystems;

    private bool activated= false;
    public bool IsActivated() { return activated; }

    private bool isUnstoppable = false;


    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
    }


    public void ActivateCorruption()
    {
        activated = true;
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }


    public void DeactivateCorruption()
    {
        if (isUnstoppable) return;
        activated = false;
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
    }


    public void MakeUnstoppable()
    {
        isUnstoppable = true;
    }
}
