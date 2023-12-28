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
    public GameObject Register_selection;//퀵슬롯 등록창
    
    public Skill skill_info; // 스킬슬롯에 해당하는 스킬 참조
    public int slot_number; // 스킬의 슬롯번호 참조
    public Abillity_Slot[] slots; //플레이어 슬롯 참조
    //public Quick_Slot[] quick_slot; //플레이어의 퀵슬롯 참조
   // public Transform quickslot_holder;
    public PlayerStat stat;

    public Image skill_icon; // 버프스킬 사용 시 맵 패널 하단에 표시되는 버프 이미지 (확장 필요)    
    public TextMeshProUGUI timerText; // 타이머를 표시할 텍스트

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
        abs.skill_icon.gameObject.SetActive(false); // 버프가 종료되면 스킬아이콘 비활성화


    }
    public Skill Get_Slotnum(int slotnum) //슬롯에 있는 아이템을 참조받아 private Item 변수에 저장해두고, 그 슬롯의 넘버도 보관
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
            //TODO : 스킬 사용 후 로직 (버프스킬 상단표시)

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
            Register_selection.SetActive(false); // 콘솔창만 종료하고 함수 종료
            return;
        }

        return;

    }
    public void RegisterQuickSlot()
    {
        Register_selection.gameObject.SetActive(false);
        Managers.Sound.Play("Coin");



        //for (int i = 0; i < quick_slot.Length; i++) //퀵슬롯에 이미 해당아이템이 있는지 검사
        //{
        //    if (quick_slot[i].item == slot_item)
        //    {
        //        PlayerQuickSlot.Instance.Quick_slot_RemoveItem(quick_slot[i].slotnum); // 그 해당아이템 데이터를 전부 삭제하고
        //    }
        //}

        //PlayerQuickSlot.Instance.Quick_slot_AddItem(slot_item); //새로 갱신하여 등록

    }


    public void consoleExit()
    {
        Register_selection.gameObject.SetActive(false);
        
        return;

    }
}
