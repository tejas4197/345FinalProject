using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [System.Serializable]
    public class SpawnSettings {
        public float minSpawnTime;
        public float maxSpawnTime;

        public List<Actor.Color> enemyTypes;
    }
    public SpawnSettings settings;

    /// <summary>
    /// Radius within which enemies can spawn
    /// </summary>
    public float spawnRadius;

    /// <summary>
    /// List of enemy types that can spawn
    /// </summary>

    public EnemyController enemyController;

    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
