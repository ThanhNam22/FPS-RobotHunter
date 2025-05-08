using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Camera cam;
    public float bulletVelocity = 30f;
    public float bulletLifeTime = 3f;
    public float fireRate = 0.1f;
    public ParticleSystem muzzleFlash;
    public Animator animator;
    private float nextTimeToFire = 0f;
    public bool canShoot = false;
    private void Update()
    {
        if (canShoot && Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
        animator.SetTrigger("GUN");
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            targetPoint = hit.point;
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        else
        {
            targetPoint = cam.transform.position + cam.transform.forward * range;
        }

        Vector3 shootDirection = (targetPoint - bulletSpawn.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.useGravity = false;
        bulletRb.velocity = shootDirection * bulletVelocity;

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

}
