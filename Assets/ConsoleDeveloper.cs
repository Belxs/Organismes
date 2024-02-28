using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ConsoleDeveloper : MonoBehaviour
{
    public string FieldInput;
    public string FieldOutput;
    public GameObject ui;
    public TMP_InputField field;
    public TextMeshProUGUI output;
    public static bool active;
    public void Output(object text)
    {
        FieldOutput += text.ToString() + '\n';
    }
    
    public float SetAndGet( float n,List<string> commandStrount)
    {
        if (commandStrount.Count > 1)
            switch (commandStrount[1])
            {
                case "get":
                    Output(n);
                    return float.NaN;
                    

                case "set":
                    float number = 0;
                    if (commandStrount.Count > 2)
                        if (float.TryParse(commandStrount[2], out number))
                        {
                            n = number;
                            Output("seted: " + n);
                            return n;
                        }
                    return float.NaN;
                    
                default:
                    Output("Ошибка ввода комманды: " + commandStrount[1]);
                    return float.NaN;
                   

            }
        return float.NaN;
    }
    public void SendCommand(string command)
    {
        Output(command);
        List<string> commandStrount=new();
        string value="";
        int end = command.Length-1;
        for(int i = 0; i < command.Length; i++)
        {
            if(i == end)
            {
                value += command[i];
                commandStrount.Add(value);
                value = "";
            }
            else
            if(command[i]==' ')
            {
                commandStrount.Add(value);
                value = "";
            }
            else
            {
                value += command[i];
            }
        }
        switch (commandStrount[0])
        {
            case "Rate":
                 float f = SetAndGet(Application.targetFrameRate, commandStrount);
                if (!float.IsNaN(f))
                {
                    
                    Application.targetFrameRate = (int)f;

                }

                break;
            case "Step":
               
                    float f1 = SetAndGet(Time.fixedDeltaTime, commandStrount);
                    if (!float.IsNaN(f1))
                    {

                        Time.fixedDeltaTime = f1;

                    }
                
                break;
            case "Vsunc":

                float f2 = SetAndGet(QualitySettings.vSyncCount, commandStrount);
                if (!float.IsNaN(f2))
                {

                    QualitySettings.vSyncCount = (int)f2;

                }

                break;
            default:
                Output("Ошибка ввода комманды: " + commandStrount[0]);
                break;
        }
    }
    
    public void OnDeselect(string text)
    {
        FieldInput = text;
    }
    // Update is called once per frame
    void Update()
    {
        ui.SetActive(active);
        output.text = FieldOutput;
        if (active)
        {
            field.onValueChanged.AddListener(OnDeselect);
            if (Input.GetKeyUp(KeyCode.RightShift))
            {
                SendCommand(FieldInput);
                field.text = "";
                FieldInput = "";
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            active = !active;
        }
    }
}
