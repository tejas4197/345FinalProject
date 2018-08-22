using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*

An Actor is a controllable entity in the scene, either the player or someone else. 

The only difference between the player character and an identically-configured actor is whether 'isPlayer' is set 
to true or false. 

Ideally this class should also be used for enemies, even if they shouldn't be controlled (add a 'controllable' 
bool to class to handle this)

*/

public class Actor : MonoBehaviour {
	/// <summary>
	/// Actor health
	/// </summary>
	public float health;
	
	/// <summary>
	/// True if actor is player character
	/// </summary>
	public bool isPlayer;

    public void TakeDamage(GameObject source, float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ColorModel colorToGive = gameObject.GetComponent<ColorModel>();
            Destroy(gameObject);

            if (colorToGive != null)
                PlayerColorController.Instance.Color += colorToGive;
            else
                Debug.Log("Warning: destroyed object that did not have a ColorModel.");
        }
    }
}
