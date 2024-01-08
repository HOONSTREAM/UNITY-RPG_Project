using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gold_WithDraw_Console : MonoBehaviour
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


        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

        if (StorageGoldAmount < inputamount)
        {
            Managers.Game.PrintUserText("찾고자 하는 골드가 맡겨져 있는 골드보다 많습니다.");
            amountInputConsole.SetActive(false);
            return;
        }

        StorageGoldAmount -= inputamount;
        stat.Gold += inputamount;
        stat.onchangestat.Invoke();
        Storage_gold_text.text = StorageGoldAmount.ToString();

        Managers.Game.PrintUserText($"{inputamount}골드를 찾아 현재 소지중인 골드는 {stat.Gold} 골드 입니다. ");

        inputamount = 0; // 입력값 초기화

        amountInputConsole.SetActive(false);

        return;
    }


}
