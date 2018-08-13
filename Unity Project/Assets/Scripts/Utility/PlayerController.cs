﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/*

Place this on a GameObject in the scene to allow player control

All player input (behavior defined as Commands) is executed through the PlayerController. When these Commands 
are executed, the controlled Actor is passed as an argument. This allows simple commands such as movement or 
jumping to be simply executed on a variety of Actors with little to no fine-tuning.

Each available command (stored in the controlled Actor's Action Queue) is executed each frame. If you only want
the behavior to trigger at a specific moment - such as when the player presses a button - those checks should be
made within the Command definition (usually in Command.execute()).

This design is best suited for a game which features switching control between characetrs, but can also prove 
useful for programmoing behavior to be used across many different actors.

If no actor is assigned at Start(), it searches the scene for an actor with isPlayer = true

 */

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Actor player is currently controlling
    /// </summary>
    public Actor controlledActor;

    void Start()
    {
        // Set actor to player if null
        if(controlledActor == null)
        {
            SetActor(FindPlayer());
        }
    }

    /// <summary>
    /// Returns reference to Player actor in scene (assumes isPlayer = true)
    /// </summary>
    public Actor FindPlayer()
    {
        // TODO: check if player actor not found
        return FindObjectsOfType<Actor>().Where(i => i.isPlayer).First() as Actor;
    }

    /// <summary>
    /// Returns GameObject reference to Player actor in scene.
    /// </summary>
    /// <returns></returns>
    public GameObject FindPlayerAsGameObject()
    {
        return GameObject.FindWithTag("Player");
    }

    void Update () 
    {
        List<Actor> actors = FindObjectsOfType<Actor>().ToList();
        
        foreach(Actor actor in actors) {
            ExecuteCommands(actor);
        }
    }


    /// <summary>
    /// Performs all commands in the actor's commands list
    /// </summary>
    /// <param name="actor">The actor.</param>
    private void ExecuteCommands(Actor actor)
    {
        // Execute each command in actor's command list
        foreach(Command command in actor.commands) {
            switch (command.type) {

                // Commands performed without a target
                case Command.Type.SELF:
                    command.execute(actor);
                    break;

                // Commands performed on some target
                case Command.Type.TARGET:
                    command.execute(actor);
                    break;
            }
        }
    }

    /// <summary>
    /// Sets the actor controlled by player input
    /// </summary>
    /// <param name="actor">Actor</param>
    public void SetActor(Actor actor)
    {
        this.controlledActor = actor;
        GameController.Log("Changed actor to " + actor.name);
    }

}