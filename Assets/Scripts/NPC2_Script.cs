using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC2_Script : MonoBehaviour , IPointerClickHandler
{
    private int _mask = (1 << (int)Define.Layer.NPC2);
    Texture2D _attackIcon;
    GameObject _player;
   

    void Awake()
    {
        _player = Managers.Resources.Load<GameObject>("PreFabs/UnityChan"); // 플레이어 세팅 
        
    }

 /*IPointer 인터페이스를 사용하고, MainCamera Physics Raycaster를 활용하면 UI 뒤 3D오브젝트를 뚫지않게끔 할수 있다.*/
    public void OnPointerClick(PointerEventData eventData)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.NPC2)
            {

                PlayerController pc = _player.GetComponent<PlayerController>();
                pc.State = Define.State.Idle;
                Debug.Log($"{hit.collider.gameObject.name}을(를) 클릭합니다.!");

                GameObject go = GameObject.Find("Storage CANVAS").gameObject;
                Storage_Script storage = go.GetComponent<Storage_Script>();
                storage.Enter();

            }

        }
    }
}

