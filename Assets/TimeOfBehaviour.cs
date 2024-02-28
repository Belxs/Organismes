using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfBehaviour : MonoBehaviour
{
    public bool pause;
    public float savetimespeed;
    // Start is called before the first frame update
    void Start()
    {
        //AutoTimeUpdate.AddUpdate(Update);
        
    }
    public void SetTimeScale(float f)
    {
        Time.timeScale = f;
    }
    // Update is called once per frame
    void Update()
    {
       // Debug.Log("TimeBehaviour!");
       if(!ConsoleDeveloper.active)
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!pause)
            {
                pause = true;
                savetimespeed = Time.timeScale;
                ExceptionPause.SetPause(pause);
                Time.timeScale = 0;
            }
            else
            {
                pause = false ;
                ExceptionPause.SetPause(pause);
                Time.timeScale = savetimespeed;
            }
        }
    }
}
