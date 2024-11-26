using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnTrigger2D : MonoBehaviour
{
    [Tooltip("Every object tagged with this tag will reduce lives on collision")]
    [SerializeField] private string triggeringTag = "Enemy";

    [Tooltip("Name of the game-over scene to load")]
    [SerializeField] private string gameOverSceneName = "GameOverScene";

    private int lives = 3; // Total lives for the player

    private void Start()
    {
        Debug.Log($"Player starts with {lives} lives.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the correct tag
        if (other.CompareTag(triggeringTag))
        {
            // Decrease lives by 1
            lives--;
            Debug.Log($"Player collided with {other.name}. Lives remaining: {lives}");

            // Destroy the enemy
            Destroy(other.gameObject);

            // Check if lives have reached zero
            if (lives <= 0)
            {
                Debug.Log("Game Over!");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Stops the game in the Unity Editor
#else
                SceneManager.LoadScene(gameOverSceneName); // Load the game-over scene
#endif
            }
        }
    }
}
