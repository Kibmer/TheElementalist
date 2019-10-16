using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerManager : MonoBehaviour
{
    public delegate void OnClickEvent();
    public static event OnClickEvent OpenUI;
    public static event OnClickEvent CloseUI;
    public static event OnClickEvent Summon;
    public static event OnClickEvent CancelAttack;
    public static event OnClickEvent Attack;

    public static string element;


    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;
    public SteamVR_Action_Boolean Trigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose;

    

    private string el; 
    private bool isSummon = false;
    
    
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
        if(Trigger.GetStateDown(righthand) && (pose.GetLocalRotation(righthand).eulerAngles.z >= 250f && pose.GetLocalRotation(righthand).eulerAngles.z <= 290f ))
        {
            //소환
            Summon();
            isSummon = true;
        }
        else if(Trigger.GetStateUp(righthand) && isSummon == true)
        {
            Attack();
            isSummon = false;
        }
    }
}
