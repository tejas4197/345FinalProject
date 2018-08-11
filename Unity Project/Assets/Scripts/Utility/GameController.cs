using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// Physics logs will display in console if true
    /// </summary>
	public bool logPhysics;
    
    /// <summary>
    /// Command logs will display in console if true
    /// </summary>
    public bool logCommand;

	void Start()
	{
        // Add LogHelper mapping here for each logger
        loggers = new Dictionary<LogHelper, bool>() {
            { LogPhysics, logPhysics },
            { LogCommands, logCommand }
        };

        // Initialize each logger
        InitializeLoggers();

        // Player Controller
        PlayerController = GetComponent<PlayerController>();
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
	public static LogHelper LogPhysics = new LogHelper();

    // Ideally this should allow filtering by Command and Actor executing it
    /// <summary>
    /// Command logger
    /// </summary>
    public static LogHelper LogCommands = new LogHelper();

    /// <summary>
    /// Player controller
    /// </summary>
    public static PlayerController PlayerController { get; private set; }

}
