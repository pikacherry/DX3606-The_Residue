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
    //[SerializeField] private TextMeshProUGUI interactionPromptText;
    InputActions inputActions;

    [SerializeField] private Transform attachPoint;
    Vector3 originalPosition;
    quaternion originalRotation;

    Collider lockCollider;

    public Canvas LockInfoCanvas;
    public Canvas ClickAgainToRotateCanvas;

    PlayerController playerController;

    InspectObjectRotations inspectObjectRotations;

    [Space, Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        //customActionText = "[ Inspect ]";
        lockCamera.gameObject.SetActive(false);
        ClickAgainToRotateCanvas.gameObject.SetActive(false);
        LockInfoCanvas.gameObject.SetActive(false);
        //interactionPromptText.gameObject.SetActive(false);
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
            //customActionText = "[ Inspect ]";
        }

    }

    public override void OnInteract()
    {
        if (!isInteractable) return;

        audioSource.PlayOneShot(clip);
        

        base.OnInteract();

        lockCamera.gameObject.SetActive(true);

        GetMeOutofLock(false);
    }


    private void GetMeOutofLock(bool outOfLock)
    {   
        

        LockInfoCanvas.gameObject.SetActive(!outOfLock);
        ClickAgainToRotateCanvas.gameObject.SetActive(!outOfLock);
        //interactionPromptText.gameObject.SetActive(!outOfLock);
        

        //lockUIBehaviour.LockInfoCanvas.enabled = outOfLock;
        mainCamera.enabled = outOfLock;
        lockCamera.enabled = !outOfLock;
        lockCollider.enabled = outOfLock;
        crosshair.gameObject.SetActive(!outOfLock);
        //enbale/disable the player movement        
        playerController.isInspecting = !outOfLock;

        if (!outOfLock)
        {
            customActionText = ""; // or null
        }
        
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
