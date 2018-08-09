using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Attach this to an Actor to allow movement via execution of MoveCommand

 */

public class MoveComponent : MonoBehaviour {

	/// <summary>
	/// Movement speed
	/// </summary>
	public float moveSpeed = 5;

	protected Animator animator;

	/// <summary>
	/// Direction player is currently facing
	/// </summary>
	public BaseConstants.Direction currentDirection { get; protected set;}

	protected void Start()
	{
		animator = GetComponent<Animator>();
	}

    /// <summary>
    /// Moves/animates actor based on provided horizontal/vertical input from MoveCommand
    /// </summary>
    /// <param name="input">Player input</param>
    public virtual void ManageMovement(Vector2 input)
    {
        Debug.LogError(name + " | MoveComponent.MangeMovement() not implemented");
    }

    /// <summary>
    /// Triggers jump (if implemented in derived class)
    /// </summary>
    public virtual void Jump()
    {
        Debug.LogError(name + " | MoveComponent.Jump() not implemented");
    }
}
