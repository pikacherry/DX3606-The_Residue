using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToNextScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    void Update()
    {
        // Check if any key or mouse button is pressed
        if (Input.anyKeyDown)
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("Next Scene Name is not set!");
            }
        }
    }
}