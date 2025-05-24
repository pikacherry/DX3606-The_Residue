using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    #region Variables
        [Header("Interaction Settings")]
        public float holdDuration;

        public bool holdInteract;

        public bool multipleUse;

        public bool isInteractable;
        
        [Space]
        [Header("UI Settings")] 
        public string customActionText = "[ Interact ]";

    #endregion

    #region Properties

    public float HoldDuration => holdDuration;

        public bool HoldInteract => holdInteract;

        public bool MultipleUse => multipleUse;
        
        public bool IsInteractable => isInteractable;

    #endregion


    #region Methods
        public virtual void OnInteract()
        {
            Debug.Log("Interacted" + gameObject.name);
        }
    #endregion
}
