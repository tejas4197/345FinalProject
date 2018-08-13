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
	
	public override void execute(Actor actor, GameObject target)
	{
        GameController.Log(actor.name + " moving towards " + target.name);

		if(!navComponent) {
			navComponent = actor.GetComponent<NavMeshAgent>();
		}

		navComponent.SetDestination(target.transform.position);

		// navComponent.SetDestination(target.position);
	}
}
