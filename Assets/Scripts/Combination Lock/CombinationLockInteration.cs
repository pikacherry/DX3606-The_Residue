using UnityEngine;

public class CombinationLockInteraction : InteractableBase
{
    [SerializeField] private float raycastDistance = 5f;

    public override void OnInteract()
    {
        TryRotateDial();
    }

    private void TryRotateDial()
    {
        // Use the player's camera forward direction for raycasting
        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            Dial dial = hit.collider.GetComponent<Dial>();
            if (dial != null)
            {
                dial.Rotate();
            }
        }
    }
}