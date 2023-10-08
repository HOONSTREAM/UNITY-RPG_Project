using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven : UI_Scene
{ 
    enum Gameobjects
    {
        GridPanel,

    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(Gameobjects));

        GameObject gridpanel = Get<GameObject>((int)Gameobjects.GridPanel);
        foreach (Transform child in gridpanel.transform)
        {
            Managers.Resources.Destroy(child.gameObject);
        }

            //실제 인벤토리를 참고해서
            for(int i = 0; i<9; i++)
            {
              GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridpanel.transform).gameObject;  //인자로 부모님 지정


              UI_Inven_Item invenitem = item.GetAddComponent<UI_Inven_Item>(); //프리펩에 컴포넌트 추가
              invenitem.SetInfo($"집행검{i}번");  //이름 

            }

        //드래그 이벤트
        
        BindEvent(gridpanel, (PointerEventData data) => { gridpanel.transform.position = data.position; }, Define.UIEvent.Drag);
    }

}


