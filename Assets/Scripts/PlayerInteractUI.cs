using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private Image crosshair; // Assign in Inspector

    public void ShowCrosshair(bool state)
    {
        crosshair.gameObject.SetActive(state);
    }
}