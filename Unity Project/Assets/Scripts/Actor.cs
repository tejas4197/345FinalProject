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

	/// <summary>
	/// Game Controller
	/// </summary>
	public GameController gameController;

	/// <summary>
	/// Enemy Controller
	/// </summary>
	public EnemyController enemyController;

	public Color color;

	public enum Color {
		BLACK,
		RED,
		GREEN,
		BLUE,
		CYAN,
		MAGENTA,
		YELLOW,
		WHITE
	}

	void Start()
	{
		// Find controllers
		gameController = GameObject.FindObjectOfType<GameController>();
		enemyController = GameObject.FindObjectOfType<EnemyController>();

		if(!gameController) {
			Debug.LogWarning("Game controller missing from scene!");
		}
		if(!enemyController) {
			Debug.LogWarning("Enemy controller missing from scene!");
		}
	}
}
