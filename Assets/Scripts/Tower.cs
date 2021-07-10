using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range = 0;
    [SerializeField] private float damage = 0;
    [SerializeField] private float timeBetweenShots = 0; // Time in seconds between shots
    private float nextTimeToShoot;

    public GameObject currentTarget;

    private void Start()
    {
        nextTimeToShoot = Time.time;
    }

    // Gets the current nearest enemy to tower
    private void updateNearestEnemy()
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
    protected virtual void shoot()
    {
        // Get a game object of type Enemy
        Enemy enemyScript = currentTarget.GetComponent<Enemy>();
        enemyScript.takeDamage(damage);
    }

    private void Update()
    {
        updateNearestEnemy();

        // Checks if the tower shoot cooldown
        // is ready.
        if (Time.time > nextTimeToShoot)
        {
            if (currentTarget)
            {
                // Debug.Log("You have reached shoot");
                shoot();
                nextTimeToShoot = Time.time + timeBetweenShots;
            }
        }
    }
}
