using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �絧�þ� ������ ��ξƿ� ���� ��ũ��Ʈ �Դϴ�.
/// </summary>
public class NPCscript : MonoBehaviour
{

    

    private int _mask = (1 << (int)Define.Layer.NPC);
    private const string NPC_TAG = "Broa_NPC";
    GameObject _player;
    #region NPC��ȭ

    /*=========NPC��ȭ ���� ����===============*/
   
    [SerializeField]
    GameManager gamemanager;
    [SerializeField]
    Button endbutton;
    /*=========================================*/
    #endregion

   
    void Awake()
    {
        _player = Managers.Game.GetPlayer();
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
                if (hit.collider.CompareTag(NPC_TAG))
                {
                    PlayerController pc = _player.GetComponent<PlayerController>();
                    pc.State = Define.State.Idle;

                    GameObject go = GameObject.Find("SHOP CANVAS").gameObject;

                    NPC1_shop shop = go.gameObject.GetComponent<NPC1_shop>();
                    shop.Enter();
                    Managers.Sound.Play("Inven_open");
                }

            }
        }
    }
}

