using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : MonoBehaviour
{
    public GameObject prefab_IceExplosion;
    private AudioSource fireSound;

    private bool play = false;

    private void Start()
    {
        play = true;
        fireSound = GetComponent<AudioSource>();
        fireSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(!other.CompareTag("HAND"))
        if (play)
        {
            Instantiate(prefab_IceExplosion, transform.position, Quaternion.identity);

            Collider[] colls = Physics.OverlapSphere(transform.position, 1.2f);

            //추출한 Collider 객체에 폭발력 전달
            foreach (Collider coll in colls)
            {
                BlockMgr blockMgr = coll.GetComponent<BlockMgr>();
                WoodenBlockMgr woodenBlockMgr = coll.GetComponent<WoodenBlockMgr>();
                Rigidbody rb = coll.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.mass = 1.0f;
                    rb.AddExplosionForce(500.0f, transform.position, 1.2f, 1f);
                }
                if (blockMgr != null)
                {
                    blockMgr.blockHP -= 100;
                }
                else if (woodenBlockMgr != null)
                {
                    woodenBlockMgr.w_blockHP -= 100;
                }
            }
            Destroy(this.gameObject);
            play = false;
        }
    }
}
