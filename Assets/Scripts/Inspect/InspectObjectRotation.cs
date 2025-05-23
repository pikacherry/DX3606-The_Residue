using UnityEngine;

public class InspectObjectRotations : MonoBehaviour 
{
    float rotationAmount = 360f / 4f;
    public int rotations = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotations = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnMouseDown()
    {
        transform.Rotate(0, rotationAmount, 0);
        rotations++;
        if (rotations >= 7)
        {
            rotations = 0;
        }
    }
}
