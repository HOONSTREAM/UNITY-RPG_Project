using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �絧�þ� ���� ���� �� NPC�� ���� ��ũ��Ʈ�� �����մϴ�.
/// </summary>
public class Inn_NPC_Script_Rudencian : MonoBehaviour, IPointerClickHandler, TalkManager.Additional_Talking // �������̽�
{
    [SerializeField]
    GameManager gamemanager;

    private const int RECOVERY_HP_AMOUNT = 100000;
   

    /// <summary>
    /// �絧�þ� ���� ���� �� NPC�� Ŭ������ ��, �߻��ϴ� �̺�Ʈ�� �����մϴ�.
    /// 
    /// �̺�Ʈ 1 : ü���� ȸ���ϰڽ��ϴ�. Text ���
    /// �̺�Ʈ 2 : �÷��̾� ü�� ��ü ȸ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();

        // �÷��̾��� ü���� ��ü ȸ�� ��ŵ�ϴ�.

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

        effect_3d.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
        effect_3d.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Destroy(effect_3d, 5.0f);

    }


    /// <summary>
    /// �߰� ��ȭ�� ������ ��, Ȯ������ �����ϴ� �������̽� �޼��� �Դϴ�.
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    void TalkManager.Additional_Talking.Additional_Talk()
    {
        throw new System.NotImplementedException();
    }

}
