using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SeterTogge : MonoBehaviour
{
    public string NamePer;
    public SetBoolValue sbl = new();
    public Toggle toggle;
    
    void Update()
    {
        sbl.Action(NamePer, toggle.isOn);
    }
    public class SetBoolValue
    {
        public bool n = false;
        

        public void Action(string Nameper,bool togged)
        {
            
            ReturnAdress(Nameper)=togged;


            
        }
        public ref bool ReturnAdress(string NamePeremen)
        {
            switch (NamePeremen)
            {
                case "Season":
                    return ref TimeOfYear.good;
                

            }
            return ref n;
        }
       



    }
}
