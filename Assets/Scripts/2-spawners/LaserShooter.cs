using UnityEngine;

public class LaserShooter : ClickSpawner
{
    [SerializeField]
    [Tooltip("How many points to add when the laser hits an enemy")]
    private int pointsToAdd = 1;

    [SerializeField]
    [Tooltip("Speed of the laser when fired")]
    private float laserSpeed = 10f;

    private UIManager uiManager;

    private void Start()
    {
        // Find the UIManager in the scene
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
        }
    }

    protected override GameObject spawnObject()
    {
        // Spawn the laser prefab
        GameObject newObject = base.spawnObject();

        // Set the laser's velocity
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = transform.right * laserSpeed; // Fire in the direction the player is facing
        }

        // Add a collision handler to the laser to detect when it hits an enemy
        LaserCollisionHandler collisionHandler = newObject.AddComponent<LaserCollisionHandler>();
        collisionHandler.Initialize(pointsToAdd, uiManager);

        return newObject;
    }
}

public class LaserCollisionHandler : MonoBehaviour
{
    private int pointsToAdd;
    private UIManager uiManager;

    public void Initialize(int points, UIManager manager)
    {
        pointsToAdd = points;
        uiManager = manager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Add points to the UIManager
            if (uiManager != null)
            {
                uiManager.AddScore(pointsToAdd);
            }

            // Destroy both the laser and the enemy
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
