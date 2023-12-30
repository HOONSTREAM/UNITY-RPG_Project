using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Skill_ToolTip : MonoBehaviour
{
    public Image skill_image;
    public TextMeshProUGUI skill_name;
    public TextMeshProUGUI stat_1;
    public TextMeshProUGUI stat_2;
    public TextMeshProUGUI num_1;
    public TextMeshProUGUI num_2;
    public TextMeshProUGUI Description;

    public void SetupTooltip(string name, string stat1, string stat2, int num, int num2, string des, Sprite image) // ���� �¾�
    {

        skill_name.text = name; // ���� �̸�
        stat_1.text = stat1; // ����ȿ�� 
        stat_2.text = stat2; // ����ȿ�� 2
        num_1.text = num.ToString(); // ����ȿ�� ��ġ
        num_2.text = num2.ToString(); // ����ȿ�� ��ġ2
        Description.text = des; // ���� ����
        skill_image.sprite = image; // ���� �̹��� 

    }

    public void SetupAbillityToolTip(string name, string stat1, int num, string des, Sprite image) // �����Ƽ �¾�
    {
        skill_name.text = name; // �����Ƽ �̸� 
        stat_1.text = stat1;  // ���ݷ��� �󸶳� ��ȭ�Ǿ����� ��Ÿ��
        num_1.text = num.ToString(); //���ݷ� ��ȭ��ġ
        Description.text = des; // �����Ƽ�� ���� ����
        skill_image.sprite = image; // �����Ƽ �̹���
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
