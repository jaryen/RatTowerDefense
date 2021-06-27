using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public PlacementManager placementManager;

    public GameObject basicGameObject; // Set as gun tower prefab
    private GameObject hoverTower; 

    // Instantiates a new tower when the button is 
    // pressed
    public void CreateNewTower()
    {
        // Instantiate a hover tower
        hoverTower = Instantiate(basicGameObject);
        Debug.Log("Hover tower instantiated!");

        if (hoverTower != null)
        {
            // Make the hover tower transparent
            Color c = hoverTower.GetComponent<SpriteRenderer>().color;
            c.a = 0.5f;
            Debug.Log("Made hover tower transparent!");

            // Destroy the tower and barrel rotation scripts
            // attached to the dummy tower object
            if (hoverTower.GetComponent<Tower>() != null)
            {
                Destroy(hoverTower.GetComponent<Tower>());
            }
            if (hoverTower.GetComponent<BarrelRotation>() != null)
            {
                Destroy(hoverTower.GetComponent<BarrelRotation>());
            }
        }
    }

/*    private Vector2 FollowMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mousePos = new Vector3(transform.position.x, transform.position.y, 0);

        return mousePos;
    }*/

/*    private void Update()
    {
        hoverTower.transform.position = FollowMouse();
    }*/
}
