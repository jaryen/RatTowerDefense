﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour
{
    public PlacementManager placementManager;

    public GameObject hoveredObject;
    public GameObject selectedObject;
    public LayerMask towerMask;
    public LayerMask UIMask;

    private Color originalCol;

    public Vector3 GetMousePosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }

    private void HoverObject(GameObject obj)
    {
        // If a object that has been hovered over exists
        if (hoveredObject != null)
        {
            // If there already is a hovered obj and the currently
            // hovered obj is equal to it, then do return.
            // Else, clear the pre-existing hover obj and set the 
            // hoverObject = obj
            if (obj == hoveredObject)
                return;

            ClearHover();
        }
        hoveredObject = obj;

        // Set the original color of game object to 
        // prevColor
        SpriteRenderer sr = hoveredObject.GetComponent<SpriteRenderer>();
        Color srCol = sr.color;
        originalCol = srCol;

        sr.color = Color.red;
    }

    private void SelectObject(GameObject obj)
    {
        // If a selected object ALREADY exists, then 
        // set existing one to null and return its color
        // back to original color.
        if (selectedObject != null)
        {
            if (obj == selectedObject)
                return;

            ClearSelect();
        }
        selectedObject = obj;

        SpriteRenderer sr = selectedObject.GetComponent<SpriteRenderer>();
        sr.color = Color.yellow;

        hoveredObject = null;
    }

    private void ClearHover()
    {
        if (hoveredObject == null)
            return;

        SpriteRenderer sr = hoveredObject.GetComponent<SpriteRenderer>();
        sr.color = originalCol;

        hoveredObject = null;
    }

    private void ClearSelect()
    {
        if (selectedObject == null)
            return;

        SpriteRenderer sr = selectedObject.GetComponent<SpriteRenderer>();
        sr.color = originalCol;

        selectedObject = null;
    }

    private void Update()
    {
        Vector2 mousePosition = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, towerMask, -100, 100);

        RaycastHit2D hitUI = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, UIMask, -100, 100);

        if (hit.collider != null)
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
            }
        }
        else if (hitUI.collider != null)
        {
            Debug.Log("Hit UI element");
        }
        else
        {
            ClearHover();

            if (Input.GetMouseButtonDown(0))
            {
                if (selectedObject != null)
                {
                    SpriteRenderer sr = selectedObject.GetComponent<SpriteRenderer>();
                    sr.color = originalCol;
                    selectedObject = null;
                }
            }
        }
    }
}