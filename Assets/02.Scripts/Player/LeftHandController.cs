using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftHandController : MonoBehaviour
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean ControllerTrigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
