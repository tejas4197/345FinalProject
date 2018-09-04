using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    private Actor actor;
    private Rigidbody rbody;

    public float reboundForce;

    public float ignoreDamageTime;

    /// <summary>
    /// Ignore damage when true
    /// </summary>
    bool ignoreDamage;

    private void Start()
    {
        actor = GetComponent<Actor>();
        rbody = GetComponent<Rigidbody>();

        ignoreDamage = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!ignoreDamage && collision.gameObject.tag.Equals("Enemy"))
        {
            actor.TakeDamage(collision.gameObject, collision.gameObject.GetComponent<Actor>().meleeDamage);

            Vector3 fromEnemy = transform.position - collision.transform.position;            GameController.LogWarning("Vector magnitude: " + fromEnemy.magnitude);
            // GameController.LogWarning("Receiving force from direction " + string.Format("{0}, {1}, {2}", fromEnemy.x, fromEnemy.y, fromEnemy.z));
            rbody.AddForce(fromEnemy * reboundForce, ForceMode.Impulse);

            // Ignore damage for a moment
            StartCoroutine(IgnoreDamage(ignoreDamageTime));
        }
    }

    /// <summary>
    /// Player ignores any damage for a given number of seconds
    /// </summary>
    /// <param name="time">Duration</param>
    private IEnumerator IgnoreDamage(float time)
    {
        ignoreDamage = true;
        GameController.Log("Ignoring damage for " + time + " seconds");
        yield return new WaitForSeconds(time);
        GameController.Log("No longer ignoring damage");
        ignoreDamage = false;
    }
}
