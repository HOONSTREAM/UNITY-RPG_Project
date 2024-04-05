using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Field_Item_Tooltip : MonoBehaviour
{

    // ���� ǥ�õǰ� �ִ� FieldItem�� ������ �����ϴ� ������Ƽ�� �߰��մϴ�.
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
