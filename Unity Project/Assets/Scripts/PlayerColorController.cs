using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorController : MonoBehaviour {

    #region Singleton
    public static PlayerColorController Instance { get; private set; }

    /// <summary>
    /// Red color indicator
    /// </summary>
    public Text redText;

    /// <summary>
    /// Green color indicator
    /// </summary>
    public Text greenText;
    
    /// <summary>
    /// Blue color indicator
    /// </summary>
    public Text blueText;

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

            // Check if we have enough color to unlock door
            if (DoorManager.Instance) {
                DoorManager.Instance.CheckDoors();
            }

            // Update UI
            UpdateUI(_color);
        }
    }

    void UpdateUI(ColorModel color)
    {
        redText.text = "Red: " + color.Red;
        greenText.text = "Green: " + color.Green;
        blueText.text = "Blue: " + color.Blue;
    }
}
