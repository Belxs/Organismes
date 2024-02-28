using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food :MonoBehaviour, IElement
{
    public float nutrition;

    public LateOutToDestroy ld;
    public void Awake()
    {

        ExceptionPause.AddScript(this);
        Gardener.Add(gameObject, GetType().Name);
    }
    public void OnDestroy()
    {
        Gardener.Remove(gameObject, GetType().Name);
       
            ExceptionPause.RemoveScript(this);
        
    }

  
   
    public ElementInfo GetElementInfo()
    {
        return new ElementInfo(this);
    }
}
