using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExceptionPause : MonoBehaviour
{
    public static bool pause;
    public static List<MonoBehaviour> scripts=new();
    
    public static void SetPause(bool value)
    {
        pause = value;
        for(int i = 0; i < scripts.Count; i++)
        {
            if (scripts[i] != null)
            {
                scripts[i].enabled = !value;
                
            }
        }
    }
    public static void AddScript(MonoBehaviour script)
    {
        scripts.Add(script);
    }
    public static void RemoveScript(MonoBehaviour script)
    {
        scripts.Remove(script);
    }
    // Update is called once per frame
   
}
