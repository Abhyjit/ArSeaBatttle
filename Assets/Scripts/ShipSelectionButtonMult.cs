using UnityEngine;
using Photon.Pun;

public class ShipSelectionButtonMult : MonoBehaviour
{
    public int shipIndex; // Index of the ship in the GridManager's shipPrefabs array
    public GridManagerMult gridManager; // Reference to the GridManager

    public void OnButtonClick()
    {
        gridManager.SetCurrentShip(shipIndex); // Set the currently selected ship
        Debug.Log($"Selected ship index: {shipIndex}");

        // Place the selected ship randomly and synchronize it
        gridManager.PlaceShipRandomly();

        // Use RPC to notify other players of the ship selection
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("NotifyShipSelected", RpcTarget.Others, shipIndex);
    }

    [PunRPC]
    public void NotifyShipSelected(int selectedShipIndex)
    {
        gridManager.SetCurrentShip(selectedShipIndex);
        gridManager.PlaceShipRandomly();
    }
}
