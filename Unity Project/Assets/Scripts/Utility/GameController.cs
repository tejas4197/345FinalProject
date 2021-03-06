﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*

GameController

This should always be present in the scene, ideally attached to a GameObject that won't be destroyed between
scenes (but I haven't tested functionality between scenes). 

The GameController is a static class that can be used to access the PlayerController (and through it the player 
actor/controlled actor), LogHelpers, and anything else that should be globally available. 

The LogHelpers (LogPhysics, LogCommands, etc) are called from other classes to log specific information to the
console. The associated boolean values (logPhysics, logCommand, etc) can be enabled/disabled to show/hide 
messages logged through that logger; this allows developers to filter out unrelated log messages when debugging.

 */

public class GameController : MonoBehaviour {
	[Header("Loggers")]
    /// <summary>
    /// Color logs will display in console if true
    /// </summary>
	public bool logColor;
    
    /// <summary>
    /// Merge logs will display in console if true
    /// </summary>
    public bool logMerge;

    /// <summary>
    /// Spawner logs will display in console if true
    /// </summary>
    public bool logSpawners;

    public NavMeshSurface navSurface;

	void Start()
	{
        // Add LogHelper mapping here for each logger
        loggers = new Dictionary<LogHelper, bool>() {
            { LogColor, logColor },
            { LogMerge, logMerge },
            { LogSpawner, logSpawners }
        };

        // Initialize each logger
        InitializeLoggers();

        // Build NavMeshSurface
        navSurface.BuildNavMesh();
	}

    /// <summary>
    /// Enables/disables logging foe each LogHelper, based on the mapped boolean value
    /// (ex { LogPhysics -> logPhysics })
    /// </summary>
	private void InitializeLoggers()
	{
        foreach(LogHelper logHelper in loggers.Keys) {
            logHelper.SetLogging(loggers[logHelper]);
        }
	}

    /// <summary>
    /// Log a message to the console (with option to filter through specified GameController.LogHelper)
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="logHelper">LogHelper to use (must be referenced via GameController)</param>
    public static void Log(string message, LogHelper logHelper = null)
    {
        // Check if LogHelper specified
        if(logHelper != null) {
            logHelper.Log(message);
        }
        // Otherwise just log without filtering
        else {
            Debug.Log(message);
        }
    }

    /// <summary>
    /// Log a warning to the console (with option to filter through specified GameController.LogHelper)
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="logHelper">LogHelper to use (must be referenced via GameController)</param>
    public static void LogWarning(string message, LogHelper logHelper = null)
    {
        // Check if LogHelper specified
        if(logHelper != null) {
            logHelper.LogWarning(message);
        }
        // Otherwise just log without filtering
        else {
            Debug.LogWarning(message);
        }
    }

    protected Dictionary<LogHelper, bool> loggers;

	/// <summary>
	/// Physics logger
	/// </summary>
	public static LogHelper LogColor = new LogHelper();

    /// <summary>
    /// Merge logger
    /// </summary>
    public static LogHelper LogMerge = new LogHelper();

    /// <summary>
    /// Spawner logger
    /// </summary>
    public static LogHelper LogSpawner = new LogHelper();

}
