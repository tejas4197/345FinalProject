using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyanAttack : MonoBehaviour {

    public GameObject bullet;
    public float fireCooldown;
    public float bulletSpeed;

    private Vector3[] vectors;
    private float timer = 0;

    public void Start()
    {
        // Randomly decide if this will attack at an angle or not
        if (Random.value < 0.5f)
            vectors = new Vector3[] { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
        else
            vectors = new Vector3[] { Quaternion.AngleAxis(45, Vector3.up) * Vector3.right,
                                      Quaternion.AngleAxis(135, Vector3.up) * Vector3.right,
                                      Quaternion.AngleAxis(225, Vector3.up) * Vector3.right,
                                      Quaternion.AngleAxis(315, Vector3.up) * Vector3.right};
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireCooldown)
        {
            foreach(Vector3 v in vectors)
            {
                GameObject currBullet = Instantiate(bullet, gameObject.transform.position + v * 2, Quaternion.identity);
                currBullet.GetComponent<Rigidbody>().AddForce(v * bulletSpeed);
            }
            
            timer = 0;
        }
    }
}
