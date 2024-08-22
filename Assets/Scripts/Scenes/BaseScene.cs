using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour //��� Scene�� �ֻ��� �θ�
{
    // Awake�� ��� ������Ʈ�� �ʱ�ȭ�ǰ� ȣ��Ǳ� ������ �ٸ� ��ũ��Ʈ�� ������ ������ �� �ִ�.
    
    private void Awake() 
    {
        Init();
    }

    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    
    protected virtual void Init() //EventSystem object�� ������ UI�� Ȱ��ȭ ���� �ʴ´�.
    {
        
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if (obj == null)
            Managers.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";
      
    }

    public abstract void Clear();
  
}
