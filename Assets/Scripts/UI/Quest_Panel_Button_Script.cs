using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Panel_Button_Script : MonoBehaviour
{
    public GameObject Quest_Panel;
    public GameObject Quest_CANVAS;

    public bool active_quest_panel = false;


    public void Button_Function()
    {
        active_quest_panel = !active_quest_panel;
        Quest_Panel.SetActive(active_quest_panel);
        Managers.UI.SetCanvas(Quest_CANVAS, true); // 캔버스 SortOrder 순서를 열릴때 마다 정의함. (제일 마지막에 열린것이 가장 위로)
        Managers.Sound.Play("Inven_Open");

        return;

    }

    public void Exit_Button()
    {
        if (Quest_Panel.activeSelf)
        {
            Quest_Panel.SetActive(false);
            Managers.Sound.Play("Inven_Open");

        }

        return;

    }
}
