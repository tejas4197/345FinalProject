using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {

    public float bulletDamage;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Actor actor = collision.gameObject.GetComponent<Actor>();
        if (actor && actor.isPlayer)
        {
            actor.TakeDamage(gameObject, bulletDamage);
        }
    }
}
