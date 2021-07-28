using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;

    [SerializeField] public int startingHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        //Debug.Log("You lost " + amount + " health!" + " Health: " + currentHealth);
        //
        //if (currentHealth <= 0)
        //{
        //    healthGone();
        //}
    }

    public void healthGone()
    {
        gameOverScreen.Setup();
    }
}
