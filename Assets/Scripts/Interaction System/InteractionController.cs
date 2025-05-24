using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;

public class InteractionController : MonoBehaviour
{
    #region Variables
        [Header("Data")]
        [SerializeField] private InteractionInputData interactionInputData;
        [SerializeField] private InteractionData interactionData;

        [Space, Header("UI")]
        [SerializeField] private InteractionUI interactionUI;
        public string actionText = "[ Interact ]";


        [Space, Header("Ray Settings")]
        [SerializeField] private float rayDistance;
        [SerializeField] private float raySphereRadius;
        [SerializeField] private LayerMask interactableLayer;

        [Space, Header("Camera")]
        [SerializeField] private Camera initialCamera;

        // [SerializeField] private Transform attachPoint;
    // [SerializeField] private string nextSceneName; 
    #endregion

    #region Private
        private Camera m_cam;
        private bool interacting;
        private float holdTimer = 0f;
        // private GameObject heldObject;

    #endregion

    #region Built In Methods
        void Awake()
        {
            m_cam = initialCamera != null ? initialCamera : Camera.main;
        } 

        void Update()
        {
            CheckForInteractable();
            CheckForInteractableInput();
        }

    #endregion

    #region Methods
        public void SwapCamera(Camera newCamera)
        {
            m_cam = newCamera;
        }
        private void CheckForInteractable()
        {
            Ray ray = new Ray(m_cam.transform.position, m_cam.transform.forward);
            RaycastHit hit;

            bool hitSomething = Physics.SphereCast(ray, raySphereRadius, out hit, rayDistance, interactableLayer);

            if (hitSomething)
            {
                //Debug.Log($"Hit Object: {hit.collider.gameObject.name}");
                InteractableBase interactableBase = hit.transform.GetComponent<InteractableBase>();

                if (interactableBase != null)
                {
                    //Debug.Log("Found Interactable Component!");

                    if (interactionData.IsEmpty())
                    {
                        interactionData.Interactable = interactableBase;
                        interactionUI.SetText(interactableBase.customActionText);
                        interactionUI.Show();

                    }
                    else
                    {
                        if (!interactionData.IsSameInteractable(interactableBase))
                        {
                            interactionData.Interactable = interactableBase;
                            interactionUI.SetText(interactableBase.customActionText);
                            interactionUI.Show();

                        }


                    }

                }

            }
            else
            {

                interactionUI.ResetUI();
                interactionUI.HideCrosshair();
                interactionData.ResetData();

            }

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, hitSomething ? Color.green : Color.red);
        }
        private void CheckForInteractableInput()
        {
            if(interactionData.IsEmpty()) return;
                        
            
            if(interactionInputData.InteractClicked)
            {
                if (!interacting)
                {
                    Debug.Log("Started Interacting!");
                    interacting = true;
                    holdTimer = 0f;
                }
            }

            if(interactionInputData.InteractReleased)
            {   
                Debug.Log("Stopped Interacting!");
                interacting = false;
                holdTimer = 0f;
            }
            
            if(interacting)
            {
                if(!interactionData.Interactable.IsInteractable) return;
                    

                if(interactionData.Interactable.HoldInteract){
                    holdTimer += Time.deltaTime;
                    //Debug.Log($"Holding Interaction: {holdTimer}s");

                    if(holdTimer >= interactionData.Interactable.HoldDuration)
                    {
                        Debug.Log("Hold interaction complete!");
                        interactionData.Interact();
                        interacting = false;
                    }
                }
                else
                {   
                    
                    interactionData.Interact();
                    interacting = false;
                }

            }
        }

    #endregion
}
