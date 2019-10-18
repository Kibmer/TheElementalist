using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInitializer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if(photonView.IsMine)
        {
            transform.Find("Camera").GetComponent<Camera>().enabled = true;
            transform.Find("Camera").GetComponent<AudioListener>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
