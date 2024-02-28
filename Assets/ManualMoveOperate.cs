using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualMoveOperate : MonoBehaviour
{
    public PhysicOrganel po;
    public float maxspeed;
    public GameObject flag;
    void Awake()
    {
        po = GetComponent<PhysicOrganel>();
        po.Init(maxspeed);
    }

    
    void Update()
    {
        po.Move((flag.transform.position - transform.position).normalized);
        po.Action();
        po.ControlledSpeed();
    }
}
