using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTimeUpdate : MonoBehaviour
{
    public delegate void Updates();
    public static List<Updates> updates = new();
    private void Start()
    {
        // StartCoroutine(Infinity());
        ExceptionPause.AddScript(this);
    }
    public IEnumerator Infinity()
    {
        while (true) {
           
            for(int i = 0; i < updates.Count; i++)
            {
                updates[i].Invoke();
                Debug.Log("WorkAutoUpdate!");
            }
            yield return new WaitForSeconds(0.001f);

        }
       // yield break;
    }
    public static void AddUpdate( Updates update )
    {
        updates.Add(update);
    }
    public static void RemoveUpdate(Updates update)
    {
        updates.Remove(update);
    }
    // Update is called once per frame
   
}
