
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources lefthand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources righthand = SteamVR_Input_Sources.RightHand;

    public delegate void FixedJointHandler();
    public static event FixedJointHandler OnFixedDestory;




    public SteamVR_Action_Boolean ControllerTrigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Behaviour_Pose pose;
    private GameObject grabObject;
    private GameObject colObject;

    public static GameObject preGrabObject;
    public static bool oneRespwan;
    Animator nomalHandAnim;

    bool canGrab_Obj;
    bool canRlease_Obj;

    // [HideInInspector]
    // public string itemName;

    private void Awake()
    {
        preGrabObject = new GameObject();
    }

    private void Start()
    {
        nomalHandAnim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (ControllerTrigger.GetStateDown(lefthand))
        {
            if (canGrab_Obj)
            {
                // if(colObject.tag == "Item")
                // {
                //     itemName="Item";
                //     Destroy(colObject.gameObject);
                // }
                //nomalHandAnim.SetBool("GrabObject", true);
                GrabObject();

            }
        }

        if (ControllerTrigger.GetStateUp(lefthand))
        {
            if (canRlease_Obj)
            {
                //nomalHandAnim.SetBool("GrabObject", false);
                ReleaseObject();
            }

        }

    }

    public void GrabObject()
    {
        grabObject = colObject;
        colObject = null;
        canRlease_Obj = true;
        canGrab_Obj = false;

        if (grabObject != null)
        {
            AddFixedJointComponet().connectedBody = grabObject.GetComponent<Rigidbody>();
            if (grabObject.GetComponent<MeshCollider>() != null)
            {
                grabObject.GetComponent<MeshCollider>().isTrigger = true;
            }
        }
    }

    public void GrabSack()
    {
        if (OnFixedDestory != null && !oneRespwan)
        {
            OnFixedDestory();
            oneRespwan = true;
        }
        grabObject = colObject;
        colObject = null;

        if (grabObject != null && GetComponent<FixedJoint>() == null)
        {
            AddFixedJointComponet().connectedBody = grabObject.GetComponent<Rigidbody>();
        }

    }

    private FixedJoint AddFixedJointComponet()
    {
        FixedJoint fixJoin = this.gameObject.AddComponent<FixedJoint>();
        fixJoin.breakForce = Mathf.Infinity;
        fixJoin.breakTorque = Mathf.Infinity;
        return fixJoin;
    }

    public void ReleaseObject()
    {
        if (this.gameObject.GetComponent<FixedJoint>() != null)
        {
            if (grabObject != null)
            {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>());

                grabObject.GetComponent<Rigidbody>().velocity = pose.GetVelocity();
                grabObject.GetComponent<Rigidbody>().angularVelocity = pose.GetAngularVelocity();



                if (grabObject.GetComponent<MeshCollider>() != null)
                {
                    grabObject.GetComponent<MeshCollider>().isTrigger = false;
                }

                grabObject = null;
                canRlease_Obj = false;
            }
        }
    }

    public void ReleasSack()
    {
        if (this.gameObject.GetComponent<FixedJoint>() != null)
        {
            if (grabObject != null)
            {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>());

                grabObject.GetComponent<Rigidbody>().velocity = pose.GetVelocity();
                grabObject.GetComponent<Rigidbody>().angularVelocity = pose.GetAngularVelocity();

                grabObject = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canRlease_Obj || other.GetComponent<Rigidbody>() == null) return; // 이미 손에 쥔 객체가 있으면 돌아가라
        if (other.CompareTag("GRABBABLE"))
        {
            colObject = other.gameObject;
            canGrab_Obj = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canRlease_Obj ||  other.GetComponent<Rigidbody>() == null) return;
        if (other.CompareTag("GRABBABLE"))
        {
            colObject = other.gameObject;
            canGrab_Obj = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (colObject == null)
        {
            canGrab_Obj = false;
            colObject = null;
        }
    }

}