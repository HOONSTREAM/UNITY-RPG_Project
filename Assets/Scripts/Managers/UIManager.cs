using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{ 
    int _order = 10;
    // SortOrder를 관리하기 위함. 맨 마지막에 열린것이 마지막에 닫히는 것.

    Stack<UI_Popup> _popupstack = new Stack<UI_Popup>();
    UI_Scene _scene = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");

            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };

            }

            return root;
        }
    }
    public void SetCanvas(GameObject go, bool sort = true) // 창이 열릴건데, 우선순위를 정하는 것
    {
        Canvas canvas = Util.GetAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;

        }

        else
        {
            canvas.sortingOrder = 0;
        }
            
    }
    public T MakeWorldSpaceUI<T>(Transform parent, string name = null) where T : UI_Base //Transform을 인자로 받는 것은 SetParent 함수와 합치기 위해서
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resources.Instantiate($"UI/WorldSpace/{name}"); //프리펩 생성

        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetAddComponent<T>(go); //script component 추가해서 리턴
    }

    public T MakeSubItem<T>(Transform parent , string name = null) where T : UI_Base //Transform을 인자로 받는 것은 SetParent 함수와 합치기 위해서
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resources.Instantiate($"UI/SubItem/{name}"); //프리펩 생성

        if (parent != null)
            go.transform.SetParent(parent);
            

        return Util.GetAddComponent<T>(go); //script component 추가해서 리턴
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name)) //이름이 없다면 컴포넌트의 이름을 이름에다가 대입 
            name = typeof(T).Name;

        GameObject go = Managers.Resources.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetAddComponent<T>(go);
        _scene = sceneUI; //_scene변수에 sceneUI를 넣는작업
  
         go.transform.SetParent(Root.transform);

        return sceneUI;

    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name)) //이름이 없다면 컴포넌트의 이름을 이름에다가 대입 
            name = typeof(T).Name;

       GameObject go =  Managers.Resources.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetAddComponent<T>(go);
        _popupstack.Push(popup);

        go.transform.SetParent(Root.transform);
  
       return popup;

    }

    public void ClosepopupUI(UI_Popup popup)
    {
        if (_popupstack.Count == 0)
            return;

        if(_popupstack.Peek() != popup) // 스택의 맨 윗값 확인 
        {
            Debug.Log("Close popup Failed !");
            return;

        }

        ClosepopupUI();
    }

    public void ClosepopupUI()
    {
        if (_popupstack.Count == 0)
            return;

     
        UI_Popup popup = _popupstack.Pop();
        Managers.Resources.Destroy(popup.gameObject);

        popup = null;


    }

    public void CloseAllpopupUI()
    {
        while( _popupstack.Count > 0 )
        {
            ClosepopupUI();
        }
    }


    public void Clear()
    {
        CloseAllpopupUI();
        _scene = null;
    }
}


