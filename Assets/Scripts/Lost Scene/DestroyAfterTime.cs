using UnityEngine;
using System.Collections;

public class CanvasSwitcher : MonoBehaviour
{
    [SerializeField] private float delay = 3f;
    [SerializeField] private GameObject nextCanvas;

    private void Start()
    {
        
        StartCoroutine(SwitchCanvasAfterDelay());
    }

    private IEnumerator SwitchCanvasAfterDelay()
    {
        // wait for the specified delay
        yield return new WaitForSeconds(delay);

        // destroy this canvas (or you could just disable it with SetActive(false))
        Destroy(gameObject);

        // activate the next canvas if assigned
        if (nextCanvas != null)
            nextCanvas.SetActive(true);
    }
}