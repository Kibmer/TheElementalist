using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1.0";
    public string userId = "Kibmer";
    public byte maxPlayer = 20;
    public int SceneIndexToMove = 1;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.NickName = userId;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect To Master");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed Join room !!!");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomProperties = new Hashtable() { { "Ppos", true } };
        roomOptions.MaxPlayers = this.maxPlayer;
        PhotonNetwork.CreateRoom(null
                                , roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room !!!");
        PhotonNetwork.LoadLevel(SceneIndexToMove);
    }
}
