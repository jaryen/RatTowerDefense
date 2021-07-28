using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    [SerializeField] private float damage = 30;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Enemy e = col.gameObject.GetComponent<Enemy>();

            if (e)
            {
                e.TakeDamage(damage);
            }
        }
    }
}
