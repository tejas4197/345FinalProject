using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMoveComponent : MoveComponent {

    public float dashForce;
    public float dashCooldown;
    public float dashDuration;

    Rigidbody rbody;
    float timer;

    // Use this for initialization
    new void Start ()
    {
        base.Start();
        rbody = GetComponent<Rigidbody>();
        timer = dashCooldown;
	}

    /// <summary>
    /// Moves/animates actor based on provided horizontal/vertical input from MoveCommand
    /// </summary>
    /// <param name="input">Player input</param>
    void Update()
    {
        // Keep timer up to date
        timer += Time.deltaTime;

        // Check if player is starting dash
        if (Input.GetKeyDown(KeyCode.Space) && timer >= dashCooldown)
        {
            timer = 0;
        }

        // Get player input
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1);
        
        // Move actor in direction of input
        Vector3 movement = input * moveSpeed;

        rbody.velocity = movement;

        // If player is dashing
        if (timer < dashDuration)
        {
            // Multiply velocity by a value that decreases as the dash goes on
            float inverseTimer = Mathf.Abs(timer - dashDuration);
            float velocityScalar = Mathf.Clamp(dashForce * inverseTimer, 1, Mathf.Infinity);
            rbody.velocity *= velocityScalar;
        }
    }
}
