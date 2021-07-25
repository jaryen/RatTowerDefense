using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    [Header("Unity Setup Fields")]
    public Transform pivot;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public Laser laser;

    protected override void Shoot()
    {
        laser.DamageEnemy(currentTarget.transform);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, currentTarget.transform.position);
        lineRenderer.useWorldSpace = true;
    }

    protected override void Update()
    {
        base.Update();
        if (!currentTarget)
        {
            lineRenderer.enabled = false;
        }
    }
}
