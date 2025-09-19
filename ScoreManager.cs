using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour, ICatchHandler
{
    public TMP_Text scoreText;
    private int score = 0;

    void Start()
    {
        UpdateScore();
    }

    public void RegisterCatch(bool correct)
    {
        score += correct ? 1 : -2;
        if (score < 0) score = 0;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Очки: " + score;
    }
}