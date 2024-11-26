using UnityEngine;

public class LivesField : MonoBehaviour
{
    private int lives = 3; // Default number of lives

    public void Add(int amount)
    {
        lives += amount;
        UpdateDisplay();
    }

    public int GetValue()
    {
        return lives;
    }

    private void UpdateDisplay()
    {
        var textMesh = GetComponent<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = lives.ToString();
        }
        else
        {
            Debug.LogError($"No TextMesh found on {gameObject.name}! Lives cannot be displayed.");
        }
    }
}
