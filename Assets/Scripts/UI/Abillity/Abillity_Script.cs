using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SerializableDictionary;

public class Abillity_Script : MonoBehaviour
{
    public bool active_abillity_panel = false;
    public GameObject Abillity_Panel;
    public GameObject Abillity_canvas;
    public GameObject Abillity_Explaination_Panel;
    public GameObject select_quickslot_register;


    private PlayerAbillity abillity; 
    private PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)

    public delegate void OnUpdateAbillity();
    public OnUpdateAbillity onupdate_abillity;

    public Abillity_Slot[] abillity_Slots;
    public Transform abillity_slotHolder;

    #region 버프스킬 지속시간 UI 관련 변수
   
    private int skill_slot_number;
    private Skill skill_info;
    public Buff_Slot[] buff_slot;
    public Transform buff_slot_holder;
    #endregion


    private void Start() //Player Controller에 붙이면, 프리펩이므로 Start 전의 프리펩이 붙어서 slot이 업데이트가 안됨. 
    {
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        abillity = PlayerAbillity.Instance;
        abillity_Slots = abillity_slotHolder.GetComponentsInChildren<Abillity_Slot>();       
        abillity.onChangeSkill += RedrawSlotUI;
        UI_Base.BindEvent(Abillity_Panel, (PointerEventData data) => {Abillity_Panel.transform.position = data.position; }, Define.UIEvent.Drag);
        Managers.UI.SetCanvas(Abillity_canvas, true);
        buff_slot = buff_slot_holder.GetComponentsInChildren<Buff_Slot>();

        Abillity_Panel.SetActive(active_abillity_panel);

        onupdate_abillity += Accumulate_abillity_Func; //delegate
        



        //기본적인 세팅 (한손검,양손검)
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[0]);
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[1]);
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[2]);
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[3]);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("스킬창을 열기 위한 K키 입력 완료");
            active_abillity_panel = !active_abillity_panel;
            Abillity_Panel.SetActive(active_abillity_panel);
            Managers.Sound.Play("Inven_Open");

            if (Abillity_Panel.activeSelf == false)
            {
                select_quickslot_register.gameObject.SetActive(false);

            }

        }
    }
    public Skill Get_Slotnum(int slotnum) //슬롯에 있는 스킬 을 참조받아 변수에 저장해두고, 그 슬롯의 넘버도 보관
    {
        skill_slot_number = slotnum;
        return skill_info = abillity_Slots[slotnum].skill;

    }


    public void Button_Function()
    {

        active_abillity_panel = !active_abillity_panel;
        Abillity_Panel.SetActive(active_abillity_panel);
        Managers.Sound.Play("Inven_Open");

        return;

    }

    public void X_Button_Exit()
    {
        if (Abillity_Panel.activeSelf)
        {
            Abillity_Panel.SetActive(false);
            active_abillity_panel = false;
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

        for (int i = 0; i < abillity_Slots.Length; i++) //싹 밀어버리고
        {
           abillity_Slots[i].RemoveSlot();
        }

        for (int i = 0; i < abillity.PlayerSkill.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
        {
            abillity_Slots[i].skill = abillity.PlayerSkill[i];
            abillity_Slots[i].UpdateSlotUI();

        }
    }

    public void Accumulate_abillity_Func()

        //TODO : 어빌 1.00 당 해당 무기 고정데미지 향상 +5 , 게임저장 방법(유니티교과서) , 그레이드
    {
       
        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value) && value.weapontype == WeaponType.One_Hand) // 무기를 장착중이고, 한손검인경우
        {
                                 
            for (int i = 0; i < abillity_Slots.Length; i++)
            {
                if (abillity_Slots[i].skill_name.text == "한손검")
                {                                                 
                    if(double.Parse(abillity_Slots[i].Level.text) == 50.00)
                    {
                        abillity_Slots[i].Name_grade.text = "SENIOR";
                    }
                    if (double.Parse(abillity_Slots[i].Level.text) == 100.00)
                    {
                        Debug.Log("어빌이 100에 달성하였습니다.");
                        // TODO : 그레이드 진행
                        abillity_Slots[i].Name_grade.text = "MASTER";
                        return;
                    }

                    abillity_Slots[i].Level.text = $"{abillity_Slots[i].skill.abillity += 10.00}"; //TEST TODO

                }               
            }

       
           
        }

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.Two_Hand) // 무기를 장착중이고, 두손검인경우
        {
            Debug.Log("두손검 어빌을 올립니다.");

            for (int i = 0; i < abillity_Slots.Length; i++)
            {
                if (abillity_Slots[i].skill_name.text == "양손검")
                {
                    Debug.Log("두손검 어빌 발견");
                    Debug.Log($"{abillity_Slots[i].slotnum}번 스킬슬롯 입니다.");
                    Debug.Log("어빌을 올립니다.");
                    abillity_Slots[i].Level.text = $"{abillity_Slots[i].skill.abillity += 0.01}";
                }

            }

        }

        else
        {
            Debug.Log("맨손 입니다.");

            return;
        }
    }

  
    public void Explaination_Panel_Open()
    {
        Abillity_Explaination_Panel.SetActive(true);
    }

    public void Explaination_Panel_Close()
    {
        if (Abillity_Explaination_Panel.activeSelf)
        {
            Abillity_Explaination_Panel.SetActive(false);
        }
    }

    IEnumerator StartCountdown()
    {
        float currentTime = skill_info.skill_cool_time;
        Skill currentskill = skill_info; 

        while (currentTime > 0)
        {
            if (Time.timeScale > 0)
            {
                for (int i = 0; i < buff_slot.Length; i++)
                {
                    if (buff_slot[i].skill == currentskill)
                    {
                        buff_slot[i].skill_time_text.text = currentTime.ToString("0");
                        currentTime -= Time.deltaTime;
                        yield return null;
                    }
                }
            }

        }

     
        for(int i = 0; i<buff_slot.Length; i++)
        {
            if (buff_slot[i].skill_time_text.text == "0")
            {
                PlayerBuff_Slot.Instance.Buff_Slot_RemoveBuffSkill(i);
            }
        }

       
    }

    public void start_buff_skill(Skill _skill_info)
    {
        skill_info = _skill_info;
        PlayerBuff_Slot.Instance.Buff_slot_AddBuffSkill(_skill_info);
        StartCoroutine("StartCountdown");

        return;
    }

}
