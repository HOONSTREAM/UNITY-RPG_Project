using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
       UI_Base.BindEvent(go, action, type);
    }

    public static T GetAddComponent<T>(this GameObject go) where T : Component
    {
       
        return Util.GetorAddComponent<T>(go);
    }

    public static bool IsValid (this GameObject go)
    {
        return go != null && go.activeSelf;

    }
}
