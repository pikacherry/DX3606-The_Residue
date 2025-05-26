using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerScript : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
    } 
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
