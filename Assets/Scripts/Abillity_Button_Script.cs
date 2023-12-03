using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abillity_Button_Script : MonoBehaviour
{

    public GameObject Abillity_Panel;

    public bool active_abillity_panel = false;


    public void Button_Function()
    {
        active_abillity_panel = !active_abillity_panel;
        Abillity_Panel.SetActive(active_abillity_panel);
        Managers.Sound.Play("Inven_Open");

        return;

    }
}
