using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC2_Script : MonoBehaviour
{
    private int _mask = (1 << (int)Define.Layer.NPC2);
    Texture2D _attackIcon;
    GameObject _player;
   

    void Awake()
    {
        _player = Managers.Resources.Load<GameObject>("PreFabs/UnityChan"); // �÷��̾� ���� 
        
    }

    
    void Update()
    {
        
       OnNPCTalking();
        
    }

    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();


        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }


    private void OnNPCTalking()
    {
        if (Input.GetMouseButtonUp(0) == true)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f, _mask))
            {
                if (hit.collider.gameObject.layer == (int)Define.Layer.NPC2)
                {

                    PlayerController pc = _player.GetComponent<PlayerController>();
                    pc.State = Define.State.Idle;
                    Debug.Log($"{hit.collider.gameObject.name}��(��) Ŭ���մϴ�.!");

                    GameObject go = GameObject.Find("Storage CANVAS").gameObject;
                    Storage_Script storage = go.GetComponent<Storage_Script>();
                    storage.Enter();

                }

            }

        }

        return;
    }

        
 }

