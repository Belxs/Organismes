using System.Collections;
using TMPro;
using UnityEngine;
public class ShowTextBar : MonoBehaviour
{
    public TMP_Text tmp;
    public static ShowTextBar me;
    public static float timeout, time = 2, speed = 1, timesin = 1, timeoutsin;
    public static string text;
    public void Start()
    {
        me = this;
       // ShowText("");
    }
    public static void ShowText(string tex)
    {
        text = tex;
        
    }

    void Update()
    {
        if (text != "") {
            me.tmp.text = text;
            if ((timeoutsin <= timesin)&(timeout<time))
            {
                timeoutsin += Time.deltaTime;
                me.tmp.color = new Color(1, 1, 1, Mathf.Sin(timeoutsin / timesin));
            }
            else
            {
                if (timeout < time)
                {
                    timeout += Time.deltaTime;

                }
                else
                {
                    if (timeoutsin > 0)
                    {
                        timeoutsin -= Time.deltaTime;
                        me.tmp.color = new Color(1, 1, 1, Mathf.Sin(timeoutsin / timesin));
                    }
                    else
                    {
                        timeout = 0;
                        timeoutsin = 0;
                        me.tmp.text = "";
                        text = "";
                    }
                }
            }

            
        }
    }
}
