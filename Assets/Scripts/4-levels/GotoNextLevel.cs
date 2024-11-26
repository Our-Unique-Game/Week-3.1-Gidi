using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoNextLevel : MonoBehaviour
{
    [Tooltip("The tag that triggers lives reduction (e.g., 'Enemy')")]
    [SerializeField] private string enemyTag = "Enemy";

    [Tooltip("The tag that triggers level progression (e.g., 'ToNextLevel')")]
    [SerializeField] private string nextLevelTag = "ToNextLevel";

    [Tooltip("The name of the scene to load for level progression")]
    [SerializeField] private string sceneName;

    [Tooltip("The name of the scene to load when the player runs out of lives")]
    [SerializeField] private string gameOverSceneName;

    [Tooltip("Player's starting lives")]
    [SerializeField] private int lives = 3;

    private UIManager uiManager;
    private bool isGameOver = false; // Prevent multiple triggers

    private void Start()
    {
        Debug.Log($"Player starts with {lives} lives.");

        // Find the UIManager in the scene
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetLives(lives);
        }
    }

    public void AddLives(int amount)
    {
        lives += amount;
        if (uiManager != null)
        {
            uiManager.SetLives(lives);
        }
        Debug.Log($"Lives increased by {amount}. Current lives: {lives}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle collisions with enemies
        if (other.CompareTag(enemyTag) && CompareTag("Player"))
        {
            // Prevent multiple triggers
            if (isGameOver) return;

            lives--;
            if (uiManager != null)
            {
                uiManager.SetLives(lives);
            }
            Debug.Log($"Player hit by enemy. Lives remaining: {lives}");

            // Check if lives are 0 or less
            if (lives <= 0)
            {
                isGameOver = true; // Mark as game over to prevent multiple calls
                Debug.Log($"Game Over! Transitioning to game-over scene: {gameOverSceneName}");
                SceneManager.LoadScene(gameOverSceneName); // Load the game-over scene
            }

            return; // Exit after handling enemy collision
        }

        // Handle level progression
        if (other.CompareTag(nextLevelTag) && CompareTag("Player"))
        {
            Debug.Log($"Triggering level transition to {sceneName}.");
            SceneManager.LoadScene(sceneName); // Load the next level
        }
    }
}
