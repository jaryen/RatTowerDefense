using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 25f;
    [SerializeField] private float damage = 25;

    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void HitTarget()
    {
        DamageEnemy(target);
        Destroy(gameObject);
    }

    private void DamageEnemy(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        //transform.position += transform.right * bulletSpeed;
    }
}
