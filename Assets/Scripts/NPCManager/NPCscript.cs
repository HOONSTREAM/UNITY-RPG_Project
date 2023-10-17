using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCscript : MonoBehaviour
{
    private int _mask = (1 << (int)Define.Layer.NPC);
    Texture2D _attackIcon;
    GameObject _player;
    #region NPC��ȭ
    /*=========NPC��ȭ ���� ����===============*/
   
    [SerializeField]
    GameManager gamemanager;
    [SerializeField]
    Button endbutton;
    /*=========================================*/
    #endregion

   
    void Start()
    {
        _player = Managers.Resources.Load<GameObject>("PreFabs/UnityChan"); // �÷��̾� ���� 

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
                if (hit.collider.gameObject.layer == (int)Define.Layer.NPC)
                {

                    PlayerController pc = _player.GetComponent<PlayerController>();
                    pc.State = Define.State.Idle;
                    Debug.Log("NPC�� Ŭ���մϴ�.!");
                    gamemanager.SelectedNPC = gameObject;

                    // gamemanager.TalkAction(); //�� ������Ʈ�� �پ��ִ� ���ӿ�����Ʈ�� scanObject�� ���ڷ� �Ѱ��ش�.

                    NPC1_shop shop = GetComponent<NPC1_shop>();
                    shop.Enter();
                    Managers.Sound.Play("Inven_open");



                }

            }
        }
    }
}

