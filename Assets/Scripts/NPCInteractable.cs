using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    private NPCLookAt npcLookAt;
   

    private void Awake() {
        npcLookAt = GetComponent<NPCLookAt>();
    }

    public void Interact(Transform interactorTransform) {
        //ChatBubble3D.Create(transform.transform, new Vector3(-.3f, 1.7f, 0f), "Hello there!");
        float playerHeight = .4f;
        npcLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);

        
    }
    
}
