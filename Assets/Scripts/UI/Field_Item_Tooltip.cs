using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Field_Item_Tooltip : MonoBehaviour
{

    // 현재 표시되고 있는 FieldItem의 참조를 저장하는 프로퍼티를 추가합니다.
    public FieldItem CurrentItem { get; private set; }

    public TextMeshProUGUI Item_name;

    public void SetupToolTip(FieldItem item)
    {
        CurrentItem = item;
        Item_name.text = item.item.itemname;
    }

    private void Update()
    {

        this.transform.position = Input.mousePosition;
    }


}
