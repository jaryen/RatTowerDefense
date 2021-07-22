using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    [Header("Unity Setup Fields")]
    public Transform pivot;
    public Transform barrel;
    public LineRenderer lineRenderer;

    protected override void Shoot()
    {
        lineRenderer.SetPosition(0, barrel.position);
        lineRenderer.SetPosition(1, currentTarget.transform.position);
    }
}
