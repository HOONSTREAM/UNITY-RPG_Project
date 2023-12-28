using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Skill_QuickSlot_Register : MonoBehaviour
{
    public GameObject Register_selection;//������ ���â
    
    public Skill skill_info; // ��ų���Կ� �ش��ϴ� ��ų ����
    public int slot_number; // ��ų�� ���Թ�ȣ ����
    public Abillity_Slot[] slots; //�÷��̾� ���� ����
    //public Quick_Slot[] quick_slot; //�÷��̾��� ������ ����
   // public Transform quickslot_holder;
    public PlayerStat stat;

    public Image skill_icon; // ������ų ��� �� �� �г� �ϴܿ� ǥ�õǴ� ���� �̹��� (Ȯ�� �ʿ�)    
    public TextMeshProUGUI timerText; // Ÿ�̸Ӹ� ǥ���� �ؽ�Ʈ

    IEnumerator StartCountdown()
    {
        float currentTime = skill_info.skill_cool_time;

        while (currentTime > 0)
        {
            if(Time.timeScale > 0)
            {
                timerText.text = currentTime.ToString("0");
                currentTime -= Time.deltaTime;
                yield return null;
            }
           
        }

        timerText.text = "0";
      
        Abillity_Script abs = GameObject.Find("Abillity_Slot_CANVAS ").gameObject.GetAddComponent<Abillity_Script>();
        abs.skill_icon.gameObject.SetActive(false); // ������ ����Ǹ� ��ų������ ��Ȱ��ȭ


    }
    public Skill Get_Slotnum(int slotnum) //���Կ� �ִ� �������� �����޾� private Item ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;
        return skill_info = slots[slotnum].skill;
    }


    void Start()
    {
        GameObject go = GameObject.Find("Skill_Slot_UI").gameObject;
        slots = go.GetComponentsInChildren<Abillity_Slot>();
        stat = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();

    }

    public void Skill_Use()
    {
        bool isUse = skill_info.Skill_Use();

        if (isUse)
        {
            //TODO : ��ų ��� �� ���� (������ų ���ǥ��)

            Register_selection.SetActive(false);

            if(skill_info.skilltype == SkillType.Buff)
            {
                Abillity_Script abs = GameObject.Find("Abillity_Slot_CANVAS ").gameObject.GetAddComponent<Abillity_Script>();
                abs.skill_icon.gameObject.SetActive(true);
                abs.skill_icon.sprite = skill_info.skill_image;
                StartCoroutine("StartCountdown");
 
            }

            return;
        }
        else if (!isUse)
        {
            Register_selection.SetActive(false); // �ܼ�â�� �����ϰ� �Լ� ����
            return;
        }

        return;

    }
    public void RegisterQuickSlot()
    {
        Register_selection.gameObject.SetActive(false);
        Managers.Sound.Play("Coin");



        //for (int i = 0; i < quick_slot.Length; i++) //�����Կ� �̹� �ش�������� �ִ��� �˻�
        //{
        //    if (quick_slot[i].item == slot_item)
        //    {
        //        PlayerQuickSlot.Instance.Quick_slot_RemoveItem(quick_slot[i].slotnum); // �� �ش������ �����͸� ���� �����ϰ�
        //    }
        //}

        //PlayerQuickSlot.Instance.Quick_slot_AddItem(slot_item); //���� �����Ͽ� ���

    }


    public void consoleExit()
    {
        Register_selection.gameObject.SetActive(false);
        
        return;

    }
}
