using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour,IElement
{
    public ElementInfo GetElementInfo()
    {
        return new ElementInfo(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        ExceptionPause.AddScript(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
