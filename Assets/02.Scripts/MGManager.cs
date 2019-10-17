using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MGManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static MGManager instance = null;

    private Transform cameraRigTr;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public bool myTurn = false;
    public GameObject[] turnFlames;

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }

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

        cameraRigTr = GameObject.FindWithTag("CAMERARIG").transform;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Room room = PhotonNetwork.CurrentRoom;
            Hashtable cp = room.CustomProperties;
            cp["Ppos"] = !(bool)cp["Ppos"];
            room.SetCustomProperties(cp);
        }
    }

    private void Start()
    {
        Debug.Log(PhotonNetwork.CountOfPlayersInRooms);
        if (PhotonNetwork.CountOfPlayersInRooms == 0)
        {
            //다른 플레이어가 방에 없을때
            MGManager.instance.myTurn = true;

            //플레이어를 비어있는 위치에 배치
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["Ppos"])
            {
                cameraRigTr.position = spawnPoint1.position;
            }
            else
            {
                cameraRigTr.position = spawnPoint2.position;
                cameraRigTr.Rotate(0, 180, 0);
            }
        }
        else
        {
            //다른 플레이어가 방에 있을때
            MGManager.instance.myTurn = false;

            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["Ppos"])
            {
                cameraRigTr.position = spawnPoint2.position;
                cameraRigTr.Rotate(0, 180, 0);
            }
            else
            {
                cameraRigTr.position = spawnPoint1.position;
            }
        }



        SetTurnFlame();
        Vector3 cannonSpawnPos = new Vector3(0, 0, Vector3.forward.z * 1.5f);
        PhotonNetwork.Instantiate("Cannon"
                    , cameraRigTr.position + cannonSpawnPos
                    , cameraRigTr.rotation
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
