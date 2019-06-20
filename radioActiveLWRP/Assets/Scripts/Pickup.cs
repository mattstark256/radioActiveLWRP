using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    Rigidbody rb;
    Vector3 relativePositionBeforePickup;

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
    }

    void DetachFromInteractor(Interactor interactor)
    {
        rb.isKinematic = false;
        transform.parent = null;
        Vector3 returnPosition = interactor.transform.TransformPoint(relativePositionBeforePickup);
        transform.SetPositionAndRotation(returnPosition, Quaternion.identity);
        interactor.OnInteractionFinished();
    }
}
