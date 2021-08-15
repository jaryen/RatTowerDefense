using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTower : Tower
{
    [Header("Unity Setup Fields")]
    public Transform pivot;
    public Transform firePoint;
    public GameObject flame;

    [Header("Attributes")]
    [SerializeField] private float damage = 20f;

    // Start is called before the first frame update
    protected override void Start()
    {
        flame.SetActive(false);
    }

    protected override void Shoot()
    {
        flame.SetActive(true);

        Collider2D enemyCol = currentTarget.GetComponent<Collider2D>();
        Collider2D flameCol = flame.GetComponent<Collider2D>();

        if (flameCol.IsTouching(enemyCol))
        {
            Enemy e = enemyCol.gameObject.GetComponent<Enemy>();

            if (e)
            {
                e.TakeDamage(damage * Time.deltaTime);
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        UpdateNearestEnemy();

        if (currentTarget)
        {
            Shoot();
        }
        else
        {
            flame.SetActive(false);
        }
    }
}
