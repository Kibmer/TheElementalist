using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodExplosion : MonoBehaviour
{
    private AudioSource orcSream;

    void Start()
    {
        orcSream = GetComponent<AudioSource>();
        orcSream.Play();
        //Destroy(this.gameObject, 5f);
    }
}
