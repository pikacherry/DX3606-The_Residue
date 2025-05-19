using UnityEngine;

public class PickUpInteractable : InteractableBase
{
    [Header("Data")]
    [SerializeField] private InteractionInputData interactionInputData;


    [Space, Header("Attach")]
    [SerializeField] private Transform attachPoint;
    [SerializeField] private bool isAttached = false;



    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            attachPoint = player.transform.Find("AttachPoint");

        }
        
    }

    public override void OnInteract()
    {
        if (!isAttached)
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        if (attachPoint == null) return;

        transform.SetParent(attachPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        isAttached = true;
        GetComponent<Rigidbody>().isKinematic = true; // Disables physics while attached
        GetComponent<Collider>().enabled = false; // Prevents re-interaction
        Debug.Log("Object attached to player!");
    }

    public void TryDestroy()
    {
        Debug.Log("Object destroyed!");
        Destroy(gameObject);
    }

    
}


