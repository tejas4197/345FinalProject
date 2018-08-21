using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyData {

	/// <summary>
	/// Dictionary of compatible colors for merging
	/// </summary>
	public static Dictionary<Actor.Color, List<Actor.Color>> mergeCompatible = new Dictionary<Actor.Color, List<Actor.Color>> {
		{ Actor.Color.RED, new List<Actor.Color>{Actor.Color.GREEN, Actor.Color.BLUE} },
		{ Actor.Color.GREEN, new List<Actor.Color>{Actor.Color.RED, Actor.Color.BLUE} },
		{ Actor.Color.BLUE, new List<Actor.Color>{Actor.Color.RED, Actor.Color.GREEN} },

		{ Actor.Color.CYAN, new List<Actor.Color>{Actor.Color.MAGENTA, Actor.Color.YELLOW} },
		{ Actor.Color.MAGENTA, new List<Actor.Color>{Actor.Color.CYAN, Actor.Color.YELLOW} },
		{ Actor.Color.YELLOW, new List<Actor.Color>{Actor.Color.CYAN, Actor.Color.MAGENTA} },
	};

	public static Dictionary<List<Actor.Color>, Actor.Color> mergeResult = new Dictionary<List<Actor.Color>, Actor.Color> {
		{ new List<Actor.Color>{Actor.Color.RED, Actor.Color.GREEN}, Actor.Color.YELLOW },
		{ new List<Actor.Color>{Actor.Color.RED, Actor.Color.BLUE}, Actor.Color.MAGENTA },
		{ new List<Actor.Color>{Actor.Color.GREEN, Actor.Color.BLUE}, Actor.Color.CYAN },

		{ new List<Actor.Color>{Actor.Color.CYAN, Actor.Color.MAGENTA, Actor.Color.YELLOW}, Actor.Color.WHITE },
	};

	/// <summary>
	/// Returns true if two enemies are capable of merging
	/// </summary>
	/// <param name="one"></param>
	/// <param name="another"></param>
	/// <returns></returns>
	public static bool MergeCompatible(Actor.Color one, Actor.Color another)
	{
		if(mergeCompatible.ContainsKey(one) && mergeCompatible.ContainsKey(another))
		{
			return mergeCompatible[one].Contains(another);
		}
		// Returns false for black/white (not in dicitonary)
		return false;
	}

	public static Actor.Color? MergeResult(Actor.Color one, Actor.Color another)
	{
		if(MergeCompatible(one, another)) {
			return mergeResult[new List<Actor.Color>{one, another}];
		}
		return null;
	}
}
