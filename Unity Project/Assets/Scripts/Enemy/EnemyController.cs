using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour 
{
	/// <summary>
	/// List of enemy prefabs
	/// </summary>
	public List<Actor> enemyVariants;
	
	public static Dictionary<Actor.Color, Dictionary<Actor.Color, Actor.Color>> mergeDict = new Dictionary<Actor.Color, Dictionary<Actor.Color, Actor.Color>> {
		{ Actor.Color.RED, 
			new Dictionary<Actor.Color, Actor.Color> {
				{Actor.Color.GREEN, Actor.Color.YELLOW},
				{Actor.Color.BLUE, Actor.Color.MAGENTA}
			} 
		},
		{ Actor.Color.GREEN, 
			new Dictionary<Actor.Color, Actor.Color> {
				{Actor.Color.RED, Actor.Color.YELLOW},
				{Actor.Color.BLUE, Actor.Color.CYAN}
			} 
		},
		{ Actor.Color.BLUE, 
			new Dictionary<Actor.Color, Actor.Color> {
				{Actor.Color.GREEN, Actor.Color.CYAN},
				{Actor.Color.RED, Actor.Color.MAGENTA}
			} 
		}
	};

	/// <summary>
	/// Returns true if two enemies are capable of merging
	/// </summary>
	/// <param name="one"></param>
	/// <param name="another"></param>
	/// <returns></returns>
	public static bool MergeCompatible(Actor one, Actor another)
	{
		if(mergeDict.ContainsKey(one.color))
		{
			return mergeDict[one.color].ContainsKey(another.color);
		}
		// Returns false for black/white (not in dicitonary)
		return false;
	}

	public static Actor.Color? MergeResult(Actor one, Actor another)
	{
		if(MergeCompatible(one, another)) {
			return mergeDict[one.color][another.color];

		}
		return null;
	}

	public Actor Merge(Actor one, Actor another)
	{
		// Get merge result
		Actor.Color? resultColor = MergeResult(one, another);
		Debug.Log("Getting merge result for colors (" + one.color + ", " + another.color + "): " + resultColor);


		// Check if valid merge before returning enemy
		if(resultColor != null) {
			return enemyVariants.Find(a => a.color == resultColor);
		}

		Debug.LogWarning(name + " | Invalid merge attempted with " + one.name + " and " + another.name);
		return null;
	}
}
