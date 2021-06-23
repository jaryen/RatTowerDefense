using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 0.5f;

    private void Start()
    {
        Destroy(gameObject, 10f); // Destroys bullet 10 sec after instantiation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position += transform.right * bulletSpeed;
    }
}
