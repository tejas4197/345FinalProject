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
    /// True if enemy can spawn this frame
    /// </summary>
    bool canSpawn;


    // Use this for initialization
    void Start ()
    {
        // Set flag to true
        canSpawn = true;

        // Get spawn radius (radius of attached SphereCollider)
        SphereCollider spawnBounds = GetComponent<SphereCollider>();
        Assert.IsNotNull(spawnBounds, "SphereCollider component not found on spawner " + name);
        spawnRadius = spawnBounds.radius;

        if(!surface) {
            Debug.LogWarning("BoxCollider surface not found on spawner " + name + ". Please add a reference to the surface enemies will spawn on for easier spawn positioning.");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(canSpawn) {
            // Select random spawn time/enemy color
            float delay = Random.Range(settings.minSpawnTime, settings.maxSpawnTime);
            Actor.Color color = GetRandomColor();

            StartCoroutine(SpawnEnemy(delay, color));
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
    IEnumerator SpawnEnemy(float delay, Actor.Color color)
    {
        // Set flag to false so method isn't called until we finish
        canSpawn = false;

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

        newEnemy.transform.position = position;


        Debug.Log("Spawned enemy at " + position.x + ", " + position.y + ", " + position.z);

        // Set flag to false so method isn't called until we finish
        canSpawn = true;
    }
}
