using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset_;


    void Start()
    {
        // Save offset at game start
        offset_ = transform.position - player.transform.position; //+ offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Adjust camera towards player based on offset
        transform.position = player.transform.position + offset_;
    }
}

