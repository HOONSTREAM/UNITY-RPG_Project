using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base


{
    string _name;

     enum Gameobjects
    {
        ItemIcon,   
        ItemnameText,

    }

    public override void Init()
    {
        
        Bind<GameObject>(typeof(Gameobjects));
       
        Get<GameObject>((int)Gameobjects.ItemnameText).GetComponent<Text>().text = _name;

        Get<GameObject>((int)Gameobjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"아이템 클릭{_name}"); });

    }


    public void SetInfo(string name)
    {
        _name = name;
    }


}
