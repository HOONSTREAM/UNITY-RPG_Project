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

        //���� �Ǹ� ���� 
        //TODO : �˻� �ʿ� (����� ������ �ִ���, �� , â ������ �ʱ�ȭ, ���� �����ǰ���ħ ���������۳��� equip �Ҹ����� ���� üũ�Ǵ� ���� )
        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        for(int i = 0; i<inputamount ; i++)
        {
            stat.Gold += sell_console.slot_item.sellprice;
            totalsellgold += sell_console.slot_item.sellprice;
        }
        
        stat.PrintUserText($"������ �Ǹ��Ͽ�{totalsellgold}��带 ������ϴ�.");
        PlayerInventory.Instance.RemoveItem(sell_console.slot_number);

        totalsellgold = 0;
    }

}
