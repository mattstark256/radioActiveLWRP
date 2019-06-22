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

    [FMODUnity.EventRef]
    [SerializeField]
    private string voSound1;
    [FMODUnity.EventRef]
    [SerializeField]
    private string voSound2;
    [SerializeField]
    private GameObject _speakerObject;
    [SerializeField]
    private float _waitTime;

    private bool _voPlayed = false;


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
        if(_speakerObject != null)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(voSound1, _speakerObject);
            StartCoroutine(WaitForVO());
        }


    }


    IEnumerator WaitForVO()
    {
        //Debug.Log("Called wait");
        yield return new WaitForSeconds(_waitTime);
        FMODUnity.RuntimeManager.PlayOneShotAttached(voSound2, _speakerObject);
    }
}
