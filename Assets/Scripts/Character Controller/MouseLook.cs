using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Assign the Player object here in the inspector
    // public Transform headBone;
    // public Vector3 offset;
    // public float smoothSpeed = 10f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate camera up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate player left/right
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // void LateUpdate()
    // {
    //     if (headBone == null) return;

    //     transform.position = Vector3.Lerp(transform.position, headBone.position + offset, smoothSpeed * Time.deltaTime);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, headBone.rotation, smoothSpeed * Time.deltaTime);
    // }
}
