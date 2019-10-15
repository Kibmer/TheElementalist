using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    //public GameObject p_debris;                //파편큐브프리팹
    public float debrisF;                        //파편프리팹 날아갈 세기
    public Rigidbody[] debRb;                    //파편프리팹 Rb 

    public void Start()
    {

    }
    public void Explosion()
    {
        transform.Find("Debris").gameObject.SetActive(true);
        //Debug.Log(transform.Find("Debris").gameObject.name);
        transform.Find("LOD").gameObject.SetActive(false);
        //Debug.Log(transform.Find("LOD").gameObject.name);
        for (int i = 0; i < debRb.Length; i++)
        {
            debRb[i].AddExplosionForce(debrisF, transform.position, 1.0f);
        }

    }
}
