using UnityEngine;

public class Door : Interactable
{
    public override void Interact() {
        Debug.Log("The door is opening!");
        // Add your door opening logic here (e.g., animation, sound, etc.)
    }
}
