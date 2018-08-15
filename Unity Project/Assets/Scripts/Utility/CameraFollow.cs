using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset_;


    void Start()
    {
        offset_ = transform.position - player.transform.position; //+ offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset_;
    }
}

