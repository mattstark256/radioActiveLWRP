using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAudio : MonoBehaviour
{
    [FMODUnity.EventRef]
    [SerializeField]
    private string radioMusic;
    FMOD.Studio.EventInstance radioEvent;
    FMOD.Studio.ParameterInstance placedParameter;
    
    void Awake()
    {
        radioEvent = FMODUnity.RuntimeManager.CreateInstance(radioMusic);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(radioEvent, transform, GetComponent<Rigidbody>());
        radioEvent.start();
        radioEvent.getParameter("Placed", out placedParameter);
    }

    
    // This is called when the radio is placed at the correct corruption location.
    public void ChangeMusic()
    {
        placedParameter.setValue(1.0f);
    }


    // This is called once the radio has finished destroying the corruption.
    public void RevertMusic()
    {
        placedParameter.setValue(0.0f);
    }
}
