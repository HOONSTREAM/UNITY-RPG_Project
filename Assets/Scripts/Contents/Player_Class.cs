using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Class : MonoBehaviour
{
    public enum ClassType
    {
        UnKnown, // �ʽ���(��������)
        Warrior, // �Ѽհ� Ưȭ�� ����
        Paladin, // �μհ� Ưȭ�� ����

    }

    /// <summary>
    /// ������ ������ ���� �ּ��� �����Ƽ ��ġ�Դϴ�.
    /// </summary>
    private const int Class_acquisition_required_Ability = 10;

    [SerializeField]
    private TextMeshProUGUI Player_class_UI_Text;

    [SerializeField]
    private ClassType _classtype;
    
    void Start()
    {
        _classtype = ClassType.UnKnown;
        Player_class_UI_Text = GameObject.Find("job_info_text").gameObject.GetAddComponent<TextMeshProUGUI>();
        Set_Player_Class(_classtype);
    }

    
    public void Set_Player_Class(ClassType type)
    {
        switch (type)
        {
            case ClassType.UnKnown:
                _classtype = ClassType.UnKnown;
                Player_class_UI_Text.text = "�ʽ���";
                break;
            case ClassType.Warrior:
                _classtype = ClassType.Warrior;
                Player_class_UI_Text.text = "������";
                break;
            case ClassType.Paladin:
                _classtype = ClassType.Paladin;
                Player_class_UI_Text.text = "�ȶ��";
                break;

        }
    }

    public ClassType Get_Player_Class()
    {
        return _classtype;
    }

    public int class_acquisition_required_Ability()
    {
        return Class_acquisition_required_Ability;
    }



}
