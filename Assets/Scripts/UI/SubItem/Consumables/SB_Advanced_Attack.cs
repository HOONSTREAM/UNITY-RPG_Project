using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEffect/SkillBook(��ų��)/���꽺�����")]

public class SB_Advanced_Attack : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {

        GameObject player = Managers.Game.GetPlayer();
        
        Print_Info_Text.Instance.PrintUserText($"��ų���� ����Ͽ� ��ų�� ȹ���Ͽ����ϴ�.");
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[2]);

        return true;
    }

}
