using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;
using Photon.Realtime;

public class RightHandController : MonoBehaviourPunCallbacks
{
    public SteamVR_Action_Pose pos = SteamVR_Actions.default_Pose;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    public GameObject prefab_ui_element;
    public GameObject prefab_fireBall;
    public GameObject prefab_iceBall;
    public GameObject prefab_cannon;

    private GameObject currentUI;
    private GameObject attack;

    private Rigidbody rb;

    void OnEnable()
    {
        PlayerManager.Summon += Summon;
        PlayerManager.CancelAttack += CancelAttack;
        PlayerManager.MagicAttack += MagicAttack;
        PlayerManager.WeaponEquip += WeaponEquip;
    }
    void OnDisable()
    {
        PlayerManager.Summon -= Summon;
        PlayerManager.CancelAttack -= CancelAttack;
        PlayerManager.MagicAttack -= MagicAttack;
        PlayerManager.WeaponEquip -= WeaponEquip;
    }

    void Summon()
    {
        if (!photonView.IsMine) return;

        photonView.RPC("SummonRPC", RpcTarget.All);
    }

    [PunRPC]
    void SummonRPC()
    {
        Debug.Log("Summon");
        if (PlayerManager.Weapon == "ICE")
        {
            // CancelAttack();
            attack = Instantiate(prefab_iceBall, transform.position + (Vector3.up * 0.2f), transform.rotation, this.transform);
        }
        else if (PlayerManager.Weapon == "FIRE")
        {
            // CancelAttack();
            attack = Instantiate(prefab_fireBall, transform.position + (Vector3.up * 0.2f), transform.rotation, this.transform);
        }
    }

    void MagicAttack()
    {
        if (!photonView.IsMine) return;

        Rigidbody attackRb = attack.GetComponent<Rigidbody>();
        attackRb.velocity = pos.GetVelocity(righthand) * 3f;

        photonView.RPC("MagicAttackRPC", RpcTarget.All, attack.transform.position, attackRb.velocity);
    }

    [PunRPC]
    void MagicAttackRPC(Vector3 pos, Vector3 vel)
    {
        Rigidbody attackRb = attack.GetComponent<Rigidbody>();

        attackRb.isKinematic = false;
        attack.transform.SetParent(null);
        attack.transform.position = pos;
        attackRb.velocity = vel;
        if (PlayerManager.Weapon == "ICE")
        {
            attack.GetComponent<Iceball>().enabled = true;
        }
        else if (PlayerManager.Weapon == "FIRE")
        {
            attack.GetComponent<Fireball>().enabled = true;
        }
    }

    void WeaponEquip()
    {

        prefab_cannon.SetActive(true);
    }

    void CancelAttack()
    {
        prefab_cannon.SetActive(false);

        if (attack != null)
        {
            Destroy(attack);
        }
    }
}
