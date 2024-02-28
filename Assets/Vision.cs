using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public List<GameObject> objs = new();
    public void Start()
    {
        ExceptionPause.AddScript(this);
    }
    public float GetVisionZone()
    {
        return GetComponent<CircleCollider2D>().radius;
    }
    public List<GameObject> SubSearchName(string named,string use,bool cicl,Organism buffer)
    {
        IElement ie = null;
        GameObject me = transform.parent.gameObject;
        List<GameObject> objsSub = new();
        Organism thisIe = me.GetComponent<Organism>();
        for (int i = 0; i < objs.Count; i++)
        {
            
            GameObject obj = null;
            bool good=false;
           
            obj = objs[i];
            if (obj != null)
            {
                ie = obj.GetComponent<IElement>();
                if (ie == null)
                {
                    ie = obj.transform.parent.GetComponent<IElement>();
                    obj = obj.transform.parent.gameObject;
                }
            }

            if (obj == me) continue;
            if (ie != null)
                if (ie.GetType().Name == named)
                {

                    if (named == "Organism")
                    {
                        Organism org_ie = (Organism)ie;
                        if ((use == "Spar") || (use == "Run"))
                        {
                            switch (thisIe.gender)
                            {
                                case Gender.M:
                                    if (org_ie.gender == Gender.W)
                                        if (org_ie.fetus == null)
                                        {
                                            good = true;
                                            objsSub.Add(obj);
                                        }
                                    break;
                                case Gender.B:
                                    good = true;
                                    objsSub.Add(obj);
                                    break;
                                case Gender.G:
                                    if ((org_ie.gender == Gender.G))
                                        if (org_ie.fetus == null)
                                        {
                                            good = true;
                                            objsSub.Add(obj);
                                        }
                                    break;


                            }
                        }
                        else
                        {
                            switch (use)
                            {
                                case "UnRun":
                                    switch (thisIe.gender)
                                    {
                                        case Gender.M:
                                            if (org_ie.gender != Gender.W)
                                            {

                                                good = true;
                                                objsSub.Add(obj);
                                            }

                                            break;
                                        case Gender.B:
                                            if (org_ie.gender == Gender.B)
                                            {
                                                objsSub.Add(obj);
                                                good = true;
                                            }
                                            break;
                                        case Gender.G:
                                            if (org_ie.gender != Gender.G)
                                            {
                                                good = true;
                                                objsSub.Add(obj);
                                            }

                                            break;


                                    }
                                    break;
                                case "maleLove":
                                    if (org_ie.gender == Gender.M)
                                    {
                                        if(org_ie.behaviour.target!=null)
                                            if (org_ie.behaviour.target.element!=null)
                                            if (org_ie.behaviour.target.element.GetType().Name=="Organism")
                                        if ((Organism)org_ie.behaviour.target.element == buffer)
                                        {
                                            good = true;
                                            objsSub.Add(obj);
                                        }
                                    }
                                    break;
                                default: continue;
                            }
                        }


                        
                    }
                    else { objsSub.Add(obj); good = true; }
                   
                }
            if(good)
            if (!cicl)
            {
                return objsSub;
            }

        }
        return objsSub;
    }
    public GameObject SearchName(string named, string use)
    {
        GameObject me = transform.parent.gameObject;
        List<GameObject> objsBase = SubSearchName(named, use, false,null);
        if (objsBase.Count > 0)
        {
            GameObject TargetObj = objsBase[0], MaleEnemy;
            Organism thisIe = me.GetComponent<Organism>();
            if ((use == "Spar") || (use == "Run"))
            {
                Organism org = TargetObj.GetComponent<Organism>();
                List<GameObject> ObjSubSub = SubSearchName(named, "maleLove", false, org);
                if (ObjSubSub.Count > 0)
                {
                    MaleEnemy = ObjSubSub[0];
                    if (MaleEnemy != null)
                    {

                        IElement ie = MaleEnemy.GetComponent<IElement>();
                        ActorTarget at = thisIe.behaviour.SetActor(ie, "V", 0);
                        thisIe.behaviour.CreateTarget(MaleEnemy.transform.position, ie, at, "Atack");
                    }
                    else return TargetObj;
                }
                else return TargetObj;
            }
            else return TargetObj;
        }
        
        return null;



       
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        objs.Add(collision.gameObject);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        objs.Remove(collision.gameObject);
    }
    public void OnDestroy()
    {
        ExceptionPause.RemoveScript(this);
    }
}
