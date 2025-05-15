// using UnityEngine;
// using System;

// public class PlayerInteractV1 : MonoBehaviour 
// {
//     [SerializeField] private InputManager inputManager;
    

//     private void Start() {
//         if(inputManager == null) {
//             inputManager = InputManager.Instance;
//         }
        


//     }
//     private void Update() {
        
//         if(inputManager.PlayerInteract()) {
//             float interactRange = 2f;
//             Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
//             foreach (Collider collider in colliderArray) {
//                 if (collider.TryGetComponent(out NPCInteractable npcInteractable)) {
//                     npcInteractable.Interact(transform);
//                 }
//             }
//         }
         
//     }


//     public NPCInteractable GetInteractableObject() {
//         float interactRange = 2f;
//         Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
//         foreach (Collider collider in colliderArray) {
//             if (collider.TryGetComponent(out NPCInteractable npcInteractable)) {
//                 return npcInteractable;
//             }
//         }
//         return null;
//     }
// }
