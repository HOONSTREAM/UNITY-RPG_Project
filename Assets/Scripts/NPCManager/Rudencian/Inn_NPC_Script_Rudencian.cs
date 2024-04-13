using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 루덴시안 여관 내부 헥센 NPC에 대한 스크립트를 정의합니다.
/// </summary>
public class Inn_NPC_Script_Rudencian : MonoBehaviour, IPointerClickHandler, TalkManager.Additional_Talking // 인터페이스
{
    [SerializeField]
    GameManager gamemanager;

    private const int RECOVERY_HP_AMOUNT = 100000;
   

    /// <summary>
    /// 루덴시안 여관 내부 헥센 NPC를 클릭했을 때, 발생하는 이벤트를 제어합니다.
    /// 
    /// 이벤트 1 : 체력을 회복하겠습니다. Text 출력
    /// 이벤트 2 : 플레이어 체력 전체 회복
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();

        // 플레이어의 체력을 전체 회복 시킵니다.

        var player_stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        if (player_stat == null) return;

        player_stat.Hp += RECOVERY_HP_AMOUNT;
        if (player_stat.Hp > player_stat.MAXHP)
        {
            player_stat.Hp = player_stat.MAXHP;
        }

        Managers.Sound.Play("LEVELup_sound",Define.Sound.Effect);

        HP_Recovery_Buff_Effect();
    }

    private void HP_Recovery_Buff_Effect()
    {
        GameObject effect_3d = Managers.Resources.Instantiate("Skill_Effect/Healing_Effect");

        effect_3d.transform.parent = Managers.Game.GetPlayer().transform; // 부모설정
        effect_3d.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Destroy(effect_3d, 5.0f);

    }


    /// <summary>
    /// 추가 대화가 생겼을 때, 확장기능을 제공하는 인터페이스 메서드 입니다.
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    void TalkManager.Additional_Talking.Additional_Talk()
    {
        throw new System.NotImplementedException();
    }

}
