using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public ShopManager shopManager;
    public GameObject basicTowerObject;

    private GameObject dummyPlacement;
    private GameObject hoverTile;

    public Camera cam;
    public LayerMask mask;
    public LayerMask towerMask;

    public bool isBuilding;

    public void Start()
    {
        StartBuilding();
    }

    // Gets the current position of the mouse
    public Vector2 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    // Checks if the current tile is a valid space to 
    // place a tower on.
    public void getCurrentHoverTile()
    {
        Vector2 mousePosition = GetMousePosition();

        // Sets all of the object collisions by the raycast
        // to "hit".
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, mask, -100, 100);

        if (hit.collider != null)
        {
            // Checking if the object detected is a maptile
            if (MapGenerator.mapTiles.Contains(hit.collider.gameObject))
            {
                // Checking if the maptile detected is NOT a pathtile
                if (!MapGenerator.pathTiles.Contains(hit.collider.gameObject))
                {
                    // Set "hoverTile" to the gameObject that
                    // collided with the raycast
                    hoverTile = hit.collider.gameObject;
                    //Debug.Log(hit.collider.name);
                }
            }
        }
    }

    public bool checkForTower()
    {
        bool towerOnSlot = false;

        Vector2 mousePosition = GetMousePosition();

        // Sets the "hit" raycast to collide with any
        // EXISTING towers in play
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, towerMask, -100, 100);

        // Checks if there is a tower already in the tile
        if (hit.collider != null)
        {
            towerOnSlot = true;
        }

        return towerOnSlot;
    }

    public void placeBuilding()
    {
        if (hoverTile != null)
        {
            // If there's no tower in the current tile
            if (checkForTower() == false)
            {
                if (shopManager.CanBuyTower(basicTowerObject) == true)
                {
                    GameObject newTowerObject = Instantiate(basicTowerObject);
                    newTowerObject.layer = LayerMask.NameToLayer("Tower");
                    newTowerObject.transform.position = hoverTile.transform.position;

                    EndBuilding();
                    shopManager.BuyTower(basicTowerObject);
                }
                else
                {
                    Debug.Log("Not enough money!");
                }
            }
        }
    }

    public void StartBuilding()
    {
        isBuilding = true;
        dummyPlacement = Instantiate(basicTowerObject);

        // Destroy the tower and barrel rotation scripts
        // attached to the dummy tower object
        if (dummyPlacement.GetComponent<Tower>() != null)
        {
            Destroy(dummyPlacement.GetComponent<Tower>());
        }
        if (dummyPlacement.GetComponent<BarrelRotation>() != null)
        {
            Destroy(dummyPlacement.GetComponent<BarrelRotation>());
        }
    }

    public void EndBuilding()
    {
        isBuilding = false;

        if (dummyPlacement != null)
        {
            Destroy(dummyPlacement);
        }
    }

    public void Update()
    {
        if (isBuilding == true)
        {
            if (dummyPlacement != null)
            {
                getCurrentHoverTile();

                if (hoverTile != null)
                {
                    // Snaps the position of the dummy tower
                    // to the same position as the current tile 
                    // being hovered over.
                    dummyPlacement.transform.position = hoverTile.transform.position;
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                placeBuilding();
            }
        }
    }
}
