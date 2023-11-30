using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _object = new Dictionary<Type, UnityEngine.Object[]>();
    // 예를들어 Button 이라면 Dictionary<Button, PointButton> 식으로 key와 value를 배열로 보관하고 있을 것.

    public abstract void Init(); //추상함수 정의

    private void Start()
    {
        Init();

    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); //string의 배열로 반환이됨. 예를들어 Text라면 Point_Text와 Score_Text가 string 배열로 저장이 된다.

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // Text를 받았다면 배열이 2개가 된다. Dictionary의 value를 담기위한 배열
        _object.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null)
                Debug.Log($"Failed to bind !{names[i]}");

        }

    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_object.TryGetValue(typeof(T), out objects) == false)
        {
            return null;
        }

        return objects[idx] as T; //objects 배열을 T로 캐스팅하여 반환

    }
    protected GameObject GetGameobject(int idx)
    {
        return Get<GameObject>(idx);    
    }
    protected Text GetText(int idx)
    {
        return Get<Text>(idx);
    }

    protected Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }

    protected Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }

    public static void BindEvent(GameObject go, Action<PointerEventData> action,Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetorAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;

                break;

        }
       
    }

}
