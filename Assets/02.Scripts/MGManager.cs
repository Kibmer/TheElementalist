using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MGManager : MonoBehaviourPunCallbacks
{
    public static MGManager instance = null;

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public bool myTurn = false;

    public GameObject[] turnFlames;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Debug.Log(PhotonNetwork.CountOfPlayersInRooms);
        if (PhotonNetwork.CountOfPlayersInRooms >= 1)
        {
            //다른 플레이어가 방에 있을때
            MGManager.instance.myTurn = false;
            transform.position = spawnPoint2.position;
            transform.Rotate(0, 180, 0);
        }
        else
        {
            //다른 플레이어가 방에 없을때
            MGManager.instance.myTurn = true;
            transform.position = spawnPoint1.position;
        }
    }

    private void Start()
    {
        SetTurnFlame();
        Vector3 cannonSpawnPos = new Vector3(0, 0, Vector3.forward.z * 1.5f);
        PhotonNetwork.Instantiate("Cannon"
                    , transform.position + cannonSpawnPos
                    , transform.rotation
                    , 0);
    }

    public void EndTurn()
    {
        myTurn = !myTurn;
    }

    public void SetTurnFlame()
    {
        if (myTurn)
        {
            foreach (var flame in turnFlames)
            {
                flame.SetActive(true);
            }
        }
        else
        {
            foreach (var flame in turnFlames)
            {
                flame.SetActive(false);
            }
        }
    }
}
