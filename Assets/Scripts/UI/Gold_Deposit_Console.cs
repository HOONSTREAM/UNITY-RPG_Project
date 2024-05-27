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
    public GameObject amountInputConsole; //�ش� ��� �Է� â
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


        GameObject player = Managers.Game.GetPlayer(); //Find �Լ��� ����.
        PlayerStat stat = player.GetComponent<PlayerStat>();

        if (stat.Gold < inputamount)
        {
            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("���� ���� ��尡 �����մϴ�.");
            amountInputConsole.SetActive(false);
            return;
        }

        stat.Gold -= inputamount;
        stat.onchangestat.Invoke();
        StorageGoldAmount += inputamount;

        Storage_gold_text.text = StorageGoldAmount.ToString();

        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"{inputamount}��带 �ð� ���� �������� ���� {stat.Gold} ��� �Դϴ�. ");

        inputamount = 0; // �Է°� �ʱ�ȭ


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
