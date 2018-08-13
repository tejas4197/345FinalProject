using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveCommand : Command {

	/// <summary>
	/// Target of movement
	/// </summary>
	Transform target;

	NavMeshAgent navComponent;

	public Actor player;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		navComponent = GetComponent<NavMeshAgent>();
		target = player.transform;
	}

	void FixedUpdate()
	{
		if(target) {
			navComponent.SetDestination(target.position);
		}
	}
	
	public override void execute(Actor actor, GameObject target)
	{
		if(!navComponent) {
			navComponent = GetComponent<NavMeshAgent>();
		}

		navComponent.SetDestination(actor.transform.position);

		// navComponent.SetDestination(target.position);
	}
}
