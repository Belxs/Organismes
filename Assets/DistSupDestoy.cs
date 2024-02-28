using UnityEngine;

public class DistSupDestoy : MonoBehaviour
{
    public GameObject sup;
    public void Start()
    {
        ExceptionPause.AddScript(this);
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
            if (collision.gameObject != null)
                if (collision.gameObject.layer == 6)
                {
                    if (sup != collision.gameObject)
                    {
                        IElement element = sup.GetComponent<IElement>();
                        LateOutToDestroy ld = LateOutToDestroy.GetLateOut(element);
                        if(ld!=null)
                            if (!ld.destroy)
                            {
                                ld.LateDestroy(1f);
                            }

                    }


                }
    }
    public void OnDestroy()
    {
        ExceptionPause.RemoveScript(this);
    }

}
