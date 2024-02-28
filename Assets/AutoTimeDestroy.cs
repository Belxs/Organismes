using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTimeDestroy : MonoBehaviour
{
    public float time;
    public float timeout;
    public Transform pos;
    public void Awake()
    {
        timeout = time;
        ExceptionPause.AddScript(this);
    }
    public void SetPos(Transform pos)
    {
        this.pos = pos;
    }
    void Update()
    {
        if(pos!=null)
        transform.position = pos.position - Vector3.forward * 5;
        if (timeout < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timeout -= Time.deltaTime;
        }
    }
}
