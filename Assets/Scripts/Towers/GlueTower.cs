using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueTower : Tower
{
    [Header("Unity Setup Fields")]
    public GameObject glueProjectile;

    [Header("Attributes")]
    public List<GameObject> pathTilesInRange = new List<GameObject>();
    [SerializeField] private float glueSpeed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        FindPathTilesInRange();
    }

    protected override void Shoot()
    {
        GameObject newGlue = Instantiate(glueProjectile, gameObject.transform);
        Glue glueScript = newGlue.GetComponent<Glue>();

        int randIndex = Random.Range(0, pathTilesInRange.Count);
        GameObject targetTile = pathTilesInRange[randIndex];

        if (glueScript)
        {
            glueScript.GetTargetTile(targetTile.transform);
        }
    }

    private void FindPathTilesInRange()
    {
        // Get all path tiles in range
        // of the glue tower.
        foreach (GameObject pathTile in MapGenerator.pathTiles)
        {
            if ((pathTile.transform.position - transform.position).magnitude <= range)
            {
                pathTilesInRange.Add(pathTile);
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (pathTilesInRange.Count > 0)
        {
            // Checks if the tower shoot cooldown
            // is ready.
            if (Time.time > nextTimeToShoot)
            {
                Shoot();
                nextTimeToShoot = Time.time + timeBetweenShots;
            }
        }
    }
}
