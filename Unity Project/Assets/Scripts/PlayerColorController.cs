using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorController : MonoBehaviour {

    #region Singleton
    public static PlayerColorController Instance { get; private set; }

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
            Instance.Color = new ColorModel();
        }
    }
    #endregion

    private ColorModel _color;

    public ColorModel Color
    {
        get { return _color; }
        set
        {
            _color = value;
            if (DoorManager.Instance)
            {
                DoorManager.Instance.CheckDoors();
            }
        }
    }
}
