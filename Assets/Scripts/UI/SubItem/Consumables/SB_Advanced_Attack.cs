using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEffect/SkillBook(��ų��)/���꽺�����")]

public class SB_Advanced_Attack : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {

        GameObject player = Managers.Game.GetPlayer();
        
        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"��ų���� ����Ͽ� ��ų�� ȹ���Ͽ����ϴ�.");
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[2]);

        return true;
    }

}
