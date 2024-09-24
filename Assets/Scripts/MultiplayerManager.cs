using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI; // Needed for UI elements like InputField
using UnityEngine.SceneManagement;
using TMPro;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    public static MultiplayerManager Instance;

    public TMP_InputField roomNameInputField; // Reference to the Input Field for room name
    public TextMeshProUGUI feedbackText; // Optional: To display feedback to the player (status or error messages)

    public GameObject playerPrefab; // Reference to the player prefab (ship or avatar)
    public Transform[] spawnPoints; // Array of spawn points for players

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the manager across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connect to Photon servers
        UpdateFeedback("Connecting to server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinLobby(); // Join the lobby after connecting
        UpdateFeedback("Connected to Photon. Please enter a room name.");
    }

    public void CreateRoom()
    {
        string roomName = roomNameInputField.text; // Get the input from the field
        if (string.IsNullOrEmpty(roomName))
        {
            UpdateFeedback("Room name cannot be empty.");
            return;
        }

        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 }; // Customize room settings here
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        UpdateFeedback($"Creating room: {roomName}...");
    }

    public void JoinRoom()
    {
        string roomName = roomNameInputField.text; // Get the input from the field
        if (string.IsNullOrEmpty(roomName))
        {
            UpdateFeedback("Room name cannot be empty.");
            return;
        }

        PhotonNetwork.JoinRoom(roomName);
        UpdateFeedback($"Joining room: {roomName}...");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully joined the room");
        UpdateFeedback("Successfully joined the room.");

        // Spawn the player when the room is joined
        SpawnPlayer();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Failed to create room: {message}");
        UpdateFeedback($"Room creation failed: {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Failed to join room: {message}");
        UpdateFeedback($"Join room failed: {message}");
    }

    private void SpawnPlayer()
    {
        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        DontDestroyOnLoad(player);

        // Optionally, load the game scene after player spawn
        SceneManager.LoadScene("GameScene");
    }

    private void UpdateFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
        }
    }
}
