using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{

  
    enum Buttons
    {
        Point_Button
    }

    enum Texts
    {
        
        Score_Text,

    }

    enum Gameobjects
    {
        TestObject,
        //BackGround,

    }

    enum Images
    {
        //ItemIcon,

    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons)); //Button 컴포넌트를 찾아서 enum값을 인자로 넘겨주겠다.
        Bind<Text>(typeof(Texts)); //Text 컴포넌트를 찾아서 enum값을 인자로 넘겨주겠다.
        Bind<GameObject>(typeof(Gameobjects)); //Gameobject 를 찾아서 enum 값을 인자로 넘겨주겠다.
       //Bind<Image>(typeof(Images));

       

        //버튼클릭 이벤트
        GetButton((int)Buttons.Point_Button).gameObject.BindEvent(OnButtonClicked);
      
        
        ////드래그 이벤트
        //GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        //BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);


    }

    public void OnButtonClicked(PointerEventData data)
    {


    }


}
