using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDmg : MonoBehaviour
{
    private float shotVelocity;

    private void Update()
    {
        shotVelocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("STONEBLOCK"))
        {
            coll.gameObject.GetComponent<BlockMgr>().blockHP = 0;
        }
        else if(coll.collider.CompareTag("WOODENBLOCK"))
        {
            coll.gameObject.GetComponent<WoodenBlockMgr>().w_blockHP = 0;
        }
        Debug.Log(shotVelocity);
    }
}
