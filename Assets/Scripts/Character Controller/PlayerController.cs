using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 1.0f;
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float runSpeed = 2.0f;
    [SerializeField] private float crouchWalkSpeed = 0.5f;
    
    [SerializeField] private float gravityValue = -9.81f;

    //[SerializeField]private float speeder = 3f;

    [SerializeField] private float crouchSpeed, normalHeight, crouchHeight,crouchScale;
    //[SerializeField] private Vector3 offset;
    [SerializeField] private Transform player;
    
    
    private bool walking;
    [SerializeField]
    private bool crouching;
    private bool crouchWalking;
    private bool sprinting;

    private InputManager inputManager;
    public Transform cameraTargetTransform;
    private Animator animator;

    int isWalkingHash;
    int isCrouchingHash;
    int isCrouchWalkingHash;
    int isRunningHash;

    [SerializeField] bool underSomething = false;
    [SerializeField] LayerMask overhedFilter;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTargetTransform = Camera.main.transform;
        animator = GetComponent<Animator>();

        Camera.main.GetComponent<MouseLook>().playerBody = transform;

        isWalkingHash = Animator.StringToHash("IsWalking");
        isCrouchingHash = Animator.StringToHash("IsCrouching");
        isCrouchWalkingHash = Animator.StringToHash("IsCrouchWalking");
        isRunningHash = Animator.StringToHash("IsRunning");

        // ✅ Reset CharacterController properties
        controller.height = normalHeight;
        controller.center = new Vector3(0, normalHeight / 2f, 0);

        crouching = false;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);  
        move = cameraTargetTransform.forward * move.z + cameraTargetTransform.right * move.x;
        if(move.magnitude > 0){
            controller.Move(move.normalized * playerSpeed * Time.deltaTime);
        }

        //controller.Move(move * Time.deltaTime * playerSpeed);

        walking = movement.magnitude > 0;

        // Update the walking animation
        animator.SetBool(isWalkingHash, walking);
        
        CheckOverHead();
        HandleCrouch();
        HandleCrouchWalking();
        HandleSprint();

        //rotate player
        // if (move != Vector3.zero)
        // {
        //     gameObject.transform.forward = move;
        // }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        
        

    }

    void CheckOverHead()
    {
        underSomething = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.up, out hit, 2.5f, overhedFilter))
        {
            if (hit.point.y <= transform.position.y + normalHeight + 0.2f)
            {
                underSomething = true;
            }
        }
    }

    private void HandleCrouch() {
        // if (inputManager.PlayerCrouched())
        // {
        //     crouching = !crouching;
        // }

        // if (crouching)
        if(inputManager.PlayerCrouched()) 
        {
            
            crouching = true;
            animator.SetBool(isCrouchingHash,true); 
            controller.height = Mathf.MoveTowards(controller.height, crouchHeight, crouchScale * Time.deltaTime);

            controller.center = new Vector3(0, controller.height/2f, 0);
            if(controller.height <= crouchHeight) {
                controller.height = crouchHeight; 
                controller.center = new Vector3(0, controller.height/2f, 0);
            }

        } else {
            crouching = false;
            if (underSomething) return;
            animator.SetBool(isCrouchingHash,false);
            controller.height = Mathf.MoveTowards(controller.height, normalHeight, crouchScale * Time.deltaTime);
            controller.center = new Vector3(0, controller.height/2f, 0);
            if(controller.height >= normalHeight) {
                controller.height = normalHeight; 
                controller.center = new Vector3(0,controller.height/2f, 0);
            }
        }

   
    }
    
    public bool IsCrouching()
    {
        return crouching;
    }

    private void HandleCrouchWalking()
    {
        if (walking && inputManager.PlayerCrouched())
        {
            crouchWalking = true;
            animator.SetBool(isCrouchWalkingHash, true);
            playerSpeed = crouchWalkSpeed;

        }
        else if (!sprinting)
        {
            crouchWalking = false;
            animator.SetBool(isCrouchWalkingHash, false);
            playerSpeed = walkSpeed;

        }
    }

    private void HandleSprint() {
        if(walking && inputManager.PlayerRunning()) {
            sprinting = true;
            animator.SetBool(isRunningHash,true);
            playerSpeed = runSpeed;  
        } else if (!crouchWalking){
            sprinting = false;
            animator.SetBool(isRunningHash,false);
            playerSpeed = walkSpeed;
        }
    }
   
}