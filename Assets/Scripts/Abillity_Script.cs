using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static SerializableDictionary;

public class Abillity_Script : MonoBehaviour
{
    public GameObject Abillity_Panel;
    public GameObject Abillity_canvas;


    private PlayerAbillity abillity; //�÷��̾� �κ��丮 ����
    private PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)



    public Abillity_Slot[] abillity_Slots;
    public Transform abillity_slotHolder;
   

    private void Start()
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        abillity = PlayerAbillity.Instance;
        abillity_Slots = abillity_slotHolder.GetComponentsInChildren<Abillity_Slot>();

        abillity.onChangeSkill += RedrawSlotUI;

        UI_Base.BindEvent(Abillity_Panel, (PointerEventData data) => { Abillity_Panel.transform.position = data.position; }, Define.UIEvent.Drag);
        Managers.UI.SetCanvas(Abillity_canvas, true);

        #region ���Թ��׹��� ���Ի����� ����
        abillity.AddSkill(SkillDataBase.instance.SkillDB[0]);
        abillity.RemoveSkill(0);      
        RedrawSlotUI();
        #endregion

        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[0]);
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[1]);
    }
    public void Exit()
    {
        if (Abillity_Panel.activeSelf)
        {
            Abillity_Panel.SetActive(false);
            GameObject go = GameObject.Find("GUI").gameObject;
            go.GetComponent<Abillity_Button_Script>().active_abillity_panel = false;
            Managers.Sound.Play("Inven_Open");
        }

        return;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < abillity_Slots.Length; i++)
        {
            abillity_Slots[i].slotnum = i;
        }

        for (int i = 0; i < abillity_Slots.Length; i++) //�� �о������
        {
           abillity_Slots[i].RemoveSlot();
        }

        for (int i = 0; i < abillity.PlayerSkill.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            abillity_Slots[i].skill = abillity.PlayerSkill[i];
            abillity_Slots[i].UpdateSlotUI();

        }
    }

    public void Accumulate_abillity_Func()
    {
        if(PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value) && value.weapontype == WeaponType.One_Hand) // ���⸦ �������̰�, �Ѽհ��ΰ��
        {
            Debug.Log("�Ѽհ� ����� �ø��ϴ�.");

            return;
        }

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.Two_Hand) // ���⸦ �������̰�, �μհ��ΰ��
        {
            Debug.Log("�μհ� ����� �ø��ϴ�.");

            return;

        }

        else
        {
            Debug.Log("�Ǽ� �Դϴ�.");

            return;
        }
    }
}
