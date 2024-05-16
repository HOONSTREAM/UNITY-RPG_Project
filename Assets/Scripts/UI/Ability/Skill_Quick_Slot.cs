using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill_Quick_Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;

    public Skill skill;
    public Image skill_icon;    
    public Ability_Slot[] Ability_slots;
    public Transform Ability_slot_holder;

    void Awake() // Start, Awake �Լ��� Ŭ������ ������ (�ν��Ͻ����� �ʱ�ȭ)�� ��� ���ش�.
    {
        skill_icon.gameObject.SetActive(false); //�ʱ�ȭ (������ ǥ�� ����)        
        Ability_slots = Ability_slot_holder.GetComponentsInChildren<Ability_Slot>();
    }

    


    public void UpdateSlotUI()
    {
        skill_icon.sprite = skill.skill_image;
        skill_icon.gameObject.SetActive(true);
      
    }

    public void RemoveSlot()
    {
        skill = null;
        skill_icon.gameObject.SetActive(false);
    }


    public void OnPointerUp(PointerEventData eventData) 
    {
        if (this.skill == null)
        {
            GameObject player = Managers.Game.GetPlayer();
            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�����Կ� ��ų�� �����ϴ�.");

            return;
        }
        if (skill.skilltype == SkillType.Buff)
        {
            bool isUsed = this.skill.Skill_Use();

            if (isUsed)

            {
                //TODO: �������� ������� �� ����
                Ability_Script abs = GameObject.Find("Ability_Slot_CANVAS ").gameObject.GetAddComponent<Ability_Script>();
                abs.start_buff_skill(this.skill);

            }

            return;
        }

        else if (skill.skilltype == SkillType.Active) 
        {
            Debug.Log("��ų���");
            return;
        }

        
    }




}
