using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action OnChangeItem = null;



    public void OnDrag(PointerEventData eventData) // IDragHandler �� ��ӹ����Ƿ�, ���콺�� �巡�׵Ǹ� �ڵ����� ����Ǵ� �Լ��̴�.
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);

    }

    public void OnPointerClick(PointerEventData eventData) // IPointerClickHandler�� ��ӹ����Ƿ�, ���콺�� Ŭ���� �Ǹ� �ڵ����� ����Ǵ� �Լ��̴�.
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

   
   

}

