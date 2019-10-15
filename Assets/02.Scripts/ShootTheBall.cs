using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ShootTheBall : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Fire", RpcTarget.AllViaServer, null);
        }
    }
    [PunRPC]
    private void Fire()
    {
        rb.AddForce(0, 500, 2000);
    }
}
