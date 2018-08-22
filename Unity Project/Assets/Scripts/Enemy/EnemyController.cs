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
            // RED can merge with key to form value
			new Dictionary<Actor.Color, Actor.Color> {
				{Actor.Color.GREEN, Actor.Color.YELLOW},
				{Actor.Color.BLUE, Actor.Color.MAGENTA}
			} 
		},
        // GREEN can merge with key to form value
		{ Actor.Color.GREEN, 
			new Dictionary<Actor.Color, Actor.Color> {
				{Actor.Color.RED, Actor.Color.YELLOW},
				{Actor.Color.BLUE, Actor.Color.CYAN}
			} 
		},
        // BLUE can merge with key to form value
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
        GameController.Log("Getting merge result for colors (" + one.color + ", " + another.color + "): " + resultColor, GameController.LogMerge);


		// Check if valid merge before returning enemy
		if(resultColor != null) {
			return enemyVariants.Find(a => a.color == resultColor);
		}

		GameController.LogWarning(name + " | Invalid merge attempted with " + one.name + " and " + another.name, GameController.LogMerge);
		return null;
	}
}
