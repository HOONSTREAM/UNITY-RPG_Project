using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 루덴시안 상점의 케넨에 대한 스크립트 입니다.
/// </summary>
public class NPC1script : MonoBehaviour
{
  

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
        _player = Managers.Game.GetPlayer();
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
                    GameObject go = GameObject.Find("SHOP CANVAS").gameObject;
                   
                    Shop shop = go.gameObject.GetComponent<Shop>();
                    shop.Enter();
                    Managers.Sound.Play("Inven_open");    
                }

            }
        }

        return;
    }
}

