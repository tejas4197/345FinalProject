using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyMoveCommand : Command {

	NavMeshAgent navComponent;

	public Actor target;

	Actor player;

	public float pathUpdateMinFrequency;
	public float pathUpdateMaxFrequency;

    private void Start()
    {
		Init();
		target = player;
		float delay = Random.Range(pathUpdateMinFrequency, pathUpdateMaxFrequency);
		StartCoroutine(GoToDestination(delay));
    }

	IEnumerator GoToDestination(float delay)
	{
		yield return new WaitForSeconds(delay);

		if(!navComponent) {
			Init();
		}

		if(target) {
			navComponent.SetDestination(target.transform.position);
		}

		delay = Random.Range(pathUpdateMinFrequency, pathUpdateMaxFrequency);
		StartCoroutine(GoToDestination(delay));
	}

	void Init()
	{
		navComponent = GetComponent<NavMeshAgent>();
        player = FindObjectsOfType<Actor>().Where(a => a.isPlayer).FirstOrDefault();
	}

    // TODO: add a listener to catch an EnemyMerge event and change target to mergeable enemy

    void Update ()
	{
		// if(!navComponent) {
		// 	Init();
		// }

		// if(target) {
		// 	navComponent.SetDestination(target.transform.position);
		// }
	}
}
