using UnityEngine;

public class ShipSelectionButton : MonoBehaviour
{
    public int shipIndex; // Index of the ship in the GridManager's shipPrefabs array
    public GridManager gridManager; // Reference to the GridManager

    public void OnButtonClick()
    {
        gridManager.SetCurrentShip(shipIndex); // Set the currently selected ship
        Debug.Log($"Selected ship index: {shipIndex}");

        // Place the selected ship randomly
        gridManager.PlaceShipRandomly();
    }
}