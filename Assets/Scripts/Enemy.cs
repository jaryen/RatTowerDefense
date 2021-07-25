using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public MoneyManager moneyManager;
    public HealthManager healthManager;

    [Header("Attributes")]
    [SerializeField] public float enemyHealth;
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private int killReward = 0; // Amount of money player gains when enemy killed
    [SerializeField] private int damage = 0; // The amount of damage enemy does when it reaches end

    private GameObject targetTile;
    private Vector3 previousPos;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
    }

    private void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        healthManager = FindObjectOfType<HealthManager>();

        previousPos = transform.position;
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            Die();
            moneyManager.AddMoney(killReward); // Give money to player
        }
    }

    private void Die()
    {
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, movementSpeed * Time.deltaTime);

        // Set current direction
        Vector3 currentPos = transform.position;

        // Calculate direction
        Vector3 currDir = (currentPos - previousPos).normalized;

        // Set direction of the enemy
        if (currDir == new Vector3(1,0,0)) // facing right
        {
            transform.rotation = Quaternion.Euler(0,0,90);
        }
        else if (currDir == new Vector3(-1,0,0)) // facing left
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (currDir == new Vector3(0,-1,0)) // facing down
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        previousPos = currentPos;
    }

    private void CheckPosition()
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
    private bool EnemyReachedEnd()
    {
        if (transform.position == MapGenerator.endTile.transform.position)
        {
            Die();
            return true;
        }

        return false;
    }

    private void Update()
    {
        CheckPosition();
        MoveEnemy();

        if (EnemyReachedEnd())
        {
            healthManager.takeDamage(damage);
        }
    }
}
