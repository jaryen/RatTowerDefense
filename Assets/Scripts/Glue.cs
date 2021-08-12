using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float glueSpeed = 0.1f;
    [SerializeField] private float totalOffset = 0.25f;
    [SerializeField] private float glueDuration = 2f;

    private Transform targetTile;
    private Vector3 targetPos;

    private float randXOffsetMin;
    private float randYOffsetMin;
    private float randXOffsetMax;
    private float randYOffsetMax;

    private float randXOffset;
    private float randYOffset;

    // Start is called before the first frame update
    void Start()
    {
        randXOffsetMin = randYOffsetMin = -totalOffset;
        randXOffsetMax = randYOffsetMax = totalOffset;

        randXOffset = Random.Range(randXOffsetMin, randXOffsetMax);
        randYOffset = Random.Range(randYOffsetMin, randYOffsetMax);
    }

    public void GetTargetTile(Transform tile)
    {
        targetTile = tile;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Enemy e = col.gameObject.GetComponent<Enemy>();
            
            if (e)
            {
                if (!e.isGlued)
                {
                    gameObject.SetActive(false);

                    e.isGlued = true;
                    float origSpeed = e.GetSpeed();
                    e.SetSpeed(origSpeed / 2);

                    e.StartCoroutine(SlowdownEffect(origSpeed, e));
                    Destroy(gameObject);
                }
            }
        }
    }

    private IEnumerator SlowdownEffect(float originalSpeed, Enemy e)
    {
        yield return new WaitForSeconds(glueDuration);

        e.isGlued = false;
        e.SetSpeed(originalSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetTile)
        {
            return;
        }

        Vector3 targetPos = targetTile.transform.position;

        float randX = targetPos.x + randXOffset;
        float randY = targetPos.y + randYOffset;

        Vector3 randomPosOnTile = new Vector3(randX, randY);

        transform.position = Vector3.Lerp(gameObject.transform.position,
            randomPosOnTile, glueSpeed); //targetTile.transform.position
    }
}