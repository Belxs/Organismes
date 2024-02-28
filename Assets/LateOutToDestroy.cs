using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateOutToDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sr;
    public float timeout;
    public float time;
    public bool destroy;
    public GameObject parent;
    void Start()
    {
        ExceptionPause.AddScript(this);
        sr = GetComponent<SpriteRenderer>();
        time = 1f;

        
    }

    public void Init()
    {
        IElement element = parent.GetComponent<IElement>();
        switch (element.GetType().Name)
        {
            case "Food":
                ((Food)(element)).ld = this;
                break;
            case "Water":
                ((Water)(element)).ld = this;
                break;
        }
    }
    public static LateOutToDestroy GetLateOut(IElement element)
    {
        
        switch (element.GetType().Name)
        {
            case "Food":
                return ((Food)(element)).ld;
                
            case "Water":
                return ((Water)(element)).ld;
            
        }
        return null;
    }
    public static void LateDestroy(GameObject obj,float time)
    {

    }
    public static void AddLD(GameObject obj)
    {
        
    }
    public void LateDestroy(float time)
    {
        if (!destroy)
        {
            this.time = time;
            timeout = time;
            destroy = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            if(parent.GetComponent<CircleCollider2D>())
            parent.GetComponent<CircleCollider2D>().enabled = false;
            if (timeout > 0)
            {
                timeout -= Time.deltaTime;
                sr.color = new(sr.color.r, sr.color.g, sr.color.b, (timeout / time));
            }
            else Destroy(parent);
        }
        else
        {
            if (timeout <= time)
            {
                timeout += Time.deltaTime;
                sr.color = new(sr.color.r, sr.color.g, sr.color.b, (timeout / time));
            }
            else sr.color = new(sr.color.r, sr.color.g, sr.color.b, 1); 
        }
    }
    public void OnDestroy()
    {
        ExceptionPause.RemoveScript(this);
    }

}
