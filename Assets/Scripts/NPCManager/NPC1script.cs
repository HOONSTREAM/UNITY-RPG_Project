using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC1script : MonoBehaviour
{
    //대장슬라임 케넨

    private int _mask = (1 << (int)Define.Layer.NPC1);
    Texture2D _attackIcon;
    GameObject _player;

    #region NPC대화
    /*=========NPC대화 관련 변수===============*/
   
    [SerializeField]
    GameManager gamemanager;
    [SerializeField]
    Button endbutton;

    /*=========================================*/
    #endregion

   
    void Start()
    {
        _player = Managers.Resources.Load<GameObject>("PreFabs/UnityChan"); // 플레이어 세팅 
        _attackIcon = Managers.Resources.Load<Texture2D>("Textures/Cursor/cursor(10)");
    }
    
    void Update()
    {
        OnNPCTalking();
    }

    private void OnNPCTalking()
    {
        if (Input.GetMouseButtonUp(0) == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f, _mask))
            {
                if (hit.collider.gameObject.layer == (int)Define.Layer.NPC1)
                {                   
                    Shop shop = GetComponent<Shop>();
                    shop.Enter();
                    Managers.Sound.Play("Inven_open");    
                }

            }
        }

        return;
    }
}

