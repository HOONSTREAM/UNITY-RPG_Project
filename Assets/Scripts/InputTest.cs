using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputTest : MonoBehaviour
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

        //실제 판매 과정 
        //TODO : 검사 필요 (충분한 갯수가 있는지, 등 , 창 종료후 초기화, 전설 여명의가르침 같은아이템끼리 equip 불리언이 같이 체크되는 버그 )
        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        for(int i = 0; i<inputamount ; i++)
        {
            stat.Gold += sell_console.slot_item.sellprice;
            totalsellgold += sell_console.slot_item.sellprice;
        }
        
        stat.PrintUserText($"상점에 판매하여{totalsellgold}골드를 얻었습니다.");
        PlayerInventory.Instance.RemoveItem(sell_console.slot_number);

        totalsellgold = 0;
    }

}
