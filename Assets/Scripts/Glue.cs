using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour
{
    [SerializeField] private float glueSpeed = 0.1f;
    private Transform targetTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetTargetTile(Transform tile)
    {
        targetTile = tile;
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetTile)
        {
            return;
        }

        /*        transform.position = Vector3.MoveTowards(transform.position,
                    targetTile.transform.position, glueSpeed * Time.deltaTime);*/

        transform.position = Vector3.Lerp(gameObject.transform.position,
            targetTile.transform.position, glueSpeed);
    }
}
