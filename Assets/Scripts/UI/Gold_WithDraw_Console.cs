using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class Gold_WithDraw_Console : MonoBehaviour
{
    public TMP_InputField inputamounttext;
    public int inputamount;
    private GameObject canvas; //��ũ��Ʈ�� ��ϵǾ��ִ� ĵ����
    public GameObject amountInputConsole; //�ش� ��� �Է� â
    public TextMeshProUGUI Storage_gold_text;

    public int StorageGoldAmount;

    void Awake()
    {
        canvas = GameObject.Find("StorageUI").gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            gold_withdraw();
        }
    }

    public void gold_withdraw()
    {
        StorageGoldAmount = int.Parse(Storage_gold_text.text);

        inputamount = int.Parse(inputamounttext.text);
        inputamounttext.text = "";


        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

        if (StorageGoldAmount < inputamount)
        {
            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("ã���� �ϴ� ��尡 �ð��� �ִ� ��庸�� �����ϴ�.");
            amountInputConsole.SetActive(false);
            return;
        }

        StorageGoldAmount -= inputamount;
        stat.Gold += inputamount;
        stat.onchangestat.Invoke();
        Storage_gold_text.text = StorageGoldAmount.ToString();

        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"{inputamount}��带 ã�� ���� �������� ���� {stat.Gold} ��� �Դϴ�. ");

        inputamount = 0; // �Է°� �ʱ�ȭ

        amountInputConsole.SetActive(false);

        return;
    }


}
