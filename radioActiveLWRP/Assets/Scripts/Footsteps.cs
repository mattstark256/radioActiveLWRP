using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    CharacterController _controller;
    [FMODUnity.EventRef]
    [SerializeField]
    private string InputFootsteps;
    FMOD.Studio.EventInstance FootstepsEvent;
    FMOD.Studio.ParameterInstance CorruptedParameter;

    [FMODUnity.EventRef]
    [SerializeField]
    private string _jumpEvent;
    [FMODUnity.EventRef]
    [SerializeField]
    private string _landEvent;
    FMOD.Studio.EventInstance _landInstance;

    bool playerismoving;
    [SerializeField]
    private float walkingSpeed;
    private float WoodValue;
    private float MetalValue;
    private float GrassValue;
    private bool playerisgrounded;

    private bool _firstFrameLand = true;

    void Start()
    {
        FootstepsEvent = FMODUnity.RuntimeManager.CreateInstance(InputFootsteps);
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
        _controller = GetComponent<CharacterController>();
        _landInstance = FMODUnity.RuntimeManager.CreateInstance(_landEvent);
    }

    void Update()
    {
        playerisgrounded = _controller.isGrounded;   
        if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        {
            if (playerisgrounded == true)
            {
                playerismoving = true;
            }
            else if (playerisgrounded == false)
            {
                playerismoving = false;
                _firstFrameLand = true;
            }
        }
        else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
        {
            playerismoving = false;
        }
        if(playerisgrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FMODUnity.RuntimeManager.PlayOneShot(_jumpEvent);
            }
        }
        if(_firstFrameLand == true)
        {
            if (_controller.collisionFlags == CollisionFlags.Below)
            {
                _landInstance.start();
                Debug.Log("Play landing");
                _firstFrameLand = false;
            }
        }

    }

    void CallFootsteps()
    {
        if (playerismoving == true)
        {
            FootstepsEvent.start();
        }
        else if (playerismoving == false)
        {
            //Debug.Log ("player is moving = false");
        }
    }

    void OnDisable()
    {
        playerismoving = false;
    }

}
