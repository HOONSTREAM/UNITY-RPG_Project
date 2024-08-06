using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Class : MonoBehaviour
{
    public enum ClassType
    {
        UnKnown, // 초심자(직업없음)
        Warrior, // 한손검 특화류 전사
        Paladin, // 두손검 특화류 전사

    }

    /// <summary>
    /// 직업을 가지기 위한 최소의 어빌리티 수치입니다.
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
                Player_class_UI_Text.text = "초심자";
                break;
            case ClassType.Warrior:
                _classtype = ClassType.Warrior;
                Player_class_UI_Text.text = "워리어";
                break;
            case ClassType.Paladin:
                _classtype = ClassType.Paladin;
                Player_class_UI_Text.text = "팔라딘";
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
