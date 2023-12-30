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

    public void SetupTooltip(string name, string stat1, string stat2, int num, int num2, string des, Sprite image) // 스펠 셋업
    {

        skill_name.text = name; // 스펠 이름
        stat_1.text = stat1; // 스펠효과 
        stat_2.text = stat2; // 스펠효과 2
        num_1.text = num.ToString(); // 스펠효과 수치
        num_2.text = num2.ToString(); // 스펠효과 수치2
        Description.text = des; // 스펠 설명
        skill_image.sprite = image; // 스펠 이미지 

    }

    public void SetupAbillityToolTip(string name, string stat1, int num, string des, Sprite image) // 어빌리티 셋업
    {
        skill_name.text = name; // 어빌리티 이름 
        stat_1.text = stat1;  // 공격력이 얼마나 강화되었는지 나타냄
        num_1.text = num.ToString(); //공격력 강화수치
        Description.text = des; // 어빌리티에 대한 설명
        skill_image.sprite = image; // 어빌리티 이미지
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
