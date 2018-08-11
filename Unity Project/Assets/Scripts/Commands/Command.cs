using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

All player input (behavior defined as Commands) is executed through the PlayerController. When these Commands 
are executed, the controlled Actor is passed as an argument. This allows simple commands such as movement or 
jumping to be simply executed on a variety of Actors with little to no fine-tuing.

This design is best suited for a game which features switching control between characetrs, but can also prove 
useful for programmoing behavior to be used across many different actors.

A behavior is defined by inheriting the Command base class, overriding the execute() method, and specifying a 
command Type (more on that later).

Available commands (stored in the controlled Actor's Action Queue) are executed each frame. If you only want
the behavior to trigger at a specific moment - such as when the player presses a button - those checks should 
be made within the Command definition (usually by checking the condition in Command.execute()). Additionally, 
an Actor's behaviors can be enabled/disabled by simply adding/removing the command from the Action Queue.

The command type is used for filtering out similar commands from the Actor's Action Queue; for example, if you 
want to immobilize the Actor when they're hit by a stun move, remove/disable all Commands in the Action Queue 
of type MOVEMENT (ex. JumpCommand, MoveCommand, etc).

Notes:
At the moment the implemented commands only respond to player input; a solution must be found to used them for
NPC behavior. 

Maybe these commands should be triggered by an event/listener configuration, rather than executing every frame 
and checking for condition? This sounds ideal, as the Command and Actor can both be specified in the event body. 
That way Commands can be handled the same way for the player and NPCs.

 */

public class Command : MonoBehaviour {

	// public Command() {}

    public enum Type
    {
        MOVEMENT,
        ATTACK
    }

    /// <summary>
    /// Type - used to group similar commands for filtering purposes
    /// (ex. MOVEMENT = jumping/walking commands, ATTACK = melee/projectile attack commands, etc)
    /// </summary>
    public Type type;

    /// <summary>
    /// Executes the command on the provided actor.
    /// </summary>
    /// <param name="actor">The actor.</param>
    public virtual void execute(Actor actor) {}
}
