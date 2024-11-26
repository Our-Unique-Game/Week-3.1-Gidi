using UnityEngine;

public class ShieldThePlayer : MonoBehaviour
{
    [Tooltip("The number of seconds that the shield remains active")]
    [SerializeField] private float duration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Shield triggered by player");

            // Disable collision-related components temporarily
            var destroyComponent = other.GetComponent<DestroyOnTrigger2D>();
            var gameOverComponent = other.GetComponent<GameOverOnTrigger2D>();

            if (destroyComponent) StartCoroutine(ShieldTemporarily(destroyComponent));
            if (gameOverComponent) StartCoroutine(ShieldTemporarily(gameOverComponent));

            Destroy(this.gameObject); // Destroy the shield itself
        }
        else
        {
            Debug.Log("Shield triggered by " + other.name);
        }
    }

    private System.Collections.IEnumerator ShieldTemporarily(MonoBehaviour component)
    {
        component.enabled = false;
        for (float t = duration; t > 0; t--)
        {
            Debug.Log($"Shield active for {t} more seconds.");
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Shield deactivated.");
        component.enabled = true;
    }
}
