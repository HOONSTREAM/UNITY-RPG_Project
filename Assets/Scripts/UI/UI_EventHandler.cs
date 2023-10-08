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



    public void OnDrag(PointerEventData eventData) // IDragHandler 를 상속받으므로, 마우스가 드래그되면 자동으로 실행되는 함수이다.
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);

    }

    public void OnPointerClick(PointerEventData eventData) // IPointerClickHandler를 상속받으므로, 마우스가 클릭이 되면 자동으로 실행되는 함수이다.
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

   
   

}

