using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {

    #region Singleton
    public static DoorManager Instance { get; private set; }

    //If an instance of this singleton exists, then destroy the gameobject (this means there are more than one)
    //If an instance doesn't exist, create one
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Warning: found more than one instance of PlayerColorController Singleton. Destroying " + gameObject.name + " gameobject.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public GameObject firstDoor;
    public float firstConditionRed;
    public float firstConditionGreen;
    public float firstConditionBlue;

    [System.Serializable]
    public class Door {
        /// <summary>
        /// Reference to door object
        /// </summary>
        public GameObject doorObject;

        /// <summary>
        /// True if door is unlocked
        /// </summary>
        public bool isUnlocked;

        public float redThreshold;
        public float greenThreshold;
        public float blueThreshold;

        /// <summary>
        /// Spawners to activate after opening door
        /// </summary>
        public List<EnemySpawner> spawners;
    }

    /// <summary>
    /// List of doors
    /// </summary>
    public List<Door> doors;

    public void CheckDoors()
    {
        foreach(Door door in doors) {
            if (PlayerColorController.Instance.Color >= new ColorModel(door.redThreshold, door.greenThreshold, door.blueThreshold)) {
                // Hide door object and set unlocked
                door.doorObject.SetActive(false);
                door.isUnlocked = true;

                // Activate spawners behind door
                foreach (EnemySpawner spawner in door.spawners) {
                    spawner.gameObject.SetActive(true);
                }
            }
        }
        
    }
}
