using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string[] radioEvent;
    FMOD.Studio.EventInstance[] radioInstance;
    [FMODUnity.EventRef]
    [SerializeField]
    private string _backgroundMusic;
    FMOD.Studio.EventInstance _backgroundEvent;

    [FMODUnity.EventRef]
    [SerializeField]
    private string _ambienceSound;
    FMOD.Studio.EventInstance ambienceEvent;


    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < radioEvent.Length; i++)
        {

        }
        _backgroundEvent = FMODUnity.RuntimeManager.CreateInstance(_backgroundMusic);
        _backgroundEvent.start();

        ambienceEvent = FMODUnity.RuntimeManager.CreateInstance(_ambienceSound);
        ambienceEvent.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
