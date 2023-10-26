using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputConsole : MonoBehaviour
{
    public TMP_InputField inputamounttext;
    public int inputamount;
    private GameObject console;
    private Sell_Console sell_console;
    public GameObject amountinput_console;

    private int totalsellgold = 0;

    private void Awake()
    {
        console = GameObject.Find("GUI").gameObject;
        sell_console = console.GetComponent<Sell_Console>();
    }

    public void Input()
    {
        
        inputamount = int.Parse(inputamounttext.text);
        inputamounttext.text = "";
        
        sell_console.amount = inputamount;

       
        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        
        if(sell_console.slot_item.amount < inputamount)
        {
            stat.PrintUserText("소지 중인 물품의 갯수가 부족합니다.");
            return;
        }

        
        for(int i = 0; i<inputamount ; i++)
        {
            stat.Gold += sell_console.slot_item.sellprice;
            totalsellgold += sell_console.slot_item.sellprice;
        }
        
        stat.PrintUserText($"상점에 판매하여{totalsellgold}골드를 얻었습니다.");


        for(int i = 0; i<inputamount; i++)
        {
            PlayerInventory.Instance.RemoveItem(sell_console.slot_number);
        }
       
        totalsellgold = 0;

        amountinput_console.SetActive(false);
    }

}
