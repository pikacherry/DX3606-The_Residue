using System.Collections;
using UnityEngine;
using TMPro;

public class TimedDialogue : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text dialogueText;

    [Header("Dialogue Settings")]
    [TextArea] 
    [SerializeField] private string[] lines;
    [SerializeField] private float displayDuration = 1f;

    private void Start()
    {
        if (dialogueText == null)
        {
            enabled = false;
            return;
        }
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {
        foreach (var line in lines)
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(displayDuration);
        }

        // Optionally clear text or trigger next event
        dialogueText.text = "";
    }
}