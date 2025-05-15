using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerV2 : MonoBehaviour 
{
    // private static InputManager _instance;

    // public static InputManager Instance {
    //     get {
    //         return _instance;
    //     }
    // }

    // public InteractionInputData interactionInputData;
    // // void Start() {
    // //     interactionInputData.Reset();
    // // }

    // // void Update() {
    // //     GetInteractInputData();
    // // }
  
    // private InputActions inputActions;


    // private void Awake(){
    //     if (_instance != null && _instance != this) {
    //         Destroy(this.gameObject);
    //         return;
    //     }
    //     _instance = this;
        
    //     inputActions = new InputActions();
    //     Cursor.visible = false;

    //     if (interactionInputData == null) {
    //         interactionInputData = ScriptableObject.CreateInstance<InteractionInputData>();
    //     }
    // }

    // private void OnEnable() {
    //     inputActions.Enable();
    //     inputActions.Player.Interact.performed += OnInteractPerformed;
    //     inputActions.Player.Interact.canceled += OnInteractCanceled;


    // }

    // private void OnDisable() {
    //     inputActions.Disable();
    //     inputActions.Player.Interact.performed -= OnInteractPerformed;
    //     inputActions.Player.Interact.canceled -= OnInteractCanceled;

    // }

    // private void OnInteractPerformed(InputAction.CallbackContext context) {
    //     interactionInputData.InteractClicked = true;
    //     Debug.Log("Clicked");
    // }
    // private void OnInteractCanceled(InputAction.CallbackContext context) {
    //     interactionInputData.InteractReleased = true;
    //     Debug.Log("Released");
    // }
    // // public void GetInteractInputData() {
    // //     interactionInputData.InteractClicked = inputActions.Player.Interact.WasPressedThisFrame();
    // //     if (interactionInputData.InteractClicked) {
    // //         Debug.Log("Clicked");
    // //     }
        
    // //     interactionInputData.InteractReleased = inputActions.Player.Interact.WasReleasedThisFrame();
    // //     if (interactionInputData.InteractReleased) {
    // //         Debug.Log("Released");
    // //     }

    // // }

    // public Vector2 GetPlayerMovement() {
    //     return inputActions.Player.Move.ReadValue<Vector2>();
    // }

    
    // public bool PlayerCrouched() {
    //     return inputActions.Player.Crouch.IsPressed();
    // }

    // public bool PlayerRunning() {
    //     return inputActions.Player.Sprint.IsPressed();
    // }

    

    // // public bool PlayerInteract() {
    // //     return inputActions.Player.Interact.IsPressed();
    // // }



}

