using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour
{

    /// <summary>
    /// Reference to player
    /// </summary>
    public Transform player;

    /// <summary>
    /// Offset between player and camera (saved at Start())
    /// </summary>
    private Vector3 offset_;

    /// <summary>
    /// Used to control speed of linear interpolation
    /// </summary>
    public float smoothSpeed;


    void Start()
    {
        // Save offset at game start
        offset_ = transform.position - player.position; //+ offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player) {
            // Get desired position towards player based on offset
            Vector3 newPos = player.position + offset_;
            
            // Smooth current position towards newPos using linear interpolation
            transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed);
        }
    }
}

