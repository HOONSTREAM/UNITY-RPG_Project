using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 루덴시안의 보석상 케일의 스크립트 입니다.
/// </summary>
public class Jewel_NPC_Script : MonoBehaviour
{
    private int _mask = (1 << (int)Define.Layer.NPC);
    private const string NPC_TAG = "Jewel_NPC";
    GameObject _player;

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
                    GameObject go = GameObject.Find("SHOP CANVAS").gameObject;

                    NPC2_shop shop = go.gameObject.GetComponent<NPC2_shop>();
                    shop.Enter();
                    Managers.Sound.Play("Inven_open");
                }

            }
        }

        return;
    }

}
