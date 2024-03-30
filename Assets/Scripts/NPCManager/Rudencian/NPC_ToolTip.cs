using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC_ToolTip : MonoBehaviour //TODO : 코드로 Load 연결
{
    /*IPointerInterface 사용을 하기 위한 조건
     UI 오브젝트 -> Canvas에 Graphics Raycaster 컴포넌트 할당 후 Image에 raycast target 체크 유무 확인
     3D/2D 오브젝트 -> 콜라이더 확인 / MainCamera에 physics raycaster 확인 */

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
