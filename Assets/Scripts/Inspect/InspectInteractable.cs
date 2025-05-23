using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;
using Unity.Mathematics;

public class InspectInteractable : InteractableBase
{

    [Header("Inspect Settings")]
    Camera mainCamera;

    InteractionController interactionController;
    [SerializeField] Camera lockCamera;
    [SerializeField] private Image crosshair;
    InputActions inputActions;

    [SerializeField] private Transform attachPoint;
    Vector3 originalPosition;
    quaternion originalRotation;

    Collider lockCollider;

    public Canvas LockInfoCanvas;

    PlayerController playerController;

    InspectObjectRotations inspectObjectRotations;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockCamera.gameObject.SetActive(false);
        LockInfoCanvas.gameObject.SetActive(false);
        interactionController = FindFirstObjectByType<InteractionController>();
        mainCamera = Camera.main;
        lockCollider = GetComponent<Collider>();
        playerController = FindFirstObjectByType<PlayerController>();
        inspectObjectRotations = GetComponentInChildren<InspectObjectRotations>();

        inputActions = new InputActions();
        inputActions.Enable();

        originalPosition = transform.position;
        originalRotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        float esc = inputActions.Player.Escape.ReadValue<float>();
        if (esc > 0)
        {
            GetMeOutofLock(true);
        }

    }

    public override void OnInteract()
    {
        if (!isInteractable) return;

        base.OnInteract();

        lockCamera.gameObject.SetActive(true);

        GetMeOutofLock(false);
    }


    private void GetMeOutofLock(bool outOfLock)
    {

        LockInfoCanvas.gameObject.SetActive(!outOfLock);

        //lockUIBehaviour.LockInfoCanvas.enabled = outOfLock;
        mainCamera.enabled = outOfLock;
        lockCamera.enabled = !outOfLock;
        lockCollider.enabled = outOfLock;
        crosshair.gameObject.SetActive(!outOfLock);
        //enbale/disable the player movement        
        playerController.isInspecting = !outOfLock;


        if (mainCamera.enabled)
        {
            interactionController.SwapCamera(mainCamera);
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            inspectObjectRotations.ResetRotation();
        }
        else
        {
            // move the lock camera to face the object
            lockCamera.transform.position = new Vector3(transform.position.x, lockCamera.transform.position.y, lockCamera.transform.position.z);
            interactionController.SwapCamera(lockCamera);
            transform.position = attachPoint.position;
            transform.rotation = attachPoint.rotation;
        }

    }    

}
