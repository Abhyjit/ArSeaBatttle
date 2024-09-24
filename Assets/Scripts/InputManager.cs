using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; // Required for AR functionality
using UnityEngine.XR.ARSubsystems; // Required for AR subsystems

public class InputManager : MonoBehaviour
{
    public ARRaycastManager arRaycastManager; // Reference to the ARRaycastManager
    public GridManager gridManager; // Reference to the GridManager
    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); // To store raycast hits

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // On touch down, perform raycasting
            if (touch.phase == TouchPhase.Began)
            {
                PerformRaycast(touch.position);
            }
        }
        // Check for mouse input (for testing in the editor)
        else if (Input.GetMouseButtonDown(0))
        {
            PerformRaycast(Input.mousePosition);
        }
    }

    void PerformRaycast(Vector2 screenPosition)
    {
        // Perform the raycast
        if (arRaycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Get the position of the first hit
            Pose hitPose = hits[0].pose;

            // Call the method in GridManager to create the grid if not already created
            gridManager.CreateGrid(hitPose.position);
        }
    }
}