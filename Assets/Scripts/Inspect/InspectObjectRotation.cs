using Unity.Mathematics;
using UnityEngine;

public class InspectObjectRotations : MonoBehaviour 
{
    float rotationAmount = 360f / 4f;
    public int rotations = 0;
    quaternion originalRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotations = 0;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetRotation()
    {
        transform.rotation = originalRotation;
        rotations = 0;
    }

    void OnMouseDown()
    {
        transform.Rotate(0, rotationAmount, 0);
        rotations++;
        if (rotations >= 4)
        {
            rotations = 0;
        }
    }
}
