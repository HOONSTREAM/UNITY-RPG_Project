using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC_ToolTip : MonoBehaviour //TODO : �ڵ�� Load ����
{
    /*IPointerInterface ����� �ϱ� ���� ����
     UI ������Ʈ -> Canvas�� Graphics Raycaster ������Ʈ �Ҵ� �� Image�� raycast target üũ ���� Ȯ��
     3D/2D ������Ʈ -> �ݶ��̴� Ȯ�� / MainCamera�� physics raycaster Ȯ�� */

    [SerializeField]
    private TextMeshProUGUI NPC_name;

   
    public void SetupToolTip(string name)
    {
        NPC_name.text = name;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

}
