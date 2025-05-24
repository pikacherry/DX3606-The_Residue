using UnityEngine;

public class DialInteractable : MonoBehaviour //InteractableBase
{
    float rotationAmount = 360f / 7f;
    public int rotations = 0;

    [Header("Audio Settings")]

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] rotateClips; // Assign 3 clips in Inspector


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotations = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // public override void OnInteract()
    // {
    //     if (!isInteractable)
    //         return;

    //     base.OnInteract();
    //     transform.Rotate(rotationAmount, 0, 0);
    // }

    void OnMouseDown()
    {
        transform.Rotate(rotationAmount, 0, 0);
        rotations++;
        if (rotations >= 7)
        {
            rotations = 0;
        }

        PlayRandomSound();
    }

    private void PlayRandomSound()
    {
        if (rotateClips.Length == 0) return;

        int index = Random.Range(0, rotateClips.Length);
        audioSource.PlayOneShot(rotateClips[index]);
    }
}
