using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public void Input()
    {
        StorageGoldAmount = int.Parse(Storage_gold_text.text);

        inputamount = int.Parse(inputamounttext.text);
        inputamounttext.text = "";


        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();

        if (StorageGoldAmount < inputamount)
        {
            stat.PrintUserText("ã���� �ϴ� ��尡 �ð��� �ִ� ��庸�� �����ϴ�.");
            amountInputConsole.SetActive(false);
            return;
        }

        StorageGoldAmount -= inputamount;
        stat.Gold += inputamount;

        Storage_gold_text.text = StorageGoldAmount.ToString();

        stat.PrintUserText($"{inputamount}��带 ã�� ���� �������� ���� {stat.Gold} ��� �Դϴ�. ");

        inputamount = 0; // �Է°� �ʱ�ȭ

        amountInputConsole.SetActive(false);

        return;
    }


}
