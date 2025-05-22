using UnityEngine;

public class LockLook : MonoBehaviour
{
    public float sensitivity = 100f;
    public float smoothTime = 0.05f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private float currentXRotation;
    private float currentYRotation;
    private float xRotationVelocity;
    private float yRotationVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRotation = currentXRotation = 20f;
        yRotation = currentYRotation = 180f;
    }

    void Update()
    {
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        // Vertical (X-axis) rotation clamp
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -20f, 30f);

        // Horizontal (Y-axis) rotation clamp with wraparound
        yRotation += mouseX;
        yRotation = ClampAngleWrapped(yRotation, 140f, -160f);

        // Smooth rotation
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVelocity, smoothTime);
        currentYRotation = Mathf.SmoothDampAngle(currentYRotation, yRotation, ref yRotationVelocity, smoothTime);

        transform.localRotation = Quaternion.Euler(currentXRotation, currentYRotation, 0f);
    }

    // Custom clamp for angles that wrap around -180 to 180
    float ClampAngleWrapped(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle + 180f, 360f) - 180f;
        min = Mathf.Repeat(min + 180f, 360f) - 180f;
        max = Mathf.Repeat(max + 180f, 360f) - 180f;

        if (min < max)
            return Mathf.Clamp(angle, min, max);
        else
            return (angle > max && angle < min) ? 
                (Mathf.Abs(angle - min) < Mathf.Abs(angle - max) ? min : max) : angle;
    }
}
