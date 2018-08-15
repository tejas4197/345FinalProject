using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMerge : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		SphereCollider mergeBounds = GetComponent<SphereCollider>();
		if(!mergeBounds) {
			GameController.LogWarning("Merge collider not found for " + name + ": please add a SphereCollider to a child of this GameObject");
			return;
		}

		// Ignore collision with self
		Physics.IgnoreCollision(mergeBounds, transform.parent.GetComponent<Collider>());
	}
	
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		Actor actor = other.gameObject.GetComponentInParent<Actor>();
		if(actor) {
			Debug.Log(name + " collided with actor: " + actor.name);

			if(!actor.isPlayer) {
				Debug.Log("Can merge with enemy " + actor.name);
			}
		}
	}
}
