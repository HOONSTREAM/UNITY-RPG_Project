using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour //모든 Scene의 최상위 부모
{
    // Awake는 모든 오브젝트가 초기화되고 호출되기 때문에 다른 스크립트와 연결을 보장할 수 있다.
    
    private void Awake() 
    {
        Init();
    }

    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    
    protected virtual void Init() //EventSystem object가 없으면 UI가 활성화 되지 않는다.
    {
        
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if (obj == null)
            Managers.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";
      
    }

    public abstract void Clear();
  
}
