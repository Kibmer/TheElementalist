using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBlockMgr : MonoBehaviour
{
    public int w_blockHP;

    private Rigidbody w_blockRb;
    private Rigidbody cannonShotRb;

    private float preBlockVelocity;
    private float cannonVelocity;


    public float debrisF;                        //파편프리팹 날아갈 세기
    public Rigidbody[] debRb;                    //파편프리팹 Rb 

    private GameObject woodenBlock;
    private GameObject p_debris;
    private MeshRenderer[] testStoneLOD;         //블록LOD배열의 메쉬렌더러

    void Start()
    {
        w_blockHP = 50;
        w_blockRb = gameObject.GetComponent<Rigidbody>();
        p_debris = transform.Find("Debris").gameObject;

        woodenBlock = GameObject.FindWithTag("WOODENBLOCK");
        testStoneLOD = transform.Find("LOD").gameObject.GetComponentsInChildren<MeshRenderer>();
    }



    void Update()
    {
        preBlockVelocity = w_blockRb.velocity.magnitude; //블록의 속도크기 측정
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "GROUND")
        {
            if (preBlockVelocity >= 5.0f && preBlockVelocity < 10f)
            {
                w_blockHP -= 30;
            }
            else if (preBlockVelocity >= 10.0f)
            {
                w_blockHP -= 50;
            }
        }

        //블록HP 50이하일 때 색 변화
        if (w_blockHP <= 25)

            for (int i = 0; i < testStoneLOD.Length; i++)
            {
                testStoneLOD[i].material.SetColor("_BaseColor", Color.gray);
            }

        //블록HP가 0보다 작거나 같을때 폭발
        if (w_blockHP <= 0)
        {
            Explosion();
            Destroy(p_debris, 2.0f);
        }
    }

    public void Explosion()
    {
        p_debris.SetActive(true);
        transform.Find("LOD").gameObject.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < debRb.Length; i++)
        {
            debRb[i].AddExplosionForce(debrisF, transform.position, 1.0f);
        }
    }
}
