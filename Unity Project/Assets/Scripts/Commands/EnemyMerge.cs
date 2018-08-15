using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMerge : MonoBehaviour 
{
	/// <summary>
	/// Merge bounds
	/// </summary>
	SphereCollider mergeBounds;

	/// <summary>
	/// Radius required to merge
	/// </summary>
	public float mergeRadius;

	// Use this for initialization
	void Start () 
	{
		// Create child object positioned on enemy 
		GameObject child = new GameObject("Merge Bounds");
		child.transform.SetParent(transform);
		child.transform.localPosition = Vector3.zero;

		// Add merge bounds collider
		mergeBounds = child.AddComponent<SphereCollider>();
		if(!mergeBounds) {
			GameController.LogWarning("Merge collider not found for " + name + ": please add a SphereCollider to a child of this GameObject");
			return;
		}
		
		// Set radius
		mergeBounds.radius = mergeRadius;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
