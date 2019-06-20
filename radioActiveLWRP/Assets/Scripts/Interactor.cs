using UnityEngine;

public class Interactor : MonoBehaviour
{
    IInteractable interactable;

    public void OnInteractionFinished()
    {
        interactable = null;
    }

    void OnTriggerEnter(Collider other)
    {
        interactable = other.GetComponent<IInteractable>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Interact"))
            interactable?.Interact(this);
    }
}
