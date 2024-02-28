using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonQuite : MonoBehaviour
{
    public Button button;
    public bool quit;


    private void OnButton()
    {
        if (!quit)
        {
            Application.Quit();
            quit = true;
        }
    }
    void Update()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnButton);
        }
        else
        {
            button = GetComponent<Button>();
        }
    }
}
