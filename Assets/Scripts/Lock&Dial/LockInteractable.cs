using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    [SerializeField] int[] lockCombination;
    [SerializeField] int[] currentCombination;
    [SerializeField] DialInteractable[] dials;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockCombination = new int[] { 0, 1, 2, 3, 4 };
        currentCombination = new int[] { 0, 0, 0, 0, 0 };
        interactionController = FindFirstObjectByType<InteractionController>();
        mainCamera = Camera.main;
        lockUIBehaviour = GetComponentInParent<LockUIBehaviour>();
        lockCollider = GetComponent<Collider>();

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

        print(currentCombination[0]+" "+ currentCombination[1]+" "+ currentCombination[2]+" "+ currentCombination[3]+" "+ currentCombination[4]);
        if (currentCombination.SequenceEqual(lockCombination))
        {
            print("Unlocked");
            // do the other things you want
            GetMeOutofLock(true);
        }
    }
    public override void OnInteract()
    {
        if (!isInteractable)
            return;

        base.OnInteract();
        GetMeOutofLock(false);
    }

    private void GetMeOutofLock(bool outOfLock)
    {
        lockUIBehaviour.LockInfoCanvas.enabled = outOfLock;
        mainCamera.enabled = outOfLock;
        lockCamera.enabled = !outOfLock;
        lockCollider.enabled = outOfLock;
        crosshair.gameObject.SetActive(!outOfLock);

        if (mainCamera.enabled) interactionController.SwapCamera(mainCamera);
        else interactionController.SwapCamera(lockCamera);
       
    }
}
