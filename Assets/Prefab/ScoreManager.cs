using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    int score = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }
}
