using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Tooltip("Text field to display lives")]
    [SerializeField] private TextMeshProUGUI livesText;

    [Tooltip("Text field to display score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private int lives = 3; // Default lives
    private int score = 0; // Default score

    public void SetLives(int newLives)
    {
        lives = newLives;
        UpdateLivesUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateLivesUI()
    {
        livesText.text = $"Lives: {lives}";
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }
}
