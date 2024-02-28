using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastMouse : MonoBehaviour
{
    public GameObject metka,org_inv;
    public static GameObject metka_s, org_inv_s;
    public static string CurrentTmpMetka;
    public GameObject EffectSelected;
    public SpriteRenderer r;
    public const float localTimeDelta=1f/Organism.BetweenDelta;
    public float localTime;
    public Vector3 pos;
    public void CreateSelect(GameObject obj)
    {
        if (EffectSelected != null)
        {
            Destroy(EffectSelected);
            r = null;
        }
            GameObject newObj = new GameObject("Select");
            newObj.transform.localScale = obj.transform.localScale;
            newObj.transform.parent = obj.transform;
            r = newObj.AddComponent<SpriteRenderer>();
            r.sprite = obj.GetComponent<SpriteRenderer>().sprite;
            newObj.transform.localPosition = Vector3.zero;
        newObj.transform.localRotation = Quaternion.identity;
            //r.color = Color.red;
            r.sortingOrder = 23;
            EffectSelected = newObj;
            r.color = new Color(1,0,0,0.5f);
        

    }
    public void ColorHueDance()
    {
        if (r != null)
        {
            Vector3 hsv;
            Color.RGBToHSV(r.color,out hsv.x, out hsv.y, out hsv.z);

            r.color = Color.HSVToRGB(Mathf.Abs(Mathf.Sin(localTime)), hsv.y, hsv.z);
            r.color = new Color(r.color.r, r.color.g, r.color.b, 0.5f);
        }
    }
    public GameObject RayCast()
    {
        Camera cam = Camera.main;
        
        
        
        
        //pos.z = -10;
        
        RaycastHit2D hit;
        Ray ray = new Ray(pos, Vector3.forward);
        ray = cam.ScreenPointToRay(Input.mousePosition);
        
        ray.direction = Vector3.forward;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        hit=Physics2D.Raycast(ray.origin,ray.direction,50);
        if (hit.collider != null)
        {
            if (!hit.collider.isTrigger)
            {
                //Debug.Log("Raycast! " + hit.collider.gameObject);

                if (hit.collider.gameObject.GetComponent<Detector>() != null)
                {
                    //Debug.Log("return Raycast! " + hit.collider.gameObject);
                    return hit.collider.gameObject;
                }
            }
        }
        return null;
    }
    public void Update()
    {
        localTime += localTimeDelta;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        metka.transform.position = pos;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            GameObject obj = RayCast();
            //Debug.Log("obj " + obj);
            if (obj!=null)
            if (obj.transform.parent != null)
            {
                    //Debug.Log("parent " + obj.transform.parent);
                    Organism org = obj.transform.parent.gameObject.GetComponent<Organism>();
                    if (org != null)
                    {
                        CreateSelect(org.gameObject);
                        BackingInterface.NewOrganizm(org);
                        
                    }
            }
        }
        Cell cell= Field.Get_Is_World_to_Pos(pos);
        if (cell != null)
        {
            CurrentTmpMetka = Mathf.Round(cell.temperatura-273) + "°C";
        }
        else
        {
            CurrentTmpMetka = "";
        }
        ColorHueDance();
        metka_s = metka;
        org_inv_s = org_inv;
    }
}
