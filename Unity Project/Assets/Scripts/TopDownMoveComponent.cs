using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMoveComponent : MoveComponent {

    Rigidbody2D rbody;
    
	// Use this for initialization
	void Start () {
        base.Start();
        rbody = GetComponent<Rigidbody2D>();
	}

    /// <summary>
    /// Moves/animates actor based on provided horizontal/vertical input from MoveCommand
    /// </summary>
    /// <param name="input">Player input</param>
    override
    public void ManageMovement(Vector2 input)
    {
        // Move actor in direction of input
        Vector2 movement = input * moveSpeed;
        rbody.velocity = movement;
        
        // Calculate new direction if we're moving
        bool moving = input != Vector2.zero;
        if(moving) {
            currentDirection = FaceDirection(input);
        }

        // Set triggers on animator
        if(animator) {
            AnimateWalk(input, moving);
        }
    }

    /// <summary>
    /// Animates movement based on horizontal/vertical input
    /// </summary>
    /// <param name="input">Player input</param>
    /// <param name="moving">True if player is moving</param>
    protected void AnimateWalk(Vector2 input, bool moving) 
    {
        // Tell animator if we're moving
        animator.SetBool("Moving", moving);

        // Set animator direction if moving
        if(moving) {
            animator.SetInteger("Direction", (int)currentDirection);
        }
        
    }

    /// <summary>
    /// Calculates direction for player to face based on horizontal/vertical input
    /// </summary>
    /// <param name="horizontal">Horizontal input</param>
    /// <param name="vertical">Vertical input</param>
    /// <returns>Direction to face</returns>
    protected BaseConstants.Direction FaceDirection(Vector2 input)
    {
        // If moving diagonally, continue facing along axis of currentDirection
        if(input.x != 0 && input.y != 0){
            if(DirectionHelper.IsVertical(currentDirection)) {
                return ParseInput(input.y, false);
            }
            else {
                return ParseInput(input.x, true);
            }
        }

        // Otherwise face in direction of raw input
        else if(input.x != 0) {
            return ParseInput(input.x, true);
        }
        else if(input.y != 0) {
            return ParseInput(input.y, false);
        }

        return currentDirection;
    }

    /// <summary>
    /// Parses a float representing player input (horizontal or vertical axis) and returns corresponding direction
    /// </summary>
    /// <param name="input">Player input on horizontal/vertical axis</param>
    /// <param name="horizontal">True if input direction is horizontal, false if vertical</param>
    protected BaseConstants.Direction ParseInput(float input, bool horizontal)
    {
        if (input > 0) {
            return horizontal ? BaseConstants.Direction.Right : BaseConstants.Direction.Up;
        }
        else {
            return horizontal ? BaseConstants.Direction.Left : BaseConstants.Direction.Down;
        }
    }
}
