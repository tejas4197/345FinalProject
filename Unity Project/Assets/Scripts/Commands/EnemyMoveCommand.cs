using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyMoveCommand : Command {

	NavMeshAgent navComponent;

	public Actor target;

    private void Start()
    {
        target = FindObjectsOfType<Actor>().Where(a => a.isPlayer).First();
    }

    // TODO: add a listener to catch an EnemyMerge event and change target to mergeable enemy

    void Update ()
	{
        GameController.Log(name + " moving towards " + target.name);

		if(!navComponent) {
			navComponent = GetComponent<NavMeshAgent>();
		}

		navComponent.SetDestination(target.transform.position);
	}
}
