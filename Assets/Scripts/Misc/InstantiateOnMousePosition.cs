using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnMousePosition : MonoBehaviour
{
    public GameObject prefabToInstantiate;

    private Camera _Camera;
    void Start()
    {
        _Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Store current mouse position in pixel coordinates.
            Vector3 mousePixelPos = Input.mousePosition;

            // Add depth so it can actually be used to cast a ray.
            mousePixelPos.z = 20f;

            // Transform from pixel to world coordinates
            Vector3 mouseWorldPosition = _Camera.ScreenToWorldPoint(mousePixelPos);

            // Remove depth
            mouseWorldPosition.z = 0f;

            // Spawn your prefab
            Instantiate(prefabToInstantiate, mouseWorldPosition, Quaternion.identity);
        }
    }
}
