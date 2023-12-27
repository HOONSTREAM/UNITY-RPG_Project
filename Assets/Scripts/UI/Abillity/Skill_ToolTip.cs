using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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


    /*#GPT4 `Sprite` 클래스와 `Image` 클래스는 Unity에서 2D 그래픽을 다루는 데 사용되는 클래스들인데, 각각의 역할과 사용 용도가 조금 다릅니다.

    1. `Sprite`: `Sprite`는 Unity의 2D 그래픽 시스템에서 사용되는 텍스처의 한 형태입니다. `Sprite`는 텍스처, 크기, 위치, 회전 등의 정보를 포함하며, 이를 이용하여 2D 캐릭터, 배경, 아이템 등을 표현합니다. `Sprite`는 `SpriteRenderer` 컴포넌트를 통해 화면에 표시됩니다.

    2. `Image`: `Image`는 Unity의 UI 시스템에서 사용되는 컴포넌트 중 하나입니다. `Image` 컴포넌트는 `Sprite`를 이용하여 화면에 그림을 표시하는 역할을 합니다. 즉, UI 요소(버튼, 아이콘, 배경 등)를 화면에 표시할 때 사용됩니다.

    요약하면, `Sprite`는 2D 게임 오브젝트를 표현하는 데 사용되는 데이터 형식이고, `Image`는 이러한 `Sprite`를 이용하여 UI 요소를 화면에 표시하는 컴포넌트입니다.*/
    public void SetupTooltip(string name, string stat1, string stat2, int num, int num2, string des, Sprite image)
    {

        skill_name.text = name;
        stat_1.text = stat1;
        stat_2.text = stat2;
        num_1.text = num.ToString();
        num_2.text = num2.ToString();
        Description.text = des;
        skill_image.sprite = image;

    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
