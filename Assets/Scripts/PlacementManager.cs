using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public ShopManager shopManager;
    public GameObject basicTowerObject;

    private GameObject dummyPlacement;
    public GameObject hoverTile;

    public Camera cam;
    public LayerMask mask;
    public LayerMask towerMask;

    public bool isBuilding;

    public void Start()
    {
        // StartBuilding();
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
    public void getCurrentHoverTile()
    {
        Vector2 mousePosition = GetMousePosition();

        // Sets all of the object collisions by the raycast
        // to "hit".
        // LayerMask mask = "Everything", meaning every gameobject
        // in the raycast is detected.
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

    // Instantiates a new tower when build tower 
    // button is pressed.
    public void StartBuilding()
    {
        isBuilding = true;
        dummyPlacement = Instantiate(basicTowerObject, GetMousePosition(), Quaternion.identity);
        Debug.Log("Spawned new hover tower at: " + dummyPlacement.transform.position);

        // Make the hover tower transparent
        SpriteRenderer dummySR = dummyPlacement.GetComponent<SpriteRenderer>();
        Color dummyCol = dummySR.color;
        dummyCol.a = 0.5f;
        Debug.Log(dummyCol.a); // Not working!

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
                // Get the current map tile the mouse is hovering
                // over.
                getCurrentHoverTile();

                // If mouse is currently hovering over a map tile
                if (hoverTile != null)
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
                    Debug.Log("Your mouse is not covering a map tile");
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                placeBuilding();
            }
        }
    }
}
