using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandController : MonoBehaviour
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;
    //public SteamVR_Action_Boolean ControllerTrigger = SteamVR_Actions.default_InteractUI;
    //public SteamVR_Behaviour_Pose pose;
    //public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    public GameObject ui_element;

    void Start()
    {
        ui_element = transform.GetChild(1).gameObject;
        Debug.Log(ui_element.name);
    }

    void Update()
    {
        if(grab.GetState(righthand))
        {
            Debug.Log("@@@@@@@@@");
            ui_element.SetActive(true);
        }

        ui_element.SetActive(false);
    }

    

}
