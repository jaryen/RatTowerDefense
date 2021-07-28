using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] protected float range = 0;
    [SerializeField] protected float timeBetweenShots = 0; // Time in seconds between shots
    protected float nextTimeToShoot;

    public GameObject currentTarget;

    protected virtual void Start()
    {
        nextTimeToShoot = Time.time;
    }

    // Gets the current nearest enemy to tower
    protected void UpdateNearestEnemy()
    {
        GameObject currentNearestEnemy = null;
        float distance = Mathf.Infinity;

        // Gets distance from tower's position to each enemy in play
        foreach (GameObject enemy in Enemies.enemies)
        {
            float _distance = (transform.position - enemy.transform.position).magnitude;

            // Checks if the current distance is less than the 
            // shortest distance.
            if (_distance < distance)
            {
                distance = _distance;
                currentNearestEnemy = enemy;
            }
        }

        if (distance <= range)
        {
            currentTarget = currentNearestEnemy;
        }
        else
        {
            currentTarget = null;
        }
    }

    // Protected: any class that derives from Tower can use
    // this function
    protected virtual void Shoot()
    {
        // Shoot
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    protected virtual void Update()
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
