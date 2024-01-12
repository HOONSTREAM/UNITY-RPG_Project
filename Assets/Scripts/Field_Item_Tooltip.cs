using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Field_Item_Tooltip : MonoBehaviour
{
    
    public TextMeshProUGUI Item_name;

    public void SetupToolTip(string name)
    {
        Item_name.text = name;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
