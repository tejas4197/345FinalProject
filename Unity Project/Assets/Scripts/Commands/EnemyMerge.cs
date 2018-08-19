using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMerge : MonoBehaviour 
{
	// Indicates if enemy is merging or has merged (will be deleted next frame)
	public enum MergeState {
		NOT_MERGING,
		MERGING,
		MERGED
	}

	public MergeState state;

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

		// Initialize merge state
		state = MergeState.NOT_MERGING;
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

			if(!state.Equals(MergeState.MERGING) && !actor.isPlayer) {
				mergeEnemy = actor;

				Debug.Log(transform.parent.name + " moving towards " + mergeEnemy.name);
				InvokeRepeating("MoveTowardsEnemy", Time.deltaTime, Time.deltaTime);
				
				// Set merge state
				state = MergeState.MERGING;
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

		if(WithinRange()) {
			Debug.Log(transform.parent.name + " ready to merge with " + mergeEnemy.name);
			CancelInvoke();

			// Merge with enemy if they haven't merged with us yet
			if(!mergeEnemy.GetComponentInChildren<EnemyMerge>().state.Equals(MergeState.MERGED)) {
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
		// Return false if enemy is null
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
		
		// Spawn new enemy at current location
		newEnemy.transform.position = mergeEnemy.transform.position;
		newEnemy = Instantiate(newEnemy);
		newEnemy.name = "New Enemy";

		state = MergeState.MERGED;

		// Destroy both enemies
		Destroy(mergeEnemy.gameObject);
		Destroy(transform.parent.gameObject);
	}
}
