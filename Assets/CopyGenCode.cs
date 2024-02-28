using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEngine.UI;

public class CopyGenCode : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    
    public bool active;
    void Click()
    {
        if (!active)
        {

            if (BackingInterface.organism != null)
            {
                active = true;
                GUIUtility.systemCopyBuffer= BackingInterface.organism.Gen;
                ShowTextBar.ShowText("Ген-код скопирован!");

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            button.onClick.AddListener(Click);
        }
        else
        {
            active = false;
        }
    }
}




