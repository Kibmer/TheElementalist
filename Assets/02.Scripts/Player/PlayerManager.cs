﻿using System.Collections;
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
    public static event OnClickEvent MagicAttack;
    public static event OnClickEvent WeaponEquip;
    public static event OnClickEvent CannonAttack;

    public static string Weapon;


    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;
    public SteamVR_Action_Boolean Trigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose;
    public SteamVR_Action_Boolean touchpadClick = SteamVR_Actions.default_TouchPadClick;
    public SteamVR_Action_Vector2 touchpadPos = SteamVR_Actions.default_TouchPadPos;



    private bool isSummon = false;
    private bool isEquip = false;    
    
    void Update()
    {
        //UI열기
        if (touchpadClick.GetStateDown(lefthand))
        {
            OpenUI();
        }
        //UI닫기
        else if (touchpadClick.GetStateUp(lefthand))
        {
            CloseUI();
        }
        //마법 소환
        if(Trigger.GetStateDown(righthand) && (pose.GetLocalRotation(righthand).eulerAngles.z >= 230f && pose.GetLocalRotation(righthand).eulerAngles.z <= 270f ))
        {
            if(Weapon=="FIRE" || Weapon=="ICE")
            {
                CancelAttack();
                Summon();
                isSummon = true;
                isEquip = false;
            }
        }
        //마법 공격
        else if(isSummon == true && Trigger.GetStateUp(righthand))
        {
            MagicAttack();
            isSummon = false;
        }
        //캐논 소환
        if(isEquip == false && Weapon == "CANNON" && Trigger.GetStateDown(righthand))
        {
            CancelAttack();
            WeaponEquip();
            isEquip = true;
        }
        else if (isEquip == true && Trigger.GetStateDown(righthand))
        {
            CannonAttack();
        }

    }
}
