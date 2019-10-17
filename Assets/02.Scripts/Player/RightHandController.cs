using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandController : MonoBehaviour
{
    public SteamVR_Action_Pose pos = SteamVR_Actions.default_Pose;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    public GameObject prefab_ui_element;
    public GameObject prefab_fireBall;
    public GameObject prefab_iceBall;

    private GameObject currentUI;
    private GameObject attack;

    private Rigidbody rb;

    void OnEnable()
    {
        PlayerManager.OpenUI += OpenUI;
        PlayerManager.CloseUI += CloseUI;
        PlayerManager.Summon += Summon;
        PlayerManager.CancelAttack += CancelAttack;
        PlayerManager.Attack += Attack;
    }
    void OnDisable()
    {
        PlayerManager.OpenUI -= OpenUI;
        PlayerManager.CloseUI -= CloseUI;
        PlayerManager.Summon -= Summon;
        PlayerManager.CancelAttack -= CancelAttack;
        PlayerManager.Attack -= Attack;
    }



    void OpenUI()
    {
        currentUI = Instantiate(prefab_ui_element, transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
    }

    void CloseUI()
    {
        if (currentUI == null) return;
        Destroy(currentUI);
    }

    void Summon()
    {

        if (PlayerManager.element == "ICE")
        {
            CancelAttack();
            attack = Instantiate(prefab_iceBall, transform.position + (Vector3.up * 0.2f), transform.rotation, this.transform);
        }
        else if (PlayerManager.element == "FIRE")
        {
            CancelAttack();
            attack = Instantiate(prefab_fireBall, transform.position + (Vector3.up * 0.2f), transform.rotation, this.transform);
        }
    }
    void CancelAttack()
    {
        if (attack != null)
            Destroy(attack);
            
    }

    void Attack()
    {
        Rigidbody attackRb = attack.GetComponent<Rigidbody>();
        attackRb.isKinematic = false;
        attack.transform.SetParent(null);
        attackRb.velocity = pos.GetVelocity(righthand)*3f;
        if (PlayerManager.element == "ICE")
        {
            attack.GetComponent<Iceball>().enabled = true;
        }
        else if (PlayerManager.element == "FIRE")
        {
            attack.GetComponent<Fireball>().enabled = true;
        }
    }
}
