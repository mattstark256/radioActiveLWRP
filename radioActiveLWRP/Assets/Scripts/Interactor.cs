using UnityEngine;
using System.Collections.Generic;


public class Interactor : MonoBehaviour
{
    private List<IInteractable> nearbyInteractables = new List<IInteractable>();
    IInteractable interactingInteractable;

    public void OnInteractionFinished()
    {
        interactingInteractable = null;
    }

    void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        { nearbyInteractables.Add(interactable); }
    }

    void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null &&
            nearbyInteractables.Contains(interactable))
        { nearbyInteractables.Remove(interactable); }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (interactingInteractable != null)
            {
                interactingInteractable.Interact(this);
            }
            else if (nearbyInteractables.Count>0)
            {
                nearbyInteractables[0].Interact(this);
                interactingInteractable = nearbyInteractables[0];
            }
        }
    }


}
