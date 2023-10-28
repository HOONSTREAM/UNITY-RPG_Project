using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Drop_Input_Console : MonoBehaviour
{
    public TMP_InputField inputamounttext;
    public int inputamount;
    private GameObject canvas; //스크립트가 등록되어있는 캔버스
    private Slot_Equip_Drop slot_equip_drop;
    public GameObject amountInputConsole;
    // Start is called before the first frame update
    private void Awake()
    {
        canvas = GameObject.Find("NewInvenUI").gameObject;
        slot_equip_drop = canvas.GetComponent<Slot_Equip_Drop>();

    }

    public void Input()
    {
        inputamount = int.Parse(inputamounttext.text);
        inputamounttext.text = "";
        slot_equip_drop.amount = inputamount;

        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();

        if (slot_equip_drop.slot_item.amount < inputamount)
        {
            stat.PrintUserText("소지 중인 물품의 갯수가 부족합니다.");
            amountInputConsole.SetActive(false);
            return;
        }

        for (int i = 0; i < inputamount; i++)
        {
            PlayerInventory.Instance.RemoveItem(slot_equip_drop.slot_number);
        }
        stat.PrintUserText("소비 아이템을 버립니다.");
        amountInputConsole.SetActive(false);
    }
}
