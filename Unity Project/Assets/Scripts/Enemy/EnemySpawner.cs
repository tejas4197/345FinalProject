using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour {

    public EnemyController enemyController;

    [System.Serializable]
    public class SpawnSettings {
        /// <summary>
        /// Minimum time to spawn enemy
        /// </summary>
        public float minSpawnTime;

        /// <summary>
        /// Max time to spawn enemy
        /// </summary>
        public float maxSpawnTime;

        /// <summary>
        /// List of enemy types that can spawn
        /// </summary>
        public List<Actor.Color> enemyTypes;
    }
    public SpawnSettings settings;

    /// <summary>
    /// Radius within which enemies can spawn
    /// </summary>
    float spawnRadius;

    /// <summary>
    /// Surface to spawn on (not required)
    /// </summary>
    public BoxCollider surface;

    /// <summary>
    /// True if currently spawning an enemy
    /// </summary>
    bool spawning = false;

    /// <summary>
    /// True if spawning is enabled
    /// </summary>
    bool enabled = true;

    Coroutine spawningCoroutine;

    /// <summary>
    /// Used to track actors occupying spawning radius
    /// </summary>
    public List<Actor> occupyingActors;

    void Start ()
    {
        // Get spawn radius (radius of attached SphereCollider)
        SphereCollider spawnBounds = GetComponent<SphereCollider>();
        Assert.IsNotNull(spawnBounds, "SphereCollider component not found on spawner " + name);
        spawnRadius = spawnBounds.radius;

        if(!surface) {
            GameController.LogWarning("BoxCollider surface not found on spawner " + name + ". Please add a reference to the surface enemies will spawn on for easier spawn positioning.", GameController.LogSpawner);
        }

        spawningCoroutine = StartCoroutine(SpawnEnemy());
    }

    /// <summary>
    /// Determines whether there are any Actors occupying the spawn space
    /// </summary>
    public bool IsOccupied()
    {
        // Remove all null actors from list before returning
        occupyingActors.RemoveAll(a => a == null);

        return occupyingActors.Count > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if actor entered spawn zone
        Actor actor = other.GetComponent<Actor>();
        if (actor) {
            GameController.Log(name + " | halted spawning until " + other.gameObject.name + " exits space", GameController.LogSpawner);

            // Add actor to occupying actors list
            occupyingActors.Add(actor);
                
            // Stop subsequent spawning
            enabled = false;

            // Stop coroutine if already in progress
            StopCoroutine(spawningCoroutine);
            spawning = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if actor exited spawn zone
        Actor actor = other.GetComponent<Actor>();
        if (actor) {
            // Remove actor from occupying actors list
            occupyingActors.Remove(actor);
            GameController.Log(name + " | " + other.gameObject.name + " exited space", GameController.LogSpawner);

            // Resume spawning if no longer occupied
            if (!IsOccupied()) {
                enabled = true;
                GameController.Log(name + " | " + " resuming spawning", GameController.LogSpawner);
            }
        }
    }

    void Update ()
    {
        // Spawn when possible
		if(enabled && !spawning) {
            spawningCoroutine = StartCoroutine(SpawnEnemy());
        }
	}

    /// <summary>
    /// Returns random color from list of enemy types
    /// </summary>
    Actor.Color GetRandomColor()
    {
        int index = Random.Range(0, settings.enemyTypes.Count);
        return settings.enemyTypes[index];
    }

    /// <summary>
    /// Spawns new enemy of given color after given delay
    /// </summary>
    IEnumerator SpawnEnemy()
    {
        // Set flag to false so method isn't called until we finish
        spawning = true;

        // Select random spawn time/enemy color
        float delay = Random.Range(settings.minSpawnTime, settings.maxSpawnTime);
        Actor.Color color = GetRandomColor();

        // Wait for delay
        yield return new WaitForSeconds(delay);

        // Get enemy prefab before spawning
        Actor newEnemy = enemyController.GetEnemyPrefab(color);
        Assert.IsNotNull(newEnemy, "Enemy prefab returned null for color: " + color);

        // Spawn enemy
        Instantiate(newEnemy);

        // Set position within spawnRadius
        Vector3 position = transform.position + (Random.insideUnitSphere * spawnRadius);

        // Correct height to top of given surface or spawner height
        if (surface) {
            position.y = surface.bounds.max.y;
        }
        else {
            position.y = transform.position.y;
        }

        // Assign position to enemy
        newEnemy.transform.position = position;

        //GameController.Log("Spawned enemy at " + position.x + ", " + position.y + ", " + position.z, GameController.LogSpawner);

        // Set flag to false so method isn't called until we finish
        spawning = false;
    }
}
