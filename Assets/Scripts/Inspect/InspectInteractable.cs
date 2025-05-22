using UnityEngine;

public class InspectInteractable : InteractableBase
{

    [Space, Header("Attach")]
    [SerializeField] private Transform attachPoint;
    [SerializeField] private bool isAttached = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
