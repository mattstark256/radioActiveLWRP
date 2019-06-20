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
            DetachFromInteractor(interactor);
    }

    void AttachToInteractor(Interactor interactor)
    {
        rb.isKinematic = true;
        transform.parent = interactor.transform;
        transform.SetPositionAndRotation(interactor.transform.position, interactor.transform.rotation);
        FMODUnity.RuntimeManager.PlayOneShot(_grabEvent);
    }

    void DetachFromInteractor(Interactor interactor)
    {
        rb.isKinematic = false;
        transform.parent = null;
        transform.position = interactor.transform.parent.TransformPoint(Vector3.forward * dropDistance);
        transform.rotation = Quaternion.identity;
        interactor.OnInteractionFinished();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Corruptable")
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(_placeEvent, this.gameObject);
        }
    }
}