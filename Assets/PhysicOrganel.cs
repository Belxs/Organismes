using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicOrganel : MonoBehaviour
{
    public float maxspeed;
    public Rigidbody2D rg;
    public List<Vector2> hispos=new();
    public static RigidbodyInterpolation2D inter = RigidbodyInterpolation2D.Interpolate;
    
    public void Init(float maxspeed)
    {
        this.maxspeed = maxspeed;
    }
    void Awake()
    {
        ExceptionPause.AddScript(this);
        rg =gameObject.AddComponent<Rigidbody2D>();
        rg.interpolation = inter;
        rg.gravityScale = 0;
        

    }
    public void Action()
    {
        if (hispos.Count < 2)
        {
            hispos.Add(transform.position);

        }
        else
        {
            hispos.RemoveAt(0);
        }
    }
    public void Move(Vector2 dir)
    {
        if (rg != null)
        {
            rg.AddForce(dir, ForceMode2D.Impulse);
            transform.eulerAngles = new Vector3(0, 0,Mathf.SmoothStep (transform.eulerAngles.z, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg-90,1/Time.deltaTime));
        }
    }
    public float GetFactSpeed()
    {
        if (hispos.Count >= 2)
        {
            return Vector2.Distance(hispos[0], hispos[1]);
        }
        return maxspeed;
    }
    public void ControlledSpeed()
    {
        if (rg.velocity.magnitude > maxspeed)
        {
            rg.velocity = rg.velocity.normalized * maxspeed;
        }
    }

    public void OnDestroy()
    {
        ExceptionPause.RemoveScript(this);
    }
}
