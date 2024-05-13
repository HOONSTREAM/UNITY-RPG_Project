using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEffect/SkillBook(스킬북)/난공불락")]

public class SB_Advanced_Defense : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {

        GameObject player = Managers.Game.GetPlayer();

        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"스킬북을 사용하여 스킬을 획득하였습니다.");
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[3]);

        return true;
    }
}
