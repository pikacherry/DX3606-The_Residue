using UnityEngine;
using System.Collections;

public class AnimatedDoorInteractable : InteractableBase
{
    [Header("Door Settings")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string animatorBoolName = "IsOpen";
    [SerializeField] private bool startsOpen = false;
    [SerializeField] private float animationDuration = 1.5f; // set this to match your door open/close clip

    private bool isOpen;
    private int animatorBoolHash;
    private bool isAnimating = false;

    private void Start()
    {
        isOpen = startsOpen;
        animatorBoolHash = Animator.StringToHash(animatorBoolName);

        if (doorAnimator != null)
        {
            doorAnimator.SetBool(animatorBoolHash, isOpen);
        }
    }

    public override void OnInteract()
    {
        if (!isInteractable || doorAnimator == null || isAnimating)
            return;

        base.OnInteract();

        StartCoroutine(AnimateDoor());
    }

    private IEnumerator AnimateDoor()
    {
        isAnimating = true;

        // Toggle state
        isOpen = !isOpen;
        doorAnimator.SetBool(animatorBoolHash, isOpen);

        // Wait for the animation to finish
        yield return new WaitForSeconds(animationDuration);

        isAnimating = false;

        if (!multipleUse)
        {
            isInteractable = false;
        }
    }
}