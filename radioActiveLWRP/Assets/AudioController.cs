using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string[] radioEvent;
    FMOD.Studio.EventInstance[] radioInstance;



    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < radioEvent.Length; i++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
