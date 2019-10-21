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
    public GameObject[] playerSpawnPoint;
    public GameObject[] mapSpawnPoint;
    public GameObject MapPrefab;
    private List<GameObject> Maps;

    public bool isMyTurn = false;
    private List<GameObject> turnFlames;

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

        playerSpawnPoint = GameObject.FindGameObjectsWithTag("PLAYERSPAWNPOINT");
        mapSpawnPoint = GameObject.FindGameObjectsWithTag("MAPSPAWNPOINT");
        Maps = new List<GameObject>();
        turnFlames = new List<GameObject>();
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
        //카메라 리그를 생성
        cameraRigTr = PhotonNetwork.Instantiate("[CameraRig]", Vector3.zero, Quaternion.identity, 0).transform;
        GameObject[] turnStones = GameObject.FindGameObjectsWithTag("TURNSTONE");
        foreach (var turnStone in turnStones)
        {
            turnFlames.Add(turnStone.GetComponentInChildren<ParticleSystem>(true).gameObject);
        }

        Debug.Log(PhotonNetwork.CountOfPlayersInRooms);
        //다른 플레이어가 방에 없을때
        if (PhotonNetwork.CountOfPlayersInRooms == 0)
        {
            MGManager.instance.isMyTurn = true;

            //카메라 리그를 비어있는 위치에 배치
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["Ppos"])
            {
                cameraRigTr.position = playerSpawnPoint[0].transform.position;
                cameraRigTr.Rotate(0, 180, 0);
            }
            else
            {
                cameraRigTr.position = playerSpawnPoint[1].transform.position;
            }
        }
        //다른 플레이어가 방에 있을때
        else
        {
            MGManager.instance.isMyTurn = false;

            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["Ppos"])
            {
                cameraRigTr.position = playerSpawnPoint[1].transform.position;
            }
            else
            {
                cameraRigTr.position = playerSpawnPoint[0].transform.position;
                cameraRigTr.Rotate(0, 180, 0);
            }
        }

        //Vector3 cannonSpawnPos = new Vector3(0, 0, (cameraRigTr.TransformVector(Vector3.forward) * 1.5f).z);
        //PhotonNetwork.Instantiate("Cannon"
        //            , cameraRigTr.position + cannonSpawnPos
        //            , cameraRigTr.rotation
        //            , 0);


        // ResetStage();
        SetTurnFlame();
        // Invoke("Sync", 10f);
    }

    void Sync()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SyncStage"
                            , RpcTarget.Others
                            , (object)Maps[0].GetComponentsInChildren<Transform>()
                            , (object)Maps[1].GetComponentsInChildren<Transform>());
            Debug.Log("Sync start");
        }
    }

    public void ResetStage()
    {
        foreach (var map in Maps)
        {
            Destroy(map);
        }
        Maps.Clear();
        Maps.Add(Instantiate(MapPrefab
                    , mapSpawnPoint[0].transform.position
                    , Quaternion.identity
                    , GameObject.Find("Map").transform));
        Maps.Add(Instantiate(MapPrefab
                    , mapSpawnPoint[1].transform.position
                    , Quaternion.Euler(0, 180, 0)
                    , GameObject.Find("Map").transform));
    }

    [PunRPC]
    private void SyncStage(object[] mapTr1, object[] mapTr2)
    {
        Transform[] curMapTr1 = Maps[0].GetComponentsInChildren<Transform>();
        Transform[] curMapTr2 = Maps[1].GetComponentsInChildren<Transform>();
        Transform[] newMapTr1 = (Transform[])mapTr1;
        Transform[] newMapTr2 = (Transform[])mapTr2;
        int i = 0;
        foreach (var tr in curMapTr1)
        {
            tr.position = newMapTr1[i].position;
            tr.rotation = newMapTr1[i].rotation;
            i++;
        }
        i = 0;
        foreach (var tr in curMapTr2)
        {
            tr.position = newMapTr2[i].position;
            tr.rotation = newMapTr2[i].rotation;
            i++;
        }
        Debug.Log("Sync Done");
    }

    public void EndTurn()
    {
        photonView.RPC("EndTurnRPC", RpcTarget.All);
    }

    [PunRPC]
    private void EndTurnRPC()
    {
        isMyTurn = !isMyTurn;
        SetTurnFlame();
    }

    private void SetTurnFlame()
    {
        if (isMyTurn)
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
