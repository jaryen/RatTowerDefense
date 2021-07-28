using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    [SerializeField] private float damage = 30;
    [SerializeField] private float poisonDamage = 3f;
    [SerializeField] private float poisonDuration = 3f;
    [SerializeField] private float poisonRate = 1f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Enemy e = col.gameObject.GetComponent<Enemy>();

            if (e)
            {
                e.TakeDamage(damage);
                StartCoroutine(PoisonEffect(e));
            }
        }
    }

    private IEnumerator PoisonEffect(Enemy enemy)
    {
        if (enemy)
        {
            SpriteRenderer enemySR = enemy.gameObject.GetComponent<SpriteRenderer>();
            enemySR.color = Color.green;

            for (float i = 0; i < poisonDuration; i += poisonRate)
            {
                yield return new WaitForSeconds(poisonRate);
                if (enemy)
                {
                    enemy.TakeDamage(poisonDamage);
                }
                
                //Debug.Log("Took " + poisonDamage + " damage");
            }

            if (enemy)
            {
                enemySR.color = Color.white;
            }
        }
        else
        {
            yield return null;
        }
    }
}
