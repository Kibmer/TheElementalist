using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMgr : MonoBehaviour
{
    //BlockMgr blockMgr = GameObject.Find("StoneBlock").GetComponent<BlockMgr>();
    //WoodenBlockMgr woodenBlockMgr = GameObject.Find("WoodenBlock").GetComponent<WoodenBlockMgr>();

    public int orcCount = 3;
    public GameObject p_blood;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FIREEXPLOSION")
        || other.gameObject.CompareTag("ICEEXPLOSION")
        || other.gameObject.CompareTag("SHOT")
        || other.gameObject.CompareTag("GROUND"))
        {
            orcCount -= 1;
            Instantiate(p_blood,transform.position + Vector3.up * 1.5f, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
