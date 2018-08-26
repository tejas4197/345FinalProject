using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour {

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
    public float spawnRadius;

    public EnemyController enemyController;

    bool canSpawn;


    // Use this for initialization
    void Start ()
    {
        canSpawn = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(canSpawn) {
            float delay = UnityEngine.Random.Range(settings.minSpawnTime, settings.maxSpawnTime);
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

    IEnumerator SpawnEnemy(float delay, Actor.Color color)
    {
        // Set flag to false so method isn't called until we finish
        canSpawn = false;

        // Wait for delay
        yield return new WaitForSeconds(delay);

        // Get reference to prefab
        Actor newEnemy = enemyController.GetEnemyPrefab(color);
        Assert.IsNotNull(newEnemy, "Enemy prefab returned null for color: " + color);

        Instantiate(newEnemy);
        Vector3 position = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
        newEnemy.transform.position = position;
        Debug.Log("Spawned enemy at " + position.x + ", " + position.y + ", " + position.z);

        // Set flag to false so method isn't called until we finish
        canSpawn = true;
    }
}
