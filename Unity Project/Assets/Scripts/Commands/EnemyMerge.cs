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
	/// True if preparing to merge with another enemy
	/// </summary>
	public bool merging;

	/// <summary>
	/// True if have performed a merge with enemy (will be deleted next frame)
	/// </summary>
	public bool merged;

	/// <summary>
	/// Distance between enemies before they merge
	/// </summary>
	public float mergeDistance;

	/// <summary>
	/// Enemy spawned via merge
	/// </summary>
	public Actor newEnemy;

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
			// Debug.Log(name + " collided with actor: " + actor.name);

			if(!merging && !actor.isPlayer) {
				mergeEnemy = actor;

				Debug.Log(transform.parent.name + " moving towards " + mergeEnemy.name);
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
		merging = true;

		// Check if reference to enemy is null
		if(!mergeEnemy) {
			GameController.LogWarning(transform.parent.name + " - attempting to merge with enemy without reference");
			return;
		}
		// Move towards enemy
		mergeEnemy.GetComponent<NavMeshAgent>().SetDestination(transform.parent.position);

		if(WithinRange()) {
			Debug.Log(transform.parent.name + " ready to merge with " + mergeEnemy.name);
			CancelInvoke("MoveTowardsEnemy");

			// Merge with enemy if they haven't merged with us yet
			if(!mergeEnemy.GetComponentInChildren<EnemyMerge>().merged) {
				Merge();
			}
			
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

	void Merge()
	{
		Debug.Log(transform.parent.name + " merging with " + mergeEnemy.name);

		if(!newEnemy) {
			Debug.Log(transform.parent.name + " - enemy merge could not be resolved; resulting enemy not determined");
			return;
		}
		newEnemy = Instantiate(newEnemy);
		newEnemy.name = "New Enemy";
		newEnemy.transform.position = mergeEnemy.transform.position;

		merged = true;

		// Destroy both enemies
		Destroy(mergeEnemy.gameObject);
		Destroy(transform.parent.gameObject);
	}
}
