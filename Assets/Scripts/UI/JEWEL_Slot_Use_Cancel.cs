using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JEWEL_Slot_Use_Cancel : MonoBehaviour
{
    public GameObject Spiritual_stone_use_cancel_panel;
    public Item slot_item; // ���Կ� �ش��ϴ� ������ ����
    public int slot_number; // ������ ��ȣ ����
    public JEWEL_Slot[] slots; //�÷��̾� ���� ����
    public PlayerStat stat;
    


    private const int WATER_SPRITUAL_STONE_ID = 12;


    void Start()
    {
        GameObject go = GameObject.Find("JEWEL_UI").gameObject;
        slots = go.GetComponentsInChildren<JEWEL_Slot>();
        stat = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();       
    }

    public Item Get_Slotnum(int slotnum) //���Կ� �ִ� �������� �����޾� private Item ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;
        return slot_item = slots[slotnum].item;
    }

    public void Spiritual_Stone_Use()
    {
        Skill skill = SkillDataBase.instance.SkillDB[5];

        if (slot_item == null)
        {
            Debug.Log("�׽�Ʈ : ���Կ� �ƹ��͵� ����");
            Spiritual_stone_use_cancel_panel.SetActive(false);
            return;
        }

        Spiritual_stone_use_cancel_panel.SetActive(false);

        switch (slot_item.ItemID)
        {
            case WATER_SPRITUAL_STONE_ID:
                Debug.Log("���Ȱ��� ���ɼ��� ����Ͽ���.");

                bool isUsed = skill.Skill_Use();

                if (isUsed)

                {
                    Ability_Script abs = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetAddComponent<Ability_Script>();
                    abs.start_buff_skill(skill);
                }

                break;

        }
       
    }
    public void consoleExit() => Spiritual_stone_use_cancel_panel.gameObject.SetActive(false);


}
