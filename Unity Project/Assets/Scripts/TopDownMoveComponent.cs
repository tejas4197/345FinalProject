using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMoveComponent : MoveComponent {

    Rigidbody rbody;
    
	// Use this for initialization
	void Start () {
        base.Start();
        rbody = GetComponent<Rigidbody>();
	}

    /// <summary>
    /// Moves/animates actor based on provided horizontal/vertical input from MoveCommand
    /// </summary>
    /// <param name="input">Player input</param>
    void Update()
    {
        // Get player input
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        // Move actor in direction of input
        Vector3 movement = input * moveSpeed;
        rbody.velocity = movement;
    }
}
