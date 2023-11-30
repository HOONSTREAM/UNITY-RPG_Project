using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class Gold_Deposit_Console : MonoBehaviour
{

    public TMP_InputField inputamounttext;
    public int inputamount;
    private GameObject canvas; //스크립트가 등록되어있는 캔버스
    public GameObject amountInputConsole; //해당 골드 입력 창
    public TextMeshProUGUI Storage_gold_text;

    public int StorageGoldAmount;
    void Awake()
    {
        canvas = GameObject.Find("StorageUI").gameObject;
    }

    
    public void Input()
    {
        StorageGoldAmount = int.Parse(Storage_gold_text.text);

        inputamount = int.Parse(inputamounttext.text);
        inputamounttext.text = "";


        GameObject player = Managers.Game.GetPlayer(); //Find 함수는 느림.
        PlayerStat stat = player.GetComponent<PlayerStat>();

        if (stat.Gold < inputamount)
        {
            stat.PrintUserText("소지 중인 골드가 부족합니다.");
            amountInputConsole.SetActive(false);
            return;
        }

        stat.Gold -= inputamount;
        stat.onchangestat.Invoke();
        StorageGoldAmount += inputamount;

        Storage_gold_text.text = StorageGoldAmount.ToString();

        stat.PrintUserText($"{inputamount}골드를 맡겨 현재 소지중인 골드는 {stat.Gold} 골드 입니다. ");

        inputamount = 0; // 입력값 초기화

        amountInputConsole.SetActive(false);

        return;
    }
   
}
