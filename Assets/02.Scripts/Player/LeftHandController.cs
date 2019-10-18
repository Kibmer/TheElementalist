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
    public SteamVR_Action_Boolean ControllerTrigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Pose pos = SteamVR_Actions.default_Pose;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    public GameObject prefab_ui_element;
    private GameObject currentUI;

    void OnEnable()
    {
        PlayerManager.OpenUI += OpenUI;
        PlayerManager.CloseUI += CloseUI;
    }
    void OnDisable()
    {
        PlayerManager.OpenUI -= OpenUI;
        PlayerManager.CloseUI -= CloseUI;
    }
    void OpenUI()
    {
        currentUI = Instantiate(prefab_ui_element, transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0),this.transform);
    }

    void CloseUI()
    {
        if (currentUI == null) return;
        Destroy(currentUI);
    }


}
