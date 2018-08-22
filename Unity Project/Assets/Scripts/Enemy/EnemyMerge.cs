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
	public Actor mergeEnemy;

	/// <summary>
	/// Reference to this enemy's Actor component (located in parent object)
	/// </summary>
	public Actor thisEnemy;

	/// <summary>
	/// Distance between enemies before they merge
	/// </summary>
	public float mergeDistance;

	EnemyController enemyController;

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

		// Get reference to actor component
		thisEnemy = GetComponentInParent<Actor>();

		// Set position to parent
		transform.localPosition = Vector3.zero;
	}
	
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		// Check if collided with actor
		Actor actor = other.gameObject.GetComponentInParent<Actor>();
		if(!actor) {
			return;
		}

		// Check if within merge radius of enemy
		if(!actor.isPlayer) {
			if(!thisEnemy) {
				Debug.LogWarning("ERROR: Reference to actor on " + transform.parent.name + " not found");
			}
			Debug.Log(thisEnemy.name + "(Color " + thisEnemy.color + ") in merge radius of actor: " + actor.name + "(Color " + actor.color + ")");

			// Check if can merge with enemy
			if(EnemyController.MergeCompatible(thisEnemy, actor) && !state.Equals(MergeState.MERGING)) {
				mergeEnemy = actor;

				Debug.Log(transform.parent.name + " moving towards " + mergeEnemy.name);
				InvokeRepeating("TryMerge", Time.deltaTime, Time.deltaTime);
				
				// Set merge state
				state = MergeState.MERGING;
			}
		}
	}

	/// <summary>
	/// Move towards enemy until within mergeDistance
	/// (called via InvokeRepeating)
	/// </summary>
	void TryMerge()
	{
		// Check if reference to enemy is null
		if(!mergeEnemy) {
			GameController.LogWarning(transform.parent.name + " - merge target not found, canceling merge");

			// Revert to merge-seeking state
			CancelInvoke();
			state = MergeState.NOT_MERGING;
			return;
		}
		// Move towards enemy
		mergeEnemy.GetComponent<NavMeshAgent>().SetDestination(transform.parent.position);

		if(WithinRange()) {
			Debug.Log(transform.parent.name + " ready to merge with " + mergeEnemy.name);

			// Merge with enemy if they haven't merged with us yet
			if(CanMerge(mergeEnemy.GetComponentInChildren<EnemyMerge>())) {
				Merge();
			}
			
		}
	}

	/// <summary>
	/// Returns true if enemy hasn't merged yet
	/// </summary>
	/// <param name="enemy">Enemy's EnemyMerge component</param>
	/// <returns></returns>
	bool CanMerge(EnemyMerge enemy) {
		return !enemy.state.Equals(MergeState.MERGED);
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

		Actor newEnemy = thisEnemy.enemyController.Merge(thisEnemy, mergeEnemy);

		if(!newEnemy) {
			Debug.LogWarning(transform.parent.name + " - enemy merge could not be resolved; resulting enemy not determined");
			return;
		}
		
		Debug.Log(name + " | spawning " + newEnemy.color + " enemy");

		// Spawn new enemy at current location
		newEnemy.transform.position = mergeEnemy.transform.position;
		newEnemy = Instantiate(newEnemy);

		// Set our state to merged
		state = MergeState.MERGED;

		Debug.Log("Spawned " + newEnemy.color + " enemy");

		// Cancel invoked methods on self and other enemy before destroying
		CancelInvoke();
		mergeEnemy.GetComponentInChildren<EnemyMerge>().CancelInvoke();

		// Destroy self and other enemy
		Destroy(mergeEnemy.gameObject);
		Destroy(transform.parent.gameObject);
	}
}
