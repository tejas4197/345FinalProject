using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMerge : MonoBehaviour 
{
	/// <summary>
	/// Enemy to merge with
	/// </summary>
	Actor mergeEnemy;

	/// <summary>
	/// Distance between enemies before they merge
	/// </summary>
	public float mergeDistance;

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
				
				mergeEnemy = actor;
				InvokeRepeating("MoveTowardsEnemy", Time.deltaTime, Time.deltaTime);
			}
		}
	}

	/// <summary>
	/// Move towards enemy until with mergeDistance
	/// (called via InvokeRepeating)
	/// </summary>
	void MoveTowardsEnemy()
	{
		// Check if reference to enemy is null
		if(!mergeEnemy) {
			GameController.LogWarning(transform.parent.name + " - attempting to merge with enemy without reference");
			return;
		}
		// Move towards enemy
		mergeEnemy.GetComponent<NavMeshAgent>().SetDestination(transform.parent.position);
		Debug.Log(name + " moving towards " + mergeEnemy.name);

		if(WithinRange()) {
			Debug.Log(transform.parent.name + " ready to merge with " + mergeEnemy.name);
			// Stop moving
			mergeEnemy.GetComponent<NavMeshAgent>().isStopped = true;
			CancelInvoke("MoveTowardsEnemy");

			// Merge with enemy
			Merge(mergeEnemy);
		}
	}

	/// <summary>
	/// Returns true if within merge distance of another enemy
	/// </summary>
	/// <returns></returns>
	bool WithinRange()
	{
		if(!mergeEnemy) {
			return false;
		}
		return Vector3.Magnitude(transform.parent.position - mergeEnemy.transform.position) < mergeDistance;
	}

	void Merge(Actor actor)
	{
		
	}
}
