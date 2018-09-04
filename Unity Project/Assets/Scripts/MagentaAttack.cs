using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagentaAttack : MonoBehaviour {

    public GameObject bullet;
    public float range;
    public float bulletSpeed;
    public float fireCooldown;

    private RaycastHit hit;
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > fireCooldown && Physics.Linecast(gameObject.transform.position, PlayerRef.Instance.transform.position, out hit) && hit.distance <= range)
        {
            Vector3 towards = PlayerRef.Instance.transform.position - gameObject.transform.position;
            towards.y = 0;
            towards.Normalize();
            GameObject currBullet = Instantiate(bullet, gameObject.transform.position + towards * 2, Quaternion.identity);
            currBullet.GetComponent<Rigidbody>().AddForce(towards * bulletSpeed);

            timer = 0;

            // Play sound effect
            AudioController.Instance.PlaySoundEffect(AudioController.SoundType.ENEMY_PROJECTILE);
        }
    }
}
