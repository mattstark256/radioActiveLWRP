using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corruption : MonoBehaviour
{
    [SerializeField]
    private string radioTag;
    public string GetRadioTag() { return radioTag; }

    private ParticleSystem[] particleSystems;
    private Light glow;

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
        glow = GetComponentInChildren<Light>();

        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
        glow.intensity = 0;

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
        StartCoroutine(ChangeLightIntensity(50, 1));
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
        StartCoroutine( ChangeLightIntensity(0, 2));
        FMODUnity.RuntimeManager.PlayOneShotAttached(_clearEvent, this.gameObject);
        _volumeValue = 0.0f;
    }


    public void MakeUnstoppable()
    {
        isUnstoppable = true;
    }


    private IEnumerator ChangeLightIntensity(float finalIntensity, float duration)
    {
        float initialIntensity = glow.intensity;
        float f = 0;

        while (f < 1)
        {
            f += Time.deltaTime / duration;
            if (f > 1) { f = 1; }

            Debug.Log(f);
            glow.intensity = Mathf.SmoothStep(initialIntensity, finalIntensity, f);

            yield return null;
        }
    }
}
