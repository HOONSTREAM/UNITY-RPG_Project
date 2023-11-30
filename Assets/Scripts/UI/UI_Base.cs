using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _object = new Dictionary<Type, UnityEngine.Object[]>();
    // ������� Button �̶�� Dictionary<Button, PointButton> ������ key�� value�� �迭�� �����ϰ� ���� ��.

    public abstract void Init(); //�߻��Լ� ����

    private void Start()
    {
        Init();

    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); //string�� �迭�� ��ȯ�̵�. ������� Text��� Point_Text�� Score_Text�� string �迭�� ������ �ȴ�.

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // Text�� �޾Ҵٸ� �迭�� 2���� �ȴ�. Dictionary�� value�� ������� �迭
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

        return objects[idx] as T; //objects �迭�� T�� ĳ�����Ͽ� ��ȯ

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
