using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    [Header("Unity Setup Fields")]
    public Transform firePoint;
    public GameObject bullet;
    public Animator shootAnimController;

    protected override void Shoot()
    {
        shootAnimController.SetTrigger("shoot");
        GameObject newBullet = Instantiate(bullet, firePoint.position, transform.rotation);
        newBullet.transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, -90));
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        if (bulletScript)
        {
            bulletScript.Seek(currentTarget.transform);
        }
    }

    protected override void Update()
    {
        UpdateNearestEnemy();
       
        // Checks if the tower shoot cooldown
        // is ready.
        if (Time.time > nextTimeToShoot)
        {
            if (currentTarget)
            {
                Shoot();
                nextTimeToShoot = Time.time + timeBetweenShots;
            }
        }
    }
}
