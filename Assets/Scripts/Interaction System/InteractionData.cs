using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Data", menuName = "Scriptable Objects/Interaction Data")]
public class InteractionData : ScriptableObject
{
    private InteractableBase interactableBase;

    public InteractableBase Interactable
    {
        get => interactableBase;
        set => interactableBase = value;
    }

    public void Interact(){
        if (interactableBase != null){
            interactableBase.OnInteract();
            ResetData();
        }
        
    }

    // public bool IsSameInteractable(InteractableBase _newInteractableBase){
    //     return interactableBase == _newInteractableBase;
    // }
    public bool IsSameInteractable(InteractableBase _newInteractableBase) => interactableBase == _newInteractableBase;
    
    public bool IsEmpty() => interactableBase == null;

    public void ResetData() => interactableBase = null;

    
}
