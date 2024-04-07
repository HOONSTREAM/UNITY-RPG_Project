using System;
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


    #region 어빌리티 인터페이스 업데이트

    public Skill skill;
    public Image skill_icon;
    public TextMeshProUGUI skill_name;
    public TextMeshProUGUI grade_level;
    public TextMeshProUGUI Abillity_Level;
    public TextMeshProUGUI _slider_percent;
    public Slider _slider;
    public GameObject Abillity_Interface_Panel;


    private readonly double ABILLITY_INCREASE_AMOUNT = Math.Round(0.01f,2);
    private const float MAX_ABILLITY_COUNT = 1.0f;
    private const float ABILLITY_INTERMEDIATE_LEVEL = 50.00f;
    private const float ABILLITY_MASTER_LEVEL = 100.00f;


    private readonly float AN_INCREASE_MORE_THAN_ABILLITY_90 = 0.05f;
    private readonly float AN_INCREASE_MORE_THAN_ABILLITY_80 = 0.15f;
    private readonly float AN_INCREASE_MORE_THAN_ABILLITY_70 = 0.2f;
    private readonly float AN_INCREASE_MORE_THAN_ABILLITY_60 = 0.35f;
    private readonly float AN_INCREASE_MORE_THAN_ABILLITY_50 = 0.4f;
    private readonly float AN_INCREASE_MORE_THAN_ABILLITY_40 = 0.4f;
    private readonly float AN_INCREASE_MORE_THAN_ABILLITY_30 = 0.4f;
    private readonly float AN_INCREASE_MORE_THAN_ABILLITY_20 = 0.5f;
    private readonly float AN_INCREASE_MORE_THAN_ABILLITY_10 = 0.5f;
    private readonly float AN_INCREASE_LESS_THAN_10 = 0.7f;
    private float potentialNewValue = 0.0f;

    #endregion

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

        Abillity_Interface_Panel.gameObject.SetActive(false); // 어빌리티 인터페이스 패널 초기화 

}

