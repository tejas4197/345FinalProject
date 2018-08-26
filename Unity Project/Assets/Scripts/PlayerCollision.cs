using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    private Actor actor;

    private void Start()
    {
        actor = GetComponent<Actor>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            actor.TakeDamage(collision.gameObject, collision.gameObject.GetComponent<Actor>().meleeDamage);
        }
    }
}
