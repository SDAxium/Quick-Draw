using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class Launcher : MonoBehaviourPunCallbacks
{

    /// <summary>
    /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
    /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
    /// Typically this is used for the OnConnectedToMaster() callback.
    /// </summary>
    bool isConnecting;
    
    /// <summary>
    /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
    /// </summary>
    
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    public byte maxPlayersPerRoom = 2;

    [Tooltip("The Ui Panel to let the user enter name, connect and play")] [SerializeField]
    public GameObject controlPanel;

    [Tooltip("The UI Label to inform the user that the connection is in progress")] [SerializeField]
    public GameObject progressLabel;

    ///<summary>
    /// This client's game version. User's are seperated by game version. 
    ///</summary>
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    private string _gameVersion = "1";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        isConnecting = true;
        
        //Check if we are connected
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("CONNECTED HERE");
            //Attempt to join a random room. If a room does not exist, a new one will be created in OnRoomJoinedFailed()
            PhotonNetwork.JoinRandomRoom();
            
        }else{
            Debug.Log("CONNECTING...");
            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
            
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        isConnecting = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(
            "PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one." +
            "\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = maxPlayersPerRoom});
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // #Critical
            // Load the Room Level.
            PhotonNetwork.LoadLevel(1);
        }
        Debug.Log("Loading room 1");
    }
}