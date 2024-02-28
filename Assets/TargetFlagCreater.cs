using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFlagCreater : MonoBehaviour
{
    public GameObject flag;
    public Organism org;
    public static bool Deactive=true;
    
    void Update()
    {

        if (org != null)
        {
            if(org.behaviour!=null)
                if (org.behaviour.target != null)
                {
                    flag.transform.position = org.behaviour.target.pos;
                }
        }
        else
        {
            org = GetComponent<Organism>();
        }
        if (Deactive)
        {
            Destroy(flag);
            Destroy(this);
        }
    }
}
