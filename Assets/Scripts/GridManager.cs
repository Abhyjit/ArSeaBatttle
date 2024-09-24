using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab; // Prefab for each grid cell
    public GameObject[] shipPrefabs; // Array of ship prefabs
    public int gridSizeX = 10; // Number of cells in the X direction
    public int gridSizeY = 10; // Number of cells in the Y direction
    public float cellSize = 1f; // Size of each cell
    public float horizontalSpacing = 0.1f; // Horizontal spacing between cells
    public float verticalSpacing = 0.1f; // Vertical spacing between cells

    private GameObject[,] gridCells; // 2D array to store grid cells
    private int currentShipIndex = -1; // Index of the currently selected ship
    private bool isGridSpawned = false; // Check if grid is already spawned
    public TrainingManager TrainingManager;

    // Create the grid at a specified position
    public void CreateGrid(Vector3 position)
    {
        if (isGridSpawned) return; // Prevent creating multiple grids

        gridCells = new GameObject[gridSizeX, gridSizeY];
        Vector3 startPos = position;

        // Generate the grid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 cellPosition = new Vector3(startPos.x + (x * (cellSize + horizontalSpacing)), startPos.y, startPos.z + (y * (cellSize + verticalSpacing)));
                GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                cell.transform.localScale = new Vector3(cellSize, 1, cellSize);
                cell.name = $"Cell_{x}_{y}";
                gridCells[x, y] = cell;
            }
        }
        TrainingManager.ShowInstruction("Click on the ship to spawn Ship");
        isGridSpawned = true; // Mark grid as spawned
    }

    // Place a ship randomly
    public void PlaceShipRandomly()
    {
        if (currentShipIndex == -1) return; // Ensure a ship is selected

        GameObject shipPrefab = shipPrefabs[currentShipIndex];
        Ship ship = shipPrefab.GetComponent<Ship>();
        int attempts = 0;

        while (attempts < 100) // Limit attempts to prevent infinite loop
        {
            // Random position for the ship
            int startX = Random.Range(0, gridSizeX);
            int startY = Random.Range(0, gridSizeY);

            // Check if the ship can be placed
            if (CanPlaceShip(startX, startY, ship.shipSize))
            {
                PlaceShip(startX, startY, shipPrefab);
                return; // Exit after successful placement
            }
            attempts++;
        }
        Debug.LogError("Unable to place ship after maximum attempts.");
    }

    // Check if the ship can be placed in the specified position
    private bool CanPlaceShip(int startX, int startY, int size)
    {
        if (startX + size > gridSizeX || startY >= gridSizeY) return false; // Check boundaries

        // Check for available space
        for (int x = startX; x < startX + size; x++)
        {
            if (gridCells[x, startY] == null) return false; // Space is occupied
        }
        return true; // Space is available
    }

    // Place a ship in the specified position
    private void PlaceShip(int startX, int startY, GameObject shipPrefab)
    {
        Vector3 position = gridCells[startX, startY].transform.position;
        GameObject ship = Instantiate(shipPrefab, position, Quaternion.identity);
        ship.name = shipPrefab.name; // Name the ship for reference

        // Mark grid cells as occupied
        for (int x = startX; x < startX + ship.GetComponent<Ship>().shipSize; x++)
        {
            gridCells[x, startY] = null; // Remove the cell reference
        }
    }

    // Set the currently selected ship
    public void SetCurrentShip(int index)
    {
        currentShipIndex = index; // Set the current ship index
    }
}