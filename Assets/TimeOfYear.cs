using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfYear : MonoBehaviour
{
    public static bool good;
    public static float timeout = 0,time=60*4,realtime,MinTemp=-30,MaxTemp=30, valueYear;
    public static string Season="NONE";
  
    public bool init=true;

    public void Start()
    {
        ExceptionPause.AddScript(this);
    }
    public bool Diapozone(float min,float value,float max,char s)
    {
        if(s=='>')
            if(value>=0)
                return (value >= min) & (value < max);
        if (s == '<')
            if (value <= 0)
                return (value >= min) & (value < max);
        return false;
    }
    public string ReturnSeason()
    {
        if (Diapozone(0, valueYear, 0.4f,'>')) return "Лето";
        if (Diapozone(0.4f, valueYear, 0.6f, '>')) return "Осень";
        if (Diapozone(0.6f, valueYear, 1f, '>')) return "Зима";
        if (Diapozone(-1f, valueYear, -0.5f, '<')) return "Зима";
        if (Diapozone(-0.5f, valueYear, -0.25f, '<')) return "Весна";
        if (Diapozone(-0.25f, valueYear, 0f, '<')) return "Лето";
       // if (Diapozone(-0.5f, valueYear, -0.25f, '<')) return "Зима";
       // if (Diapozone(-0.25f, valueYear, 0, '<')) return "Весна";
        return "Лесеина";
    }
    void Update()
    {
       
        if (good)
        {
            if (init)
            {
                realtime = time / 2f;

                if (timeout < realtime)
                {
                    timeout += Time.deltaTime;
                }
                else
                {
                    timeout = -realtime;
                }
                valueYear = timeout / realtime;
                Season = ReturnSeason();
                float k_max= (MaxTemp + 273), k_min= (MinTemp + 273);
                float sumtemp =Mathf.Abs(k_max - k_min);
                Field.Atmosphere = k_min + sumtemp*(1-Mathf.Abs(valueYear));
            }
            else
            {
                init = true;
                
            }
            

        }
    }
}
