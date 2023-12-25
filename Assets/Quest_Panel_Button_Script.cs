using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Panel_Button_Script : MonoBehaviour
{
    public GameObject Quest_Panel;

    public bool active_quest_panel = false;


    public void Button_Function()
    {
        active_quest_panel = !active_quest_panel;
        Quest_Panel.SetActive(active_quest_panel);
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
