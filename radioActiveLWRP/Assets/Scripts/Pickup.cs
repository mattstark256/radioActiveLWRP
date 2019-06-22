using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    float dropDistance = 2.0f;
    Rigidbody rb;
    [FMODUnity.EventRef]
    [SerializeField]
    private string _grabEvent;
    [FMODUnity.EventRef]
    [SerializeField]
    private string _placeEvent;
    FMOD.Studio.EventInstance _placeInstance;

    private Interactor interactor;
    public bool IsBeingCarried() { return interactor != null; }


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
       // _placeInstance = FMODUnity.RuntimeManager.CreateInstance(_placeEvent);
    }

    public void Interact(Interactor interactor)
    {
        if(transform.parent == null)
            AttachToInteractor(interactor);
        else
            DetachFromInteractor();
    }

    void AttachToInteractor(Interactor _interactor)
    {
        interactor = _interactor;
        rb.isKinematic = true;
        transform.parent = interactor.transform;
        transform.SetPositionAndRotation(interactor.transform.position, interactor.transform.rotation);
        FMODUnity.RuntimeManager.PlayOneShot(_grabEvent);
    }

    public void DetachFromInteractor()
    {
        rb.isKinematic = false;
        transform.parent = null;
        //transform.position = interactor.transform.parent.TransformPoint(Vector3.forward * dropDistance);
        //transform.rotation = Quaternion.identity;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        interactor.OnInteractionFinished();
        interactor = null;
    }

    void OnCollisionEnter(Collision other)
    {
            FMODUnity.RuntimeManager.PlayOneShotAttached(_placeEvent, this.gameObject);
    }

}