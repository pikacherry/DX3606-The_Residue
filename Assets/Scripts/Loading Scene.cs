using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image loadingBarFill;

    public float minLoadingTime = 5f;       
    public float fillSpeed = 0.5f;         

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadingSceneAsync(sceneId));
    }

    IEnumerator LoadingSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;

        LoadingScreen.SetActive(true);

        float timer = 0f;
        float targetFill = 0f;

        while (!operation.isDone)
        {
            // Get target fill amount based on load progress
            targetFill = Mathf.Clamp01(operation.progress / 0.9f);

            // Slowly move the actual fill amount toward the target
            loadingBarFill.fillAmount = Mathf.Lerp(loadingBarFill.fillAmount, targetFill, fillSpeed * Time.deltaTime);

            timer += Time.deltaTime;

            // Wait until scene is loaded AND the minimum time has passed
            if (operation.progress >= 0.9f && timer >= minLoadingTime && loadingBarFill.fillAmount >= 0.99f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
