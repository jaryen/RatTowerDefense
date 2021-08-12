using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [Header("Unity Setup Fields")]
    public Camera cam;
    public LayerMask mask;
    public LayerMask towerMask;
    public ShopManager shopManager;
    public SelectionIndicator selectionIndicator;

    [Header("Gun Tower Fields")]
    public GameObject dummyTower;
    public GameObject actualTower;

    [Header("Laser Tower Fields")]
    public GameObject dummyLaserTower;
    public GameObject actualLaserTower;
    
    [Header("Gas Tower Fields")]
    public GameObject dummyGasTower;
    public GameObject actualGasTower;
    
    [Header("Glue Tower Fields")]
    public GameObject dummyGlueTower;
    public GameObject actualGlueTower;
    
    [Header("Flamethrower Tower Fields")]
    public GameObject dummyFlameTower;
    public GameObject actualFlameTower;

    [Header("During Game")]
    public bool isBuilding;
    private GameObject hoverTile;
    public GameObject currentTower;
    public GameObject currentDummyTower;
    public GameObject dummyPlacement;

    // Select which tower to use
    public void SetTower(GameObject tower)
    {
        currentTower = tower;

        // Should probably fix this someday
        if (tower.name.Equals("gunTower"))
        {
            currentDummyTower = dummyTower;
        } 
        else if (tower.name.Equals("laserTower"))
        {
            currentDummyTower = dummyLaserTower;
        }
        else if (tower.name.Equals("gasTower"))
        {
            currentDummyTower = dummyGasTower;
        }
        else if (tower.name.Equals("glueTower"))
        {
            currentDummyTower = dummyGlueTower;
        }
        else if (tower.name.Equals("flameTower"))
        {
            currentDummyTower = dummyFlameTower;
        }
    }

    // Gets the current position of the mouse
    public Vector3 GetMousePosition()
    {
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }

    // Checks if the current tile is a valid space to 
    // place a tower on.
    public void GetCurrentHovertile()
    {
        Vector2 mousePosition = GetMousePosition();

        // Sets all of the object collisions by the raycast
        // to "hit".
        // LayerMask mask = Every gameobject
        // in the raycast is detected EXCEPT for dummy tower.
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
                    // collided with the raycast AKA a mapTile
                    hoverTile = hit.collider.gameObject;
                }
            }
        }
        else // If mouse is NOT hovering over anything
        {
            hoverTile = null;
        }
    }

    public bool CheckForTower()
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

    public void PlaceBuilding()
    {
        if (hoverTile)
        {
            // If there's no tower in the current tile
            if (!CheckForTower())
            {
                if (shopManager.CanBuyTower(currentTower))
                {
                    GameObject newTowerObject = Instantiate(currentTower);
                    newTowerObject.transform.position = hoverTile.transform.position;

                    EndBuilding();
                    shopManager.BuyTower(currentTower);
                }
                else
                {
                    Debug.Log("Not enough money!");
                }
            }
            else
            {
                Debug.Log("A tower is already here.");
            }
        }
    }

    // Instantiates a new tower when build tower 
    // button is pressed.
    public void StartBuilding()
    {
        if (dummyPlacement != null)
        {
            Debug.Log("You are already building a turret");
            return;
        }

        isBuilding = true;
        dummyPlacement = Instantiate(currentDummyTower, GetMousePosition(), Quaternion.identity);
    }

    public void EndBuilding()
    {
        isBuilding = false;

        if (dummyPlacement != null)
        {
            Destroy(dummyPlacement);
        }
    }

    public void SellTower()
    {
        if (selectionIndicator.selectedObject != null)
        {
            shopManager.SellTower(actualTower);
            Destroy(selectionIndicator.selectedObject.gameObject);
        }
        else if (isBuilding)
        {
            EndBuilding();
        }
        else
        {
            Debug.Log("Please select a tower to delete");
        }
    }

    public void Update()
    {
        if (isBuilding)
        {
            if (dummyPlacement)
            {
                // Get the current map tile the mouse is hovering
                // over.
                GetCurrentHovertile();

                // If mouse is currently hovering over a map tile
                if (hoverTile)
                {
                    // Snaps the position of the dummy tower
                    // to the same position as the current tile 
                    // being hovered over.
                    dummyPlacement.transform.position = hoverTile.transform.position;
                }
                else // If mouse is NOT hovering over ANYTHING
                {
                    // Move the dummy tower at mouse position (no matter where)
                    dummyPlacement.transform.position = GetMousePosition();
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                PlaceBuilding();
            }
        }
    }
}
