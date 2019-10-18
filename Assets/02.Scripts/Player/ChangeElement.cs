using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeElement : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FIRE")
        {

            PlayerManager.Weapon = "FIRE";
            transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("_RimColor", Color.red);
        }
        else if (other.gameObject.name == "ICE")
        {
            PlayerManager.Weapon = "ICE";
            transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("_RimColor", new Color(0.3f, 0.5f, 1, 1));
        }
        else if (other.gameObject.name == "CANNON")
        {
            PlayerManager.Weapon = "CANNON";
            transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("_RimColor", Color.black);
        }
        
    }
}
