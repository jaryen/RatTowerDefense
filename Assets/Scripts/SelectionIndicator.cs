using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionIndicator : MonoBehaviour
{
    [Header("Unity Setup Fields")]
    public GameObject hoveredObject;
    public GameObject selectedObject;
    public Transform turretUI;
    public LayerMask towerMask;

    // Other fields
    private Color selectedColor;
    private Color hoveredColor;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        turretUI.gameObject.SetActive(false);
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }

    private void HoverObject(GameObject obj)
    {
        // If a object that has been hovered over exists
        if (hoveredObject)
        {
            // If there already is a hovered obj and the currently
            // hovered obj is equal to it, then do return.
            // Else, clear the pre-existing hover obj and set the 
            // hoverObject = obj
            if (obj == hoveredObject) return;
            ClearHover();
        }
        hoveredObject = obj;

        // Set the original color of game object to 
        // prevColor
        SpriteRenderer sr = hoveredObject.GetComponentInChildren<SpriteRenderer>();
        Color srCol = sr.color;
        hoveredColor = srCol;

        sr.color = Color.red;
    }

    private void SelectObject(GameObject obj)
    {
        // If a selected object ALREADY exists, then 
        // set existing one to null and return its color
        // back to original color.
        if (selectedObject)
        {
            if (obj == selectedObject) return;
            ClearSelect();
        }
        selectedObject = obj;

        selectedColor = hoveredColor;
        SpriteRenderer sr = selectedObject.GetComponentInChildren<SpriteRenderer>();
        sr.color = Color.yellow;

        hoveredObject = null;
    }

    private void ClearHover()
    {
        if (!hoveredObject) return;

        SpriteRenderer sr = hoveredObject.GetComponentInChildren<SpriteRenderer>();
        sr.color = hoveredColor;

        hoveredObject = null;
    }

    private void ClearSelect()
    {
        if (selectedObject == null) return;

        SpriteRenderer sr = selectedObject.GetComponentInChildren<SpriteRenderer>();
        sr.color = selectedColor;

        selectedObject = null;
    }

    private void Update()
    {
        Vector2 mousePosition = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, towerMask, -100, 100);

        if (hit.collider)
        {
            // Get the gameobject corresponding to the tower hit by ray
            GameObject hitObject = hit.transform.root.gameObject;

            if (hitObject != selectedObject)
            {
                HoverObject(hitObject);
            }

            // if mouse over tower and player left clicks,
            // select that tower
            if (Input.GetMouseButtonDown(0))
            {
                SelectObject(hitObject);

                Transform selectedObjPos = selectedObject.transform;
                Vector3 offsetPos = new Vector3(selectedObjPos.position.x, selectedObjPos.position.y + selectedObject.transform.localScale.y/2);
                Vector3 screenPos = mainCamera.WorldToScreenPoint(offsetPos);

                turretUI.position = screenPos;
                turretUI.gameObject.SetActive(true);
            }
        }
        else
        {
            ClearHover();

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (selectedObject)
                {
                    SpriteRenderer sr = selectedObject.GetComponentInChildren<SpriteRenderer>();
                    sr.color = selectedColor;
                    selectedObject = null;
                }
            }
        }

        if (!selectedObject)
        {
            turretUI.gameObject.SetActive(false);
        }
    }
}
