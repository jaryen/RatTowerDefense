using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    [Header("Unity Setup Fields")]
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public Laser laser;
    public AudioSource laserAudio;

    protected override void Shoot()
    {
        laser.DamageEnemy(currentTarget.transform);
        laserAudio.Play();

        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, firePoint.transform.position);
        lineRenderer.SetPosition(1, currentTarget.transform.position);
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
