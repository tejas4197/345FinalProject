using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Used for logging information to the console

A LogHelper should be created for a number of logging purposes (ex. Physics, Environment, Actor, Command, etc.)
so that they can be enabled/disabled; this allows a developer to filter out unrelated logs while debugging.

 */

public class LogHelper {

	/// <summary>
	/// True if logging is enabled
	/// </summary>
	private bool enabled;

	public void Log(string log)
	{
		if(enabled) {
			Debug.Log(log);
		}
	}

	public void LogWarning(string log)
	{
		if(enabled) {
			Debug.LogWarning(log);
		}
	}
	
	/// <summary>
	/// Enable/disable logging
	/// </summary>
	/// <param name="enabled">If true, logging is enabled</param>
	public void SetLogging(bool enabled)
	{
		this.enabled = enabled;
	}
}
