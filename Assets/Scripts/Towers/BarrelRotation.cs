using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRotation : MonoBehaviour
{
/*    public Transform pivot;
    public Transform barrel;*/
    public Tower tower;

    private void Update()
    {
        if (tower)
        {
            if (tower.currentTarget)
            {
                Vector2 relative = tower.currentTarget.transform.position - transform.position;
                float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

                Vector3 newRotation = new Vector3(0, 0, angle);
                transform.localRotation = Quaternion.Euler(newRotation);
            }
        }
    }
}
