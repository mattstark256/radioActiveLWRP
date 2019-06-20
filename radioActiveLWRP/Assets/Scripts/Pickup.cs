using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    Rigidbody rb;
    Vector3 relativePositionBeforePickup;
    [FMODUnity.EventRef]
    [SerializeField]
    private string _grabEvent;
    [FMODUnity.EventRef]
    [SerializeField]
    private string _placeEvent;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        relativePositionBeforePickup = transform.position - interactor.transform.position;
        relativePositionBeforePickup = Vector3.ProjectOnPlane(relativePositionBeforePickup, Vector3.up);
        rb.isKinematic = true;
        transform.parent = interactor.transform;
        transform.SetPositionAndRotation(interactor.transform.position, interactor.transform.rotation);
        FMODUnity.RuntimeManager.PlayOneShot(_grabEvent);
    }

    void DetachFromInteractor(Interactor interactor)
    {
        rb.isKinematic = false;
        transform.parent = null;
        Vector3 returnPosition = interactor.transform.TransformPoint(relativePositionBeforePickup);
        transform.SetPositionAndRotation(returnPosition, Quaternion.identity);
        interactor.OnInteractionFinished();
    }

    private void OnCollisionEnter(Collision other)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(_placeEvent, this.gameObject);
    }
}
