using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAudio : MonoBehaviour
{
    [FMODUnity.EventRef]
    [SerializeField]
    private string radioMusic;
    FMOD.Studio.EventInstance radioEvent;

    
    void Awake()
    {
        radioEvent = FMODUnity.RuntimeManager.CreateInstance(radioMusic);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(radioEvent, transform, GetComponent<Rigidbody>());
        radioEvent.start();
    }

    
    // This is called when the radio is placed at the correct corruption location.
    public void ChangeMusic()
    {

    }


    // This is called once the radio has finished destroying the corruption.
    public void RevertMusic()
    {

    }
}
