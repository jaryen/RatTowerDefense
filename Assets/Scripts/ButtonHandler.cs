using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public PlacementManager placementManager;

    // Instantiates a new tower when the button is 
    // pressed
    public void CreateNewTower()
    {
        placementManager.StartBuilding();
    }

    // Destroys the dummy tower
    public void ExitTowerBuilding()
    {
        placementManager.EndBuilding();
    }
}
