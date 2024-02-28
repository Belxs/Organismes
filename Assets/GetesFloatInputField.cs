using UnityEngine;
using TMPro;
public class GetesFloatInputField : MonoBehaviour
{

    public TMP_InputField input;
    public GesetValue geset=new();
    public string NamePer;
    public string findValue;
    public string buffer;
    public bool trfl_set;
    public void Awake()
    {

        input.text = geset.Action(NamePer, "G");
        findValue = input.text;
    }
    public void OnValue(string s)
    {

        buffer = s;
        trfl_set = false;
    }
    public void SetValue(string s)
    {
        if (!trfl_set)
        {
            trfl_set= true;
            geset.Action(NamePer, "S", float.Parse(buffer));
            input.text = geset.Action(NamePer, "G");
        }
    }
    void Update()
    {
       
        
            input.onValueChanged.AddListener(OnValue);
            input.onEndEdit.AddListener(SetValue);
        
    }
    public class GesetValue
    {

        public float n = float.NaN;
        

        public string Action(string Namepere,string E,float seted=float.NaN)
        {
            string result = "";
            switch (E)
            {
                case "G":
                    result = ReturnAdress(Namepere).ToString();
                    break;
                case "S":
                    ReturnAdress(Namepere) = seted;
                    break;

            }


            return result;
        }
        public ref float ReturnAdress(string NamePeremen)
        {

            switch (NamePeremen)
            {
                case "TI":
                    return ref Field.environment.TanInSide;
                case "TDI":
                    return ref Field.environment.TanDelinSide;
                case "TDO":
                    return ref Field.environment.TanDeloutSide;
                case "SI":
                    return ref Field.environment.SinInSide;
                case "SDI":
                    return ref Field.environment.SinDelinSide;
                case "SN":
                    return ref Field.environment.StronghtNoize;
                case "DN": 
                    return ref Field.environment.DelNoize;
                case "CO":
                    return ref SpawnerOrganism.BoundsOrgs;
                case "MT":
                    return ref Organism.Mutanting;
                case "CS":
                    return ref SpawnerOrganism.SpawnCount;
                case "AXY":
                    return ref TimeOfYear.MaxTemp;
                case "INY":
                    return ref TimeOfYear.MinTemp;
                case "TY":
                    return ref TimeOfYear.time;


            }
            return ref n;
        }
       



    }
}


