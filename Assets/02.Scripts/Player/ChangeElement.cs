using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeElement : MonoBehaviour
{
    //public delegate void OnClickEvent(GameObject hand, string el);
    //public static event OnClickEvent changeElement;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "FIRE")
        {
            PlayerManager.element = "FIRE";
            transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("_RimColor", Color.red);
        }
        else if (other.gameObject.name == "ICE")
        {
            PlayerManager.element = "ICE";
            transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("_RimColor", new Color(0.3f, 0.5f, 1, 1));
        }
        
    }
}
