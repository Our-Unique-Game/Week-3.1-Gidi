using UnityEngine;

public class GameOverOnTrigger2D : MonoBehaviour
{
    private LivesField livesField;

    private void Start()
    {
        livesField = GetComponentInChildren<LivesField>();
        if (!livesField)
        {
            Debug.LogError($"No LivesField component found on {gameObject.name} or its children!");
        }
    }

    private void Update()
    {
        if (livesField != null && livesField.GetValue() <= 0)
        {
            Debug.Log("Game Over Triggered.");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stops the game in the editor
#else
            Application.Quit(); // Exits the application
#endif
        }
    }
}
