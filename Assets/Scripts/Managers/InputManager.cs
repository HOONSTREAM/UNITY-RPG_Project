using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//MonoBehaviour 상속을 받지않음으로써 컴포넌트가 아닌 일반 C# script로 사용한다.
//Input을 관리하는 매니저
public class InputManager
{
    public Action KeyAction = null; //델리게이트 (옵저버패턴 -> 입력이되는지 감시 후 입력되면 이벤트발생)
    public Action<Define.MouseEvent> MouseAction = null;
    bool _pressed = false;
    float _pressedTime = 0;

   public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
          return;
     
        if (Input.anyKey && KeyAction != null)
        {
            KeyAction.Invoke();
        }

        if(MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;

            }

            else
            {
                if (_pressed)
                {
                    if(Time.time < _pressedTime + 0.2f)
                    {
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    }

                    MouseAction.Invoke(Define.MouseEvent.PointerUp);

                }
                    
                _pressed = false;
                _pressedTime = 0; // 마우스를 뗐으므로 초기화
            }
        }


    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }

}
