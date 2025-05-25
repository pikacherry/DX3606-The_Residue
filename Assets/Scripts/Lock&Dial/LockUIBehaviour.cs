using UnityEngine;

public class LockUIBehaviour : MonoBehaviour
{
    //public Canvas LockInfoCanvas;
    Camera mainCamera;
    [SerializeField] Camera lockCamera;
    LockInteractable lockBehaviour;

    [SerializeField] GameObject gameObject;
    // [SerializeField] GameObject hint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        lockBehaviour = GetComponentInChildren<LockInteractable>();
        // hint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //LockInfoCanvas.enabled = true;
        lockBehaviour.enabled = true;
        // hint.SetActive(true);
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        //LockInfoCanvas.enabled = false;
        lockBehaviour.enabled = false;
        // hint.SetActive(false);
    }
}
