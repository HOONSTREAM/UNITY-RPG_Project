using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Print_Info_Text : MonoBehaviour
{

    private static Print_Info_Text s_instance; 
    public static Print_Info_Text Instance { get { Init(); return s_instance; } } 

    private static void Init()
    {
        GameObject GUI_Interface = GameObject.Find("GUI_User_Interface").gameObject;

        if(GUI_Interface == null)
        {
            GUI_Interface = new GameObject { name = "@Print_Info_Text_Object" };
            GUI_Interface.AddComponent<Print_Info_Text>();
            DontDestroyOnLoad(GUI_Interface);

        }
        s_instance = GUI_Interface.GetComponent<Print_Info_Text>();

    }

    #region PrintUserText
    private void TextClear()
    {

        GameObject text = GameObject.Find("Text_User").gameObject;
        text.GetComponent<TextMeshProUGUI>().text = " ";
    }

    public void PrintUserText(string Input)
    {
        GameObject text = GameObject.Find("Text_User").gameObject;
        text.GetComponent<TextMeshProUGUI>().text = Input;
        Managers.Sound.Play("Coin", Define.Sound.Effect);

        Invoke("TextClear", 3.0f);

        return;
    }
    #endregion


}
