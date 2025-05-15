using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private Image crosshair;

    [SerializeField] private TextMeshProUGUI text;

    public void Show(){
        text.gameObject.SetActive(true);
        ShowCrosshair();

    }

    public void Hide(){
        crosshair.gameObject.SetActive(false);
        HideCrosshair();
    }

    public void SetText(string actiontext) {
        text.SetText(actiontext);

    }

    public void ResetUI(){
        text.SetText("");
    }

    public void ShowCrosshair() {
        crosshair.gameObject.SetActive(true);

        
    }

    public void HideCrosshair() {
        crosshair.gameObject.SetActive(false);
    }
}
