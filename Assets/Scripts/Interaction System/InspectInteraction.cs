using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectInteraction: MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float rotationSensitivity = 1f;
    public float pickUpRange = 5f;

    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private int holdLayer;

    private bool isInspecting = false;

    void Start()
    {
        holdLayer = LayerMask.NameToLayer("holdLayer");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.CompareTag("canPickUp"))
                    {
                        PickUpObject(hit.transform.gameObject);
                        isInspecting = true;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0) && heldObj != null)
        {
            MoveObject();
            RotateObject();
        }

        if (Input.GetMouseButtonUp(0) && heldObj != null)
        {
            ReturnObject();
            isInspecting = false;
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;

            originalPosition = heldObj.transform.position;
            originalRotation = heldObj.transform.rotation;

            heldObj.transform.parent = holdPos;
            heldObj.transform.localPosition = Vector3.zero;
            heldObj.transform.localRotation = Quaternion.identity;

            heldObj.layer = holdLayer;
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void MoveObject()
    {
        heldObj.transform.position = holdPos.position;
    }

    void RotateObject()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
        float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;

        heldObj.transform.Rotate(Vector3.down, XaxisRotation, Space.World);
        heldObj.transform.Rotate(Vector3.right, YaxisRotation, Space.World);
    }

    void ReturnObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;

        heldObj.transform.position = originalPosition;
        heldObj.transform.rotation = originalRotation;

        heldObj = null;
    }
}
