using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePort_ToolTip : MonoBehaviour
{
    private void Update()
    {
        transform.position = Input.mousePosition + new Vector3(0, 105.0f, 0);
    }
}
