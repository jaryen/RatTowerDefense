using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float damage = 30;

    public void DamageEnemy(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e)
        {
            e.TakeDamage(damage * Time.deltaTime);
        }
    }
}
