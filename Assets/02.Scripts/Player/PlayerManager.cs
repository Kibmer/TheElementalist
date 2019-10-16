using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerManager : MonoBehaviour
{
    public delegate void OnClickEvent();
    public static event OnClickEvent OpenUI;
    public static event OnClickEvent CloseUI;

    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;
    public SteamVR_Action_Boolean ControllerTrigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Pose pos = SteamVR_Actions.default_Pose;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.GetStateDown(righthand))
        {
            //오픈ui
            OpenUI();
        }
        else if (grab.GetStateUp(righthand))
        {
            //닫기ui
            CloseUI();
        }
    }
}
