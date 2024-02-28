using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class GetFloatText : MonoBehaviour
{
    public string NameValue,SubText,Syfix;
    public TMP_Text tmp;
    public ReturnValue rv;
    public void Awake()
    {
        rv = new(NameValue);
    }
    void Update()
    {
        tmp.text =SubText+ rv.Action()+Syfix;
    }
}

public class ReturnValue
{
    public float n=float.NaN;
    public string Namepere;

    public string Action()
    {
        string result = "";
        result = ReturnAdress(Namepere).ToString();


        return result;
    }
    public object ReturnAdress(string NamePeremen)
    {
        switch (NamePeremen)
        {
            case "Atmo":
                return  Mathf.Round( Field.Atmosphere-273f);
            case "CountOrgs":
                return SpawnerOrganism.orgs.Count;
            case "Season":
                return TimeOfYear.Season;
            case "MetkaTmp":
                return RayCastMouse.CurrentTmpMetka;
            case "TimeScale":
                return Mathf.Round(Time.timeScale*10)/10;

        }
        return 0;
    }
    public ReturnValue(string NamePeremen)
    {

        Namepere = NamePeremen;
    }
   


}
