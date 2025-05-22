using UnityEngine;

public class LockLook : MonoBehaviour
{
    public float sensitivity = 100f;
    public float smoothTime = 0.05f;
    private float yRotation = 0f;
    private float xRotation = 0f;
    private float currentXRotation;
    private float currentYRotation;
    private float xRotationVelocity;
    private float yRotationVelocity;
    private Vector3 currentCameraVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentXRotation = 0; //transform.localRotation.eulerAngles.x;   
        currentYRotation = 0; //transform.localRotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVelocity, smoothTime);

        float targetY = currentYRotation + mouseX;
        currentYRotation = Mathf.SmoothDampAngle(currentYRotation, targetY, ref yRotationVelocity, smoothTime);
        transform.localRotation = Quaternion.Euler(currentXRotation, currentYRotation, 0f);
        
    }
}
