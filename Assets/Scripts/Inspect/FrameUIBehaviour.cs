using UnityEngine;

public class FrameUIBehaviour : MonoBehaviour
{
    //public Canvas LockInfoCanvas;
    Camera mainCamera;
    [SerializeField] Camera lockCamera;
    InspectInteractable inspectkBehaviour;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        inspectkBehaviour = GetComponentInChildren<InspectInteractable>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //LockInfoCanvas.enabled = true;
        inspectkBehaviour.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //LockInfoCanvas.enabled = false;
        inspectkBehaviour.enabled = false;
    }
}
