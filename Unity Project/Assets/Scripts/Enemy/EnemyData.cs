using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ScriptableObject {

	/// <summary>
	/// Dictionary of compatible colors for merging
	/// </summary>
	public Dictionary<Actor.Color, List<Actor.Color>> mergeDictionary = new Dictionary<Actor.Color, List<Actor.Color>> {
		{Actor.Color.RED, new List<Actor.Color>{Actor.Color.GREEN, Actor.Color.BLUE}},
		{Actor.Color.GREEN, new List<Actor.Color>{Actor.Color.RED, Actor.Color.BLUE}},
		{Actor.Color.BLUE, new List<Actor.Color>{Actor.Color.RED, Actor.Color.GREEN}},

		{Actor.Color.CYAN, new List<Actor.Color>{Actor.Color.MAGENTA, Actor.Color.YELLOW}},
		{Actor.Color.MAGENTA, new List<Actor.Color>{Actor.Color.CYAN, Actor.Color.YELLOW}},
		{Actor.Color.YELLOW, new List<Actor.Color>{Actor.Color.CYAN, Actor.Color.MAGENTA}},
	};

	/// <summary>
	/// Returns true if two enemies are capable of merging
	/// </summary>
	/// <param name="one"></param>
	/// <param name="another"></param>
	/// <returns></returns>
	public bool CanMerge(Actor.Color one, Actor.Color another)
	{
		if(mergeDictionary.ContainsKey(one) && mergeDictionary.ContainsKey(another))
		{
			return mergeDictionary[one].Contains(another);
		}
		// Returns false for black/white (not in dicitonary)
		return false;
	}
}
