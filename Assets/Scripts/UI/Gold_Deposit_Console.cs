using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.EventSystems;
using Input = UnityEngine.Input;

public class Gold_Deposit_Console : MonoBehaviour
{

    public TMP_InputField inputamounttext;
    public int inputamount;
    public GameObject amountInputConsole; //해당 골드 입력 창
    [SerializeField]
    private TextMeshProUGUI Storage_gold_text;

    public int StorageGoldAmount;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            amount_Input();
        }
    }

    public void amount_Input()
    {
        StorageGoldAmount = int.Parse(Storage_gold_text.text);

        inputamount = int.Parse(inputamounttext.text);
        inputamounttext.text = "";


        GameObject player = Managers.Game.GetPlayer(); //Find 함수는 느림.
        PlayerStat stat = player.GetComponent<PlayerStat>();

        if (stat.Gold < inputamount)
        {
            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("소지 중인 골드가 부족합니다.");
            amountInputConsole.SetActive(false);
            return;
        }

        stat.Gold -= inputamount;
        stat.onchangestat.Invoke();
        StorageGoldAmount += inputamount;

        Storage_gold_text.text = StorageGoldAmount.ToString();

        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"{inputamount}골드를 맡겨 현재 소지중인 골드는 {stat.Gold} 골드 입니다. ");

        inputamount = 0; // 입력값 초기화


        PlayerStorage.Instance.Storage_Gold = StorageGoldAmount;

        amountInputConsole.SetActive(false);

        return;
    }

    public void Load_Storage_Gold(int amount)
    {
        Storage_gold_text.text = amount.ToString();

        return;
    }
   
}
