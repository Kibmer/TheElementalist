using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodExplosion : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }
}
