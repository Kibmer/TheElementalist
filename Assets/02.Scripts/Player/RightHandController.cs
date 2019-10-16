using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandController : MonoBehaviour
{
    public SteamVR_Action_Pose pos = SteamVR_Actions.default_Pose;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    public GameObject ui_element;

    private GameObject currentUI;

    void Awake()
    {
        PlayerManager.OpenUI += OpenUI;
        PlayerManager.CloseUI += CloseUI;
    }

    
    void OpenUI()
    {
        currentUI = Instantiate(ui_element, transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
    }
    
    void CloseUI()
    {
        if (currentUI == null) return;
        Destroy(currentUI);
    }


}
