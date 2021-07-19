using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public MoneyManager moneyManager;
    public HealthManager healthManager;

    [Header("Attributes")]
    [SerializeField] private float enemyHealth;
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private int killReward = 0; // Amount of money player gains when enemy killed
    [SerializeField] private int damage = 0; // The amount of damage enemy does when it reaches end

    private GameObject targetTile;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
    }

    private void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        healthManager = FindObjectOfType<HealthManager>();
        initializeEnemy();
    }

    private void initializeEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            die();
            moneyManager.AddMoney(killReward); // Give money to player
        }
    }

    private void die()
    {
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }

    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, movementSpeed * Time.deltaTime);
    }

    private void checkPosition()
    {
        if (targetTile != null && targetTile != MapGenerator.endTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;

            if (distance < 0.001f)
            {
                int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile);

                targetTile = MapGenerator.pathTiles[currentIndex + 1];
            }
        }
    }

    // destroy the enemy and return true
    // when reached end of track
    private bool enemyReachedEnd()
    {
        if (transform.position == MapGenerator.endTile.transform.position)
        {
            die();
            return true;
        }

        return false;
    }

    private void Update()
    {
        checkPosition();
        moveEnemy();

        if (enemyReachedEnd())
        {
            healthManager.takeDamage(damage);
        }
    }
}
