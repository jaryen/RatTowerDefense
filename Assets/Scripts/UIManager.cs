using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Unity Setup Fields")]
    public GameObject cancelButton;
    public AudioSource cancelBtnAudio;

    private PlacementManager placementManager;

    // Start is called before the first frame update
    void Start()
    {
        placementManager = FindObjectOfType<PlacementManager>();
        cancelButton.SetActive(false);
    }

    public void cancelButtonPressed()
    {
        if (placementManager.dummyPlacement)
        {
            cancelBtnAudio.Play();
            Destroy(placementManager.dummyPlacement);
            placementManager.isBuilding = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (placementManager.isBuilding)
        {
            cancelButton.SetActive(true);
        } 
        else
        {
            cancelButton.SetActive(false);
        }
    }
}
