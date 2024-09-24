using UnityEngine;

public class CellClick : MonoBehaviour
{
    private GridManager gridManager; // Reference to GridManager
    private int gridX; // X position in the grid
    private int gridY; // Y position in the grid

    // Set grid manager and cell coordinates
    public void SetGridManager(GridManager manager, int x, int y)
    {
        gridManager = manager;
        gridX = x;
        gridY = y;
    }

    private void OnMouseDown() // This assumes the object has a collider
    {
       // gridManager.PlaceShip(gridX, gridY, gridManager.shipPrefabs[gridManager.CurrentShipIndex]); // Call the method to place the ship
    }
}
