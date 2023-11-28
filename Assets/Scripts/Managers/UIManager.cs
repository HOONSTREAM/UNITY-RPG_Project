using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{ 
    int _order = 10;
    // SortOrder�� �����ϱ� ����. �� �������� �������� �������� ������ ��.

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
    public void SetCanvas(GameObject go, bool sort = true) // â�� �����ǵ�, �켱������ ���ϴ� ��
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
    public T MakeWorldSpaceUI<T>(Transform parent, string name = null) where T : UI_Base //Transform�� ���ڷ� �޴� ���� SetParent �Լ��� ��ġ�� ���ؼ�
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resources.Instantiate($"UI/WorldSpace/{name}"); //������ ����

        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetAddComponent<T>(go); //script component �߰��ؼ� ����
    }

    public T MakeSubItem<T>(Transform parent , string name = null) where T : UI_Base //Transform�� ���ڷ� �޴� ���� SetParent �Լ��� ��ġ�� ���ؼ�
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resources.Instantiate($"UI/SubItem/{name}"); //������ ����

        if (parent != null)
            go.transform.SetParent(parent);
            

        return Util.GetAddComponent<T>(go); //script component �߰��ؼ� ����
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name)) //�̸��� ���ٸ� ������Ʈ�� �̸��� �̸����ٰ� ���� 
            name = typeof(T).Name;

        GameObject go = Managers.Resources.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetAddComponent<T>(go);
        _scene = sceneUI; //_scene������ sceneUI�� �ִ��۾�
  
         go.transform.SetParent(Root.transform);

        return sceneUI;

    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name)) //�̸��� ���ٸ� ������Ʈ�� �̸��� �̸����ٰ� ���� 
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

        if(_popupstack.Peek() != popup) // ������ �� ���� Ȯ�� 
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


