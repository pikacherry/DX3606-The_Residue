using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 100f;
    public float smoothTime = 0.05f;

    public Transform playerBody;
    public Transform headBone;
    public Vector3 standingOffset = Vector3.zero;
    public Vector3 crouchOffset = new Vector3(0f, -0.5f, 0f);

    private float xRotation = 0f;
    private float currentXRotation;
    private float xRotationVelocity;
    private float yRotationVelocity;
    private Vector3 currentCameraVelocity;

    private PlayerController playerController; // 👈 Reference to read crouch state

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentXRotation = xRotation;

        // 👇 Get reference to PlayerController
        playerController = playerBody.GetComponent<PlayerController>();
    }

    void Update()
    {
        HandleMouseLook();
        FollowHeadBone();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVelocity, smoothTime);
        transform.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f);

        float targetY = playerBody.eulerAngles.y + mouseX;
        float smoothY = Mathf.SmoothDampAngle(playerBody.eulerAngles.y, targetY, ref yRotationVelocity, smoothTime);
        playerBody.rotation = Quaternion.Euler(0f, smoothY, 0f);
    }

    void FollowHeadBone()
    {
        if (headBone == null || playerController == null) return;

        // 👇 Use crouching state from PlayerController
        Vector3 offset = playerController.IsCrouching() ? crouchOffset : standingOffset;
        Vector3 targetPosition = headBone.TransformPoint(offset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentCameraVelocity, smoothTime);

        Quaternion targetRotation = Quaternion.Euler(currentXRotation, playerBody.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / smoothTime);
    }
}