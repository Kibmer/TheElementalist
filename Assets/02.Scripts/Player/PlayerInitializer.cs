using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInitializer : MonoBehaviourPunCallbacks
{
    SteamVR_Behaviour_Pose steamVR_Behaviour_Pose_Left;
    SteamVR_Behaviour_Pose steamVR_Behaviour_Pose_Right;
    
    void Awake()
    {
        if(photonView.IsMine)
        {
            transform.Find("Camera").GetComponent<Camera>().enabled = true;
            transform.Find("Camera").GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            steamVR_Behaviour_Pose_Left = transform.Find("Controller (left)").GetComponent<SteamVR_Behaviour_Pose>();
            steamVR_Behaviour_Pose_Right = transform.Find("Controller (right)").GetComponent<SteamVR_Behaviour_Pose>();
            steamVR_Behaviour_Pose_Left.enabled = false;
            steamVR_Behaviour_Pose_Right.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
