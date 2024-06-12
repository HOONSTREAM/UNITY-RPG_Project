using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputConsole : MonoBehaviour
{
    public TMP_InputField inputamounttext;
    public int inputamount;
    private GameObject canvas; //��ũ��Ʈ�� ��ϵǾ��ִ� ĵ����
    private Sell_Console sell_console;
    public GameObject amountinput_console;

    private int totalsellgold = 0;

    private void Awake()
    {
        canvas = GameObject.Find("GUI_Console").gameObject;
        sell_console = canvas.GetComponent<Sell_Console>();
    }

    public void Input()
    {
        
        inputamount = int.Parse(inputamounttext.text);
        inputamounttext.text = "";
        
        sell_console.amount = inputamount;


        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        
        if(sell_console.slot_item.amount < inputamount)
        {
            amountinput_console.SetActive(false);
            Print_Info_Text.Instance.PrintUserText("���� ���� ��ǰ�� ������ �����մϴ�.");
            
            return;
        }

        
        for(int i = 0; i<inputamount ; i++)
        {
            stat.Gold += sell_console.slot_item.sellprice;
            stat.onchangestat.Invoke();
            totalsellgold += sell_console.slot_item.sellprice;
        }
        amountinput_console.SetActive(false);
        Print_Info_Text.Instance.PrintUserText($"������ �Ǹ��Ͽ�{totalsellgold}��带 ������ϴ�.");


        for(int i = 0; i<inputamount; i++)
        {
            PlayerInventory.Instance.RemoveItem(sell_console.slot_number);
        }
       
        totalsellgold = 0;

        
    }

}
