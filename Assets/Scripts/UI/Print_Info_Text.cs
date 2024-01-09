using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Print_Info_Text : MonoBehaviour
{

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
