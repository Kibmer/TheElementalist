using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    void Awake()
    {
        ChangeElement.Element += ElementChange;
    }
   

    void ElementChange(GameObject hand, string el)
    {
        if (el == "ICE")
        {
            hand.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("_RimColor", new Color(0.3f, 0.5f, 1, 1));
        }
        if (el == "FIRE")
        {
            hand.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("_RimColor", Color.red);
        }

    }
}
