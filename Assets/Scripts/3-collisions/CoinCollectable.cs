using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    [Tooltip("How many lives this coin adds to the player")]
    [SerializeField] private int lifeToAdd = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the Player collided with the coin
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player collected a coin. Adding {lifeToAdd} life.");

            // Find the GotoNextLevel script to update lives
            GotoNextLevel playerLives = other.GetComponent<GotoNextLevel>();
            if (playerLives != null)
            {
                playerLives.AddLives(lifeToAdd); // Add life to the player
            }
            else
            {
                Debug.LogError("GotoNextLevel script not found on the Player.");
            }

            // Destroy the coin
            Destroy(gameObject);
        }
    }
}
