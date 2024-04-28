using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    private Color[] availableColors = { Color.red, Color.blue, Color.green, Color.yellow }; // Predefined set of colors
    private List<Color> takenColors = new List<Color>(); // List to keep track of taken colors

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("test", null, null);
        Debug.Log("we're connected and in the Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("we're in the room");

        // Instantiate player prefab
        GameObject _player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity);
        
        // Assign color to player
        Color playerColor = GetAvailableColor();
        _player.GetComponent<PlayerSetup>().isLocalPlayer(playerColor);
    }

    // Function to get an available color
    private Color GetAvailableColor()
    {
        foreach (Color color in availableColors)
        {
            if (!takenColors.Contains(color))
            {
                takenColors.Add(color);
                return color;
            }
        }
        // If all colors are taken, return default color (white)
        return Color.white;
    }
}
