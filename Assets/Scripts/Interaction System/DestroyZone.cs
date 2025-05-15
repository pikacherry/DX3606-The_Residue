using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    [SerializeField] private  InteractionInputData interactionInputData;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && interactionInputData.InteractClicked) {
            PickUpInteractable[] allObjects = FindObjectsOfType<PickUpInteractable>();

            foreach (PickUpInteractable obj in allObjects)
            {
                if (obj.isActiveAndEnabled)
                {
                    obj.TryDestroy();
                    break;
                }
            }
        }
    }
}
