using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Cannon : MonoBehaviourPunCallbacks
{
    private GameObject roundShot;
    private Transform firePos;

    private void Awake()
    {
        roundShot = (GameObject)Resources.Load("Round_shot");
        firePos = transform.Find("Small_cannon").Find("FirePos");
    }


    private void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Fire", RpcTarget.All, null);
        }
    }

    [PunRPC]
    private void Fire()
    {
        GameObject shot = Instantiate(roundShot, firePos.position, Quaternion.identity);
        // shot.GetComponent<Rigidbody>().AddForce(firePos.forward * 4000f);
        shot.GetComponent<Rigidbody>().velocity = firePos.forward*40f;
        StartCoroutine(aa(shot));
        
    }
    
    IEnumerator aa(GameObject shot)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log(shot.GetComponent<Rigidbody>().velocity);

    }
}
