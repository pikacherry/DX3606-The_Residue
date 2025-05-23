using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class LockInteractable : InteractableBase
{
    [Header("Lock Settings")]
    Camera mainCamera;
    [SerializeField] Camera lockCamera;
    [SerializeField] private Image crosshair;
    LockUIBehaviour lockUIBehaviour;
    InteractionController interactionController;
    InputActions inputActions;

    Collider lockCollider;

    public Canvas LockInfoCanvas;


    [SerializeField] int[] lockCombination;
    [SerializeField] int[] currentCombination;
    [SerializeField] DialInteractable[] dials;

    PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockCamera.gameObject.SetActive(false);
        LockInfoCanvas.gameObject.SetActive(false);
        
        
        lockCombination = new int[] { 5, 2, 4, 3, 1 };
        currentCombination = new int[] { 0, 0, 0, 0, 0 };
        interactionController = FindFirstObjectByType<InteractionController>();
        mainCamera = Camera.main;
        lockUIBehaviour = GetComponentInParent<LockUIBehaviour>();
        lockCollider = GetComponent<Collider>();
        playerController = FindFirstObjectByType<PlayerController>();

        inputActions = new InputActions();
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        float esc = inputActions.Player.Escape.ReadValue<float>();
        if (esc > 0)
        {
            GetMeOutofLock(true);
        }
        currentCombination = dials.Select(d => d.rotations).ToArray();

        // print(currentCombination[0]+" "+ currentCombination[1]+" "+ currentCombination[2]+" "+ currentCombination[3]+" "+ currentCombination[4]);
        if (currentCombination.SequenceEqual(lockCombination))
        {
            print("Unlocked");
            // do the other things you want
            GetMeOutofLock(true);
        }
    }
    public override void OnInteract()
    {   
        lockCamera.gameObject.SetActive(true);

        if (!isInteractable)
            return;

        base.OnInteract();
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

        if (mainCamera.enabled) interactionController.SwapCamera(mainCamera);
        else interactionController.SwapCamera(lockCamera);
       
    }
}
