using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/**
 * This component instantiates a given prefab at random time intervals and random bias from its object position.
 * The spawned objects will also disappear after a specified lifetime.
 */
public class TimedSpawnerRandom : MonoBehaviour
{
    [SerializeField] private Mover prefabToSpawn;
    [SerializeField] private Vector3 velocityOfSpawnedObject;

    [Tooltip("Minimum time between consecutive spawns, in seconds")]
    [SerializeField] private float minTimeBetweenSpawns = 0.2f;

    [Tooltip("Maximum time between consecutive spawns, in seconds")]
    [SerializeField] private float maxTimeBetweenSpawns = 1.0f;

    [Tooltip("Maximum distance in X between spawner and spawned objects, in meters")]
    [SerializeField] private float maxXDistance = 1.5f;

    [Tooltip("Maximum distance in Y between spawner and spawned objects, in meters")]
    [SerializeField] private float maxYDistance = 2f;

    [Tooltip("Lifetime of spawned objects, in seconds")]
    [SerializeField] private float objectLifetime = 5.0f;

    [SerializeField] private Transform parentOfAllInstances;

    void Start()
    {
        SpawnRoutine();
    }

    async void SpawnRoutine()
    {
        while (true)
        {
            float timeBetweenSpawnsInSeconds = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            await Awaitable.WaitForSecondsAsync(timeBetweenSpawnsInSeconds); // Coroutine-style waiting

            if (!this) break; // Exit if the spawner is destroyed when switching scenes

            Vector3 positionOfSpawnedObject = new Vector3(
                transform.position.x + Random.Range(-maxXDistance, +maxXDistance),
                transform.position.y + Random.Range(-maxYDistance, +maxYDistance),
                transform.position.z);

            // Spawn the object
            GameObject newObject = Instantiate(prefabToSpawn.gameObject, positionOfSpawnedObject, Quaternion.identity);
            newObject.GetComponent<Mover>().SetVelocity(velocityOfSpawnedObject);

            // Parent the object to the specified parent container
            if (parentOfAllInstances != null)
            {
                newObject.transform.parent = parentOfAllInstances;
            }

            // Destroy the object after the specified lifetime
            Destroy(newObject, objectLifetime);
        }
    }
}
