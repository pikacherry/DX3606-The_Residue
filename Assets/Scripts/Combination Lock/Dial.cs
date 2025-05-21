using UnityEngine;
using UnityEngine.Events;

public class Dial : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float animationDuration;
    [SerializeField] private bool isRotating = false;    
    private int currentIndex;

    [Header(" Events ")]
    [SerializeField] private UnityEvent<Dial> onDialRotated;

    private void Start()
    {
        currentIndex = Random.Range(0, 4);
        transform.localRotation = Quaternion.Euler(currentIndex * -51, 0, 0);

    }

    public void Rotate() 
    {
        if (isRotating) return;

        isRotating = true;

        currentIndex++;

        if (currentIndex >= 4) currentIndex = 0;

        LeanTween.cancel(gameObject);
        LeanTween.rotateAroundLocal(gameObject, Vector3.right, -51, animationDuration).setOnComplete(RotationCompleteCallback);

    }

    private void RotationCompleteCallback()
    {
        onDialRotated?.Invoke(this);
    }


    public int GetNumber()
    {
        return currentIndex; 
    }

    public void Lock()
    {
        isRotating = true;
    }

    public void Unlock() 
    {
        isRotating = false;
    }
}
