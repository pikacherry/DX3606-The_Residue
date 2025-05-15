using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour 
{
    private static InputManager _instance;
    private InputActions inputActions;

    public static InputManager Instance {
        get { return _instance; }
    }

    public InteractionInputData interactionInputData;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        
        inputActions = new InputActions();
        Cursor.visible = false;

        if (interactionInputData == null) {
            interactionInputData = ScriptableObject.CreateInstance<InteractionInputData>();
        }
    }

    private void OnEnable() {
        inputActions.Enable();
        inputActions.Player.Interact.performed += OnInteractPerformed;
        inputActions.Player.Interact.canceled += OnInteractCanceled;
    }

    private void OnDisable() {
        inputActions.Player.Interact.performed -= OnInteractPerformed;
        inputActions.Player.Interact.canceled -= OnInteractCanceled;
        inputActions.Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context) {
        interactionInputData.InteractClicked = true;
        interactionInputData.InteractReleased = false; 
        Debug.Log("Interact button PRESSED");
    }

    private void OnInteractCanceled(InputAction.CallbackContext context) {
        interactionInputData.InteractClicked = false;
        interactionInputData.InteractReleased = true;
        Debug.Log("Interact button RELEASED");
    }

    public Vector2 GetPlayerMovement() {
        return inputActions.Player.Move.ReadValue<Vector2>();
    }

    public bool PlayerCrouched() {
        return inputActions.Player.Crouch.IsPressed();
    }

    public bool PlayerRunning() {
        return inputActions.Player.Sprint.IsPressed();
    }
}
