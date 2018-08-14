using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Camera mainCamera;
    public ParticleSystem atkParticles;
    public float atkRange;
    public float atkSpeed;
    public float atkCooldown;
    public float atkConeAngle;

    float timer;
    Vector3 atkVector;
    Ray mouseRay;
    RaycastHit hit;
    ParticleSystem currAtkParticles;
    int layerMask;

	void Start ()
    {
        // Ignore collisions on every layer except layer 9 (background layer)
        layerMask = 1 << 9;
        timer = atkCooldown;
	}
	
	void Update ()
    {
        // Keep timer up to date
        timer += Time.deltaTime;

	    if (Input.GetKey(KeyCode.Mouse0) && timer >= atkCooldown)
        {
            // Raycast from mouse to background layer
            mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, layerMask))
            {
                // Get a vector drawing from AttackRef to mouse position and then scale it to the magnitude atkRange
                atkVector = hit.point - gameObject.transform.position;
                atkVector.Normalize();
                atkVector *= atkRange;
                Debug.DrawRay(gameObject.transform.position, atkVector, Color.red);

                //Instantiate attack particle
                currAtkParticles = Instantiate(atkParticles, gameObject.transform, false);
                currAtkParticles.transform.Translate(atkVector.x, 1, atkVector.z);
                currAtkParticles.transform.RotateAround(gameObject.transform.position, Vector3.up, -(atkConeAngle / 2));
                StartCoroutine(Attack());
            }

            timer = 0;
        }
	}

    IEnumerator Attack()
    {
        for (float atkTimer = 0; atkTimer < atkSpeed; atkTimer += Time.deltaTime)
        {
            currAtkParticles.transform.RotateAround(gameObject.transform.position, Vector3.up, atkConeAngle / (atkSpeed / Time.deltaTime));
            yield return null;
        }
        Destroy(currAtkParticles);
    }
}
