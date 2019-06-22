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

    [FMODUnity.EventRef]
    [SerializeField]
    private string _corruptionMusic;
    FMOD.Studio.EventInstance _corruptionEvent;
    FMOD.Studio.ParameterInstance volumeParameter;

    [FMODUnity.EventRef]
    [SerializeField]
    private string _spawnEvent;

    [FMODUnity.EventRef]
    [SerializeField]
    private string _clearEvent;

    private float _volumeValue;


    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
        _corruptionEvent = FMODUnity.RuntimeManager.CreateInstance(_corruptionMusic);
        _corruptionEvent.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_corruptionEvent, this.gameObject.transform, GetComponent<Rigidbody>());
        _corruptionEvent.getParameter("Volume", out volumeParameter);
    }
    void Update()
    {
        volumeParameter.setValue(_volumeValue);
    }

    public void ActivateCorruption()
    {
        activated = true;
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
        FMODUnity.RuntimeManager.PlayOneShotAttached(_spawnEvent, this.gameObject);
        _volumeValue = 1.0f;
    }


    public void DeactivateCorruption()
    {
        if (isUnstoppable) return;
        activated = false;
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
        FMODUnity.RuntimeManager.PlayOneShotAttached(_clearEvent, this.gameObject);
        _volumeValue = 0.0f;
    }


    public void MakeUnstoppable()
    {
        isUnstoppable = true;
    }
}
