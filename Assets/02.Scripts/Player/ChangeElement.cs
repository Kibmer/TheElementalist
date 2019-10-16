using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeElement : MonoBehaviour
{
    public delegate void OnClickEvent(GameObject hand, string el);
    public static event OnClickEvent Element;

    private string el;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "FIRE")
        {
            el = "FIRE";
            Element(this.gameObject, el);
        }
        else if (other.gameObject.name == "ICE")
        {
            el = "ICE";
            Element(this.gameObject, el);
        }
        
    }
}
