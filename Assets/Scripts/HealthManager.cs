using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    Enemy enemy;

    public int startingHealth;
    private int currentHealth;

    public void Start()
    {
        enemy = FindObjectOfType(typeof(Enemies)) as Enemy;
        currentHealth = startingHealth;
    }

    private void takeDamage()
    {
        currentHealth--;
    }

    public void Update()
    {
        if (enemy.enemyReachedEnd())
        {
            takeDamage();
        }
    }
}
