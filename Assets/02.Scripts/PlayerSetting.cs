using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSetting : MonoBehaviourPunCallbacks
{
    public Transform spawnPoint2;
    void Start()
    {
        if(PhotonNetwork.CountOfPlayersInRooms > 0)
        {
            Debug.Log(PhotonNetwork.CountOfPlayersInRooms);
            transform.position = spawnPoint2.position;
            transform.Rotate(0, 180, 0);
        }
    }
}
