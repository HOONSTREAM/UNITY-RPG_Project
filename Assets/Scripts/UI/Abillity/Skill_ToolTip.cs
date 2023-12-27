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


    /*#GPT4 `Sprite` Ŭ������ `Image` Ŭ������ Unity���� 2D �׷����� �ٷ�� �� ���Ǵ� Ŭ�������ε�, ������ ���Ұ� ��� �뵵�� ���� �ٸ��ϴ�.

    1. `Sprite`: `Sprite`�� Unity�� 2D �׷��� �ý��ۿ��� ���Ǵ� �ؽ�ó�� �� �����Դϴ�. `Sprite`�� �ؽ�ó, ũ��, ��ġ, ȸ�� ���� ������ �����ϸ�, �̸� �̿��Ͽ� 2D ĳ����, ���, ������ ���� ǥ���մϴ�. `Sprite`�� `SpriteRenderer` ������Ʈ�� ���� ȭ�鿡 ǥ�õ˴ϴ�.

    2. `Image`: `Image`�� Unity�� UI �ý��ۿ��� ���Ǵ� ������Ʈ �� �ϳ��Դϴ�. `Image` ������Ʈ�� `Sprite`�� �̿��Ͽ� ȭ�鿡 �׸��� ǥ���ϴ� ������ �մϴ�. ��, UI ���(��ư, ������, ��� ��)�� ȭ�鿡 ǥ���� �� ���˴ϴ�.

    ����ϸ�, `Sprite`�� 2D ���� ������Ʈ�� ǥ���ϴ� �� ���Ǵ� ������ �����̰�, `Image`�� �̷��� `Sprite`�� �̿��Ͽ� UI ��Ҹ� ȭ�鿡 ǥ���ϴ� ������Ʈ�Դϴ�.*/
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
