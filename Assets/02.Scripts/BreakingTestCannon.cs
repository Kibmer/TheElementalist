using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingTestCannon : MonoBehaviour
{
    private GameObject roundShot;
    private Transform firePos;

    private void Awake()
    {
        roundShot = (GameObject)Resources.Load("Round_shot");
        firePos = transform.Find("Small_cannon").Find("FirePos");
    }

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject shot = Instantiate(roundShot, firePos.position, Quaternion.identity);
        shot.GetComponent<Rigidbody>().AddForce(firePos.forward * 2000f);
    }
}
