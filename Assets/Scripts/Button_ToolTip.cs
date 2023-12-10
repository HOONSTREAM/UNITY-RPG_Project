using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Button_ToolTip : MonoBehaviour
{
    /*IPointerInterface ����� �ϱ� ���� ����
    UI ������Ʈ -> Canvas�� Graphics Raycaster ������Ʈ �Ҵ� �� Image�� raycast target üũ ���� Ȯ��
    3D/2D ������Ʈ -> �ݶ��̴� Ȯ�� / MainCamera�� physics raycaster Ȯ�� */

    [SerializeField]
    private TextMeshProUGUI Button_name;


    public void SetupToolTip(string name)
    {
        Button_name.text = name;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

}
