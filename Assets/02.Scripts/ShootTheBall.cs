using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTheBall : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0, 500, 2000);
    }
}
