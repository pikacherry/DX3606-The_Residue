using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;

public class InspectInteractable : InteractableBase
{

    [Header("Inspect Settings")]
    Camera mainCamera;

    InteractionController interactionController;
    [SerializeField] Camera lockCamera;
    [SerializeField] private Image crosshair;
    InputActions inputActions;

    [SerializeField] private Transform attachPoint;
    

    Collider lockCollider;

    public Canvas LockInfoCanvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockCamera.gameObject.SetActive(false);
        LockInfoCanvas.gameObject.SetActive(false);
        interactionController = FindFirstObjectByType<InteractionController>();
        mainCamera = Camera.main;
        lockCollider = GetComponent<Collider>();

        inputActions = new InputActions();
        inputActions.Enable();

    }

    // Update is called once per frame
    void Update()
    {


    }

public override void OnInteract()
{
    if (!isInteractable) return;

    base.OnInteract();

    transform.position = attachPoint.position;
    transform.rotation = attachPoint.rotation;

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

        if (mainCamera.enabled) interactionController.SwapCamera(mainCamera);
        else interactionController.SwapCamera(lockCamera);

    }
    

}
