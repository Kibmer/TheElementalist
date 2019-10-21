using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject prefab_FireExplosion;
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
            Instantiate(prefab_FireExplosion, transform.position, Quaternion.identity);

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
                    rb.AddExplosionForce(1000.0f, transform.position, 1.2f, 0f);
                }

                if (blockMgr != null)
                {
                    blockMgr.blockHP -= 100;
                    Debug.Log(blockMgr.blockHP);
                }
                else if (woodenBlockMgr != null)
                {
                    woodenBlockMgr.w_blockHP -= 100;
                    Debug.Log(woodenBlockMgr.w_blockHP);
                }
            }

            Destroy(this.gameObject);
            play = false;
        }
    }
}
