using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    
    public List<GameObject> objs = new();
    public void Start()
    {
        ExceptionPause.AddScript(this);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
      
        objs.Add(collision.gameObject);
        
    }
    public GameObject SearchName(string named,Organism buffer=null)
    {
        IElement ie=null;
        GameObject me = transform.parent.gameObject;
        
        for (int i = 0; i < objs.Count; i++)
        {
            

            GameObject obj=null;
            //Debug.Log("me: " + me);
           
                 obj = objs[i];
                
                //Debug.Log("obj: " + obj);
                if (obj != null)
                {
                    
                    ie = obj.GetComponent<IElement>();
                    //Debug.Log("ei: " + ie);
                    if (ie == null)
                    {
                        ie = obj.transform.parent.GetComponent<IElement>();
                        //Debug.Log("ei2: " + ie);
                        obj = obj.transform.parent.gameObject;
                       // Debug.Log("obj: " + obj);
                    }
                }
            
            if (obj == me) continue;

            if (ie != null)
            {
                if (named == "Atack")
                {
                    if (ie.GetType().Name == "Organism")
                    {
                        if ((Organism)ie == buffer)
                        {
                            Organism thisOrg = me.GetComponent<Organism>();
                            ActorTarget at = thisOrg.behaviour.SetActor(ie, "C", 2);
                            thisOrg.behaviour.CreateTarget(obj.transform.position, ie, at, "Selection");
                        }
                    }
                    
            }else
                if (ie.GetType().Name == named)
                {
                    return obj;
                }
            }
        }
        return null;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
     
        objs.Remove(collision.gameObject);
    }
    public void OnDestroy()
    {
        ExceptionPause.RemoveScript(this);
    }
}

