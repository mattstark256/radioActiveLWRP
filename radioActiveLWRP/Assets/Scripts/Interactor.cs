using UnityEngine;

public class Interactor : MonoBehaviour
{
    bool isInteracting;
    IInteractable interactable;

    public void OnInteractionFinished()
    {
        interactable = null;
        isInteracting = false;
    }

    void OnTriggerEnter(Collider other)
    {
        IInteractable otherInteractable = other.GetComponent<IInteractable>();
        if(otherInteractable != null)
            interactable = otherInteractable;
    }

    void OnTriggerExit(Collider other)
    {
        if(!isInteracting && other.GetComponent<IInteractable>() != null)
            interactable = null;
    }

    void Update()
    {
        if(Input.GetButtonDown("Interact") && interactable != null)
        {
            isInteracting = true;
            interactable.Interact(this);
        }
    }
}
