using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Camera mainCamera;
    public ParticleSystem atkParticles;
    public float atkRange;
    public float atkSpeed;
    public float atkCooldown;
    public float atkConeAngle;
    public float atkDamage;

    float timer;
    Vector3 atkVector;
    Ray mouseRay;
    RaycastHit rayHit, lineHit;
    ParticleSystem currAtkParticles;
    int layerMask;
    List<GameObject> hitThisAttack;

    void Start()
    {
        // Ignore collisions on every layer except layer 9 (background layer)
        layerMask = 1 << 9;

        timer = atkCooldown;
        hitThisAttack = new List<GameObject>();
    }

    void Update()
    {
        // Keep timer up to date
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && timer >= atkCooldown)
        {
            // Raycast from mouse to background layer
            mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out rayHit, Mathf.Infinity, layerMask))
            {
                // Get a vector drawing from AttackRef to mouse position and then scale it to the magnitude atkRange
                atkVector = rayHit.point - gameObject.transform.position;
                atkVector.Normalize();
                atkVector *= atkRange;

                //Instantiate attack particle
                currAtkParticles = Instantiate(atkParticles, gameObject.transform, false);
                currAtkParticles.transform.Translate(atkVector.x, 1, atkVector.z);
                currAtkParticles.transform.RotateAround(gameObject.transform.position, Vector3.up, atkConeAngle / 2);
                StartCoroutine(Attack());
            }

            timer = 0;
        }
    }

    IEnumerator Attack()
    {
        for (float atkTimer = 0; atkTimer < atkSpeed; atkTimer += Time.deltaTime)
        {
            // Rotate particle
            currAtkParticles.transform.RotateAround(gameObject.transform.position, Vector3.up, -atkConeAngle / (atkSpeed / Time.deltaTime));

            // Check if enemies are hit
            Debug.DrawLine(gameObject.transform.position, currAtkParticles.transform.position);
            if (Physics.Linecast(gameObject.transform.position, currAtkParticles.transform.position, out lineHit, 1 << 8))
            {
                GameObject objectHit = lineHit.collider.gameObject;
                if (objectHit.tag.Equals("Enemy"))
                {
                    if (!hitThisAttack.Contains(objectHit))
                    {
                        objectHit.GetComponent<Actor>().TakeDamage(gameObject, atkDamage);
                        hitThisAttack.Add(objectHit);
                    }
                }
            }

            yield return null;
        }
        Destroy(currAtkParticles);
        hitThisAttack.Clear();
    }
}
