using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    float dropDistance = 2.0f;
    Rigidbody rb;

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
        rb.isKinematic = true;
        transform.parent = interactor.transform;
        transform.SetPositionAndRotation(interactor.transform.position, interactor.transform.rotation);
    }

    void DetachFromInteractor(Interactor interactor)
    {
        rb.isKinematic = false;
        transform.parent = null;
        transform.position = interactor.transform.parent.TransformPoint(Vector3.forward * dropDistance);
        transform.rotation = Quaternion.identity;
        interactor.OnInteractionFinished();
    }
}
