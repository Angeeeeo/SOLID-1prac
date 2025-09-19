using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game2Controller : MonoBehaviour
{
    public List<GameObject> simonButtons;
    public float delayBeforeStart = 5f;
    public float highlightDuration = 0.7f;
    public int sequenceLength = 4;
    public TextMeshProUGUI resultText;

    private List<int> generatedSequence = new List<int>();
    private List<int> playerInput = new List<int>();
    private bool acceptingInput = false;

    public void StartGame()
    {
        resultText.gameObject.SetActive(false);
        StartCoroutine(BeginSimonSequence());
    }

    IEnumerator BeginSimonSequence()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        generatedSequence.Clear();
        playerInput.Clear();
        acceptingInput = false;

        for (int i = 0; i < sequenceLength; i++)
        {
            int randomIndex = Random.Range(0, simonButtons.Count);
            generatedSequence.Add(randomIndex);
        }

        yield return StartCoroutine(ShowSequence());
        acceptingInput = true;
    }

    IEnumerator ShowSequence()
    {
        foreach (int index in generatedSequence)
        {
            GameObject button = simonButtons[index];
            Vector3 originalScale = button.transform.localScale;
            button.transform.localScale = originalScale * 1.3f;
            yield return new WaitForSeconds(highlightDuration);
            button.transform.localScale = originalScale;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ButtonPressed(int index)
    {
        if (!acceptingInput) return;

        playerInput.Add(index);
        if (playerInput.Count == generatedSequence.Count)
        {
            acceptingInput = false;
            CheckResult();
        }
    }

    void CheckResult()
    {
        bool correct = true;
        for (int i = 0; i < generatedSequence.Count; i++)
        {
            if (generatedSequence[i] != playerInput[i])
            {
                correct = false;
                break;
            }
        }

        resultText.gameObject.SetActive(true);
        resultText.text = correct ? "Правильно!" : "Неправильно!";
    }
}
