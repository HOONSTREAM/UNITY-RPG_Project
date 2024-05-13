using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using Unity.Burst.Intrinsics;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI stat_1;
    public TextMeshProUGUI stat_2;
    public TextMeshProUGUI stat_3;
    public TextMeshProUGUI stat_4;

    public TextMeshProUGUI num_1;     
    public TextMeshProUGUI num_2;
    public TextMeshProUGUI num_3;
    public TextMeshProUGUI num_4;

    public TextMeshProUGUI item_rank;
    public TextMeshProUGUI Description;

    public void SetupTooltip(string name, string stat1, string stat2 , string stat3, string stat4,int num, int num2, int num3, int num4, string rank, string des)
    {
        
        itemname.text= name;
        stat_1.text = stat1;
        stat_2.text = stat2;
        stat_3.text = stat3;
        stat_4.text = stat4;

        num_1.text = num.ToString();
        num_2.text = num2.ToString();
        num_3.text = num3.ToString();
        num_4.text = num4.ToString();

        switch (rank)
        {
            case "Common":
                rank = "커먼";
                break;
            case "UnCommon":
                rank = "언커먼";
                break;
            case "Rare":
                rank = "레어";
                break;
            case "Unique":
                rank = "유니크";
                break;
            case "Legend":
                rank = "전설";
                break;

        }

        item_rank.text = rank;
        
        Description.text = des;
    
    }

    public void Setup_skillBook(string name, string rank, string des)
    {
        itemname.text = name;

        switch (rank)
        {
            case "Common":
                rank = "커먼";
                break;
            case "UnCommon":
                rank = "언커먼";
                break;
            case "Rare":
                rank = "레어";
                break;
            case "Unique":
                rank = "유니크";
                break;
            case "Legend":
                rank = "전설";
                break;

        }

        item_rank.text = rank;
        Description.text = des;

    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
