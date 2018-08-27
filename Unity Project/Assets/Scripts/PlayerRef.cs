using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRef : MonoBehaviour {

    #region Singleton
    public static PlayerRef Instance { get; private set; }

    //If an instance of this singleton exists, then destroy the gameobject (this means there are more than one)
    //If an instance doesn't exist, create one
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Warning: found more than one instance of PlayerRef Singleton. Destroying " + gameObject.name + " gameobject.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

}
