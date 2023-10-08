using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI stat_1;
    public TextMeshProUGUI stat_2;
    public TextMeshProUGUI num_1;     
    public TextMeshProUGUI num_2;
    public TextMeshProUGUI Description;

    public void SetupTooltip(string name, string stat1, string stat2 , int num, int num2, string des)
    {
        Debug.Log(name);
        Debug.Log(stat1);
        Debug.Log(stat2);
        Debug.Log(num);
        Debug.Log(num2);
        Debug.Log(des);
       
        itemname.text= name;
        stat_1.text = stat1;
        stat_2.text = stat2;
        num_1.text = num.ToString();
        num_2.text = num2.ToString();
        Description.text = des;

      
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
