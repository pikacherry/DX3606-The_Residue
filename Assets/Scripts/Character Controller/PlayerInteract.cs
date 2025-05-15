using UnityEngine;
using System;

public class PlayerInteract : MonoBehaviour 
{
    [SerializeField] private float interactRange = 2f; // Interaction distance
    [SerializeField] private LayerMask interactableLayer; // Layer for interactable objects
    [SerializeField] private PlayerInteractUI interactUI; // UI script reference

    private Interactable currentInteractable;

    private void Update()
    {
        DetectInteractable();
        HandleInteraction();
    }

    private void DetectInteractable()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange, interactableLayer);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Interactable interactable))
            {
                currentInteractable = interactable;
                interactUI.ShowCrosshair(true); // Show crosshair when near an interactable
                return;
            }
        }

        // If no interactable object is found, hide crosshair
        currentInteractable = null;
        interactUI.ShowCrosshair(false);
    }

    private void HandleInteraction()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E)) // Press 'E' to interact
        {
            currentInteractable.Interact();
        }
    }
}
