using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    [Header("Unity Setup Fields")]
    public Transform pivot;
    public Transform barrel;
    public GameObject bullet;

    protected override void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        if (bulletScript)
        {
            bulletScript.Seek(currentTarget.transform);
        }
    }
}
