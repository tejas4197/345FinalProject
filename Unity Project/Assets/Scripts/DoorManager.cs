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
        public bool isUnlocked;

        public float redThreshold;
        public float greenThreshold;
        public float blueThreshold;
    }

    /// <summary>
    /// List of doors
    /// </summary>
    public List<Door> doors;

    public void CheckDoors()
    {
        // Iterate over all doors and check if we can unlock them
        foreach(Door door in doors.FindAll(d => !d.isUnlocked)) {
            if (PlayerColorController.Instance.Color >= new ColorModel(door.redThreshold, door.greenThreshold, door.blueThreshold)) {
                door.doorObject.SetActive(false);
                door.isUnlocked = true;
            }
        }
    }
}
