using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTower : Tower
{
    [Header("Attributes")]
    public Vector3 maxRadius;

    [Header("Unity Setup Fields")]
    public GameObject gasCircle;
    public float expandRate;

    protected override void Start()
    {
        base.Start();

        // Add a parent object to gas game object
        // Helps scale gas correctly
        GameObject gasParent = new GameObject();
        gasParent.transform.parent = gameObject.transform;
        gasCircle.transform.parent = gasParent.transform;

        gasCircle.transform.localScale = new Vector3(0, 0, 0);
        maxRadius = new Vector3(range*2, range*2);
    }

    private IEnumerator ExpandGas(float expandRate)
    {
        while (gasCircle.transform.localScale.magnitude < maxRadius.magnitude)
        {
            if (gasCircle.transform.localScale.magnitude + expandRate > maxRadius.magnitude)
            {
                gasCircle.transform.localScale += 
                    new Vector3(maxRadius.magnitude - gasCircle.transform.localScale.magnitude,
                    maxRadius.magnitude - gasCircle.transform.localScale.magnitude);
            }
            else
            {
                gasCircle.transform.localScale += new Vector3(expandRate, expandRate);
            }

            yield return null;
        }

        //Debug.Log("Gas Radius: " + gasCircle.transform.localScale);
        gasCircle.transform.localScale = new Vector3(0, 0, 0);
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
                StartCoroutine(ExpandGas(expandRate));
                nextTimeToShoot = Time.time + timeBetweenShots;
            }
        }
    }
}