private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("스킬창을 열기 위한 K키 입력 완료");
            active_abillity_panel = !active_abillity_panel;
            Abillity_Panel.SetActive(active_abillity_panel);
            Managers.UI.SetCanvas(Abillity_canvas, true); // 캔버스 SortOrder 순서를 열릴때 마다 정의함. (제일 마지막에 열린것이 가장 위로)
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
        Managers.UI.SetCanvas(Abillity_canvas, true); // 캔버스 SortOrder 순서를 열릴때 마다 정의함. (제일 마지막에 열린것이 가장 위로)
        Managers.Sound.Play("Inven_Open");

        return;

    } // 어빌리티 퀵버튼

    public void X_Button_Exit()
    {
        if (Abillity_Panel.activeSelf)
        {
            Abillity_Panel.SetActive(false);
            active_abillity_panel = false;
            Managers.Sound.Play("Inven_Open");
        }

        return;
    } // 어빌리티창 off

    public void X_Button_Abillity_Interface()
    {
        if (Abillity_Interface_Panel.activeSelf)
        {
            Abillity_Interface_Panel.gameObject.SetActive(false);
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

    private void OnUpdate_Abillity_Interface(WeaponType weapontype)
    {
        if (Abillity_Interface_Panel.activeSelf == false)
        {
            Abillity_Interface_Panel.gameObject.SetActive(true);
        }

        switch (weapontype)
        {
            case WeaponType.One_Hand:

                for (int i = 0; i < abillity_Slots.Length; i++)
                {
                    if (abillity_Slots[i].skill_name.text == "한손검")
                    {
                        skill_icon.sprite = abillity_Slots[i].skill_icon.sprite;
                        skill_name.text = abillity_Slots[i].skill_name.text;
                        grade_level = abillity_Slots[i].grade_amount;
                        Abillity_Level.text = abillity_Slots[i].skill.abillity.ToString();
                        _slider.value = abillity_Slots[i]._slider.value;
                        _slider_percent.text = ((_slider.value) * 100).ToString();
                        break;
                    }
                }

                        break;

            case WeaponType.Two_Hand:


                for (int i = 0; i < abillity_Slots.Length; i++)
                {
                    if (abillity_Slots[i].skill_name.text == "양손검")
                    {
                        skill_icon.sprite = abillity_Slots[i].skill_icon.sprite;
                        skill_name.text = abillity_Slots[i].skill_name.text;
                        grade_level = abillity_Slots[i].grade_amount;
                        Abillity_Level.text = abillity_Slots[i].skill.abillity.ToString();
                        _slider.value = abillity_Slots[i]._slider.value;
                        _slider_percent.text = ((_slider.value) * 100).ToString();
                        break;
                    }
                }


                break;

        }


    }
    public void Accumulate_abillity_Func()

      
    {
       
        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value) && value.weapontype == WeaponType.One_Hand) 
        {

            for (int i = 0; i < abillity_Slots.Length; i++)
            {
                if (abillity_Slots[i].skill_name.text == "한손검")
                {
                    if (double.Parse(abillity_Slots[i].Level.text) == ABILLITY_INTERMEDIATE_LEVEL)
                    {
                        abillity_Slots[i].Name_grade.text = "SENIOR";
                    }
                    if (double.Parse(abillity_Slots[i].Level.text) == ABILLITY_MASTER_LEVEL)
                    {                   
                        // TODO : 그레이드 진행
                        abillity_Slots[i].Name_grade.text = "MASTER";
                        return;
                    }

                    foreach (var slot in abillity_Slots)
                    {
                        if (slot.skill_name.text != "한손검")
                        {
                            continue;
                        }
                                                        
                        // Level.text를 float으로 변환합니다.
                        float level = float.Parse(slot.Level.text);

                        // 증가할 value를 저장할 변수를 선언합니다.
                        float increaseAmount = 0f;

                        // 레벨에 따라 increaseAmount 값을 조정합니다.
                        if (level >= 90) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_90;
                        else if (level >= 80) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_80;
                        else if (level >= 70) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_70;
                        else if (level >= 60) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_60;
                        else if (level >= 50) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_50;
                        else if (level >= 40) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_40;
                        else if (level >= 30) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_30;
                        else if (level >= 20) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_20;
                        else if (level >= 10) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_10;
                        else if (level <= 10) increaseAmount = AN_INCREASE_LESS_THAN_10;

                        
                        // 계산된 증가량을 적용합니다.
                        slot._slider.value += increaseAmount;


                        //계산된 증가량을 저장하여 차이 증가량을 남겨 다음 증가량에 보존시키기 위한 변수
                        potentialNewValue += increaseAmount;



                        if (potentialNewValue >= MAX_ABILLITY_COUNT)
                        {
                            float excess = potentialNewValue - MAX_ABILLITY_COUNT;
                            excess = (float)Math.Round(excess, 2);
                            slot.Level.text = (slot.skill.abillity + ABILLITY_INCREASE_AMOUNT).ToString(); // 어빌리티 레벨 증가
                            slot.skill.abillity += ABILLITY_INCREASE_AMOUNT; // level 변수도 증가시켜줍니다.

                            slot._slider.value = excess;
                            potentialNewValue = excess;
                        }

                        else
                        {
                            slot._slider.value = potentialNewValue;
                        }




                        OnUpdate_Abillity_Interface(WeaponType.One_Hand);

                        return;
                    }
                    

                    OnUpdate_Abillity_Interface(WeaponType.One_Hand);
                    return;

                }

                
            }

        }

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.Two_Hand) // 무기를 장착중이고, 두손검인경우
        {

            for (int i = 0; i < abillity_Slots.Length; i++)
            {
                if (abillity_Slots[i].skill_name.text == "양손검")
                {
                    if (double.Parse(abillity_Slots[i].Level.text) == ABILLITY_INTERMEDIATE_LEVEL)
                    {
                        abillity_Slots[i].Name_grade.text = "SENIOR";
                    }
                    if (double.Parse(abillity_Slots[i].Level.text) == ABILLITY_MASTER_LEVEL)
                    {
                        Debug.Log("어빌이 100에 달성하였습니다.");
                        // TODO : 그레이드 진행
                        abillity_Slots[i].Name_grade.text = "MASTER";
                        return;
                    }

                    foreach (var slot in abillity_Slots)
                    {
                        // Level.text를 float으로 변환합니다.
                        float level = float.Parse(slot.Level.text);

                        // 증가할 value를 저장할 변수를 선언합니다.
                        float increaseAmount = 0f;

                        // 레벨에 따라 increaseAmount 값을 조정합니다.
                        if (level >= 90) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_90;
                        else if (level >= 80) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_80;
                        else if (level >= 70) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_70;
                        else if (level >= 60) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_60;
                        else if (level >= 50) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_50;
                        else if (level >= 40) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_40;
                        else if (level >= 30) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_30;
                        else if (level >= 20) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_20;
                        else if (level >= 10) increaseAmount = AN_INCREASE_MORE_THAN_ABILLITY_10;
                        else if (level <= 10) increaseAmount = AN_INCREASE_LESS_THAN_10;

                        // 계산된 증가량을 적용합니다.
                        // 계산된 증가량을 적용합니다.
                        slot._slider.value += increaseAmount;


                        //계산된 증가량을 저장하여 차이 증가량을 남겨 다음 증가량에 보존시키기 위한 변수
                        potentialNewValue += increaseAmount;



                        if (potentialNewValue >= MAX_ABILLITY_COUNT)
                        {
                            float excess = potentialNewValue - MAX_ABILLITY_COUNT;
                            excess = (float)Math.Round(excess, 2);
                            slot.Level.text = (slot.skill.abillity + ABILLITY_INCREASE_AMOUNT).ToString(); // 어빌리티 레벨 증가
                            slot.skill.abillity += ABILLITY_INCREASE_AMOUNT; // level 변수도 증가시켜줍니다.

                            slot._slider.value = excess;
                            potentialNewValue = excess;
                        }

                        else
                        {
                            slot._slider.value = potentialNewValue;
                        }


                        OnUpdate_Abillity_Interface(WeaponType.Two_Hand);

                        return;
                    }

                    OnUpdate_Abillity_Interface(WeaponType.Two_Hand);

                    return;

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
