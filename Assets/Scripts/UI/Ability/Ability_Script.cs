using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static SerializableDictionary;

public class Ability_Script : MonoBehaviour
{
    public bool active_Ability_panel = false;
    public GameObject Ability_Panel;
    public GameObject Ability_canvas;
    public GameObject Ability_Explaination_Panel;
    public GameObject select_quickslot_register;


    private PlayerAbility Ability; 
    private PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)

    public delegate void OnUpdateAbility();
    public OnUpdateAbility onupdate_Ability;

    public Ability_Slot[] Ability_Slots;
    public Transform Ability_slotHolder;


    #region 어빌리티 인터페이스 업데이트

    public Skill skill;
    public UnityEngine.UI.Image skill_icon;
    public TextMeshProUGUI skill_name;
    public TextMeshProUGUI grade_LEVEL;
    public TextMeshProUGUI Ability_LEVEL;
    public TextMeshProUGUI _slider_percent;
    public UnityEngine.UI.Slider _slider;
    public GameObject Ability_Interface_Panel;
   


    private readonly double Ability_INCREASE_AMOUNT = Math.Round(50.0f,2);
    private readonly double ABILITY_MASTER_LEVEL = 100.00f;
    private const float MAX_Ability_COUNT = 1.0f;
   


    private readonly float AN_INCREASE_MORE_THAN_Ability_90 = 0.05f;
    private readonly float AN_INCREASE_MORE_THAN_Ability_80 = 0.15f;
    private readonly float AN_INCREASE_MORE_THAN_Ability_70 = 0.2f;
    private readonly float AN_INCREASE_MORE_THAN_Ability_60 = 0.35f;
    private readonly float AN_INCREASE_MORE_THAN_Ability_50 = 0.4f;
    private readonly float AN_INCREASE_MORE_THAN_Ability_40 = 0.4f;
    private readonly float AN_INCREASE_MORE_THAN_Ability_30 = 0.4f;
    private readonly float AN_INCREASE_MORE_THAN_Ability_20 = 0.5f;
    private readonly float AN_INCREASE_MORE_THAN_Ability_10 = 0.5f;
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
        stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        Ability = PlayerAbility.Instance;
        Ability_Slots = Ability_slotHolder.GetComponentsInChildren<Ability_Slot>();       
        Ability.onChangeSkill += RedrawSlotUI;
        UI_Base.BindEvent(Ability_Panel, (PointerEventData data) => {Ability_Panel.transform.position = data.position; }, Define.UIEvent.Drag);
        Managers.UI.SetCanvas(Ability_canvas, true);
        buff_slot = buff_slot_holder.GetComponentsInChildren<Buff_Slot>();

        Ability_Panel.SetActive(active_Ability_panel);

        onupdate_Ability += Accumulate_Ability_Func;
        



        //기본적인 세팅 (한손검,양손검)
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[0]);
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[1]);
        Ability_Interface_Panel.gameObject.SetActive(false); // 어빌리티 인터페이스 패널 초기화 

}

private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Toggle_Ability_Skill_UI();

            if (Ability_Panel.activeSelf == false)
            {
                select_quickslot_register.gameObject.SetActive(false);

            }

        }
    }
    public Skill Get_Slotnum(int slotnum) //슬롯에 있는 스킬 을 참조받아 변수에 저장해두고, 그 슬롯의 넘버도 보관
    {
        skill_slot_number = slotnum;
        return skill_info = Ability_Slots[slotnum].skill;

    }


    public void Toggle_Ability_Skill_UI()
    {
        active_Ability_panel = !active_Ability_panel;
        Ability_Panel.SetActive(active_Ability_panel);
        Managers.UI.SetCanvas(Ability_canvas, true); // 캔버스 SortOrder 순서를 열릴 때마다 정의함. (제일 마지막에 열린 것이 가장 위로)
        Managers.Sound.Play("Inven_Open");

        return;
    }

    public void Close_Ability_Skill_UI()
    {
        if (Ability_Panel.activeSelf)
        {
            Ability_Panel.SetActive(false);
            active_Ability_panel = false;
            Managers.Sound.Play("Inven_Open");
        }

        return;
    } // 어빌리티창 off

    public void X_Button_Ability_Interface()
    {
        if (Ability_Interface_Panel.activeSelf)
        {
            Ability_Interface_Panel.gameObject.SetActive(false);
        }

        return;
    }
    void RedrawSlotUI()
    {
        for (int i = 0; i < Ability_Slots.Length; i++)
        {
            Ability_Slots[i].slotnum = i;
        }

        for (int i = 0; i < Ability_Slots.Length; i++) //싹 밀어버리고
        {
           Ability_Slots[i].RemoveSlot();
        }

        for (int i = 0; i < Ability.PlayerSkill.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
        {
            Ability_Slots[i].skill = Ability.PlayerSkill[i];
            Ability_Slots[i].UpdateSlotUI();

        }
    }

    private void OnUpdate_Ability_Interface(WeaponType weapontype)
    {
        if (Ability_Interface_Panel.activeSelf == false)
        {
            Ability_Interface_Panel.gameObject.SetActive(true);
        }

        foreach (var slot in Ability_Slots)
        {
            if ((weapontype == WeaponType.One_Hand && slot.skill_name.text == "한손검") ||
                (weapontype == WeaponType.Two_Hand && slot.skill_name.text == "양손검"))
            {
                skill_icon.sprite = slot.skill_icon.sprite;
                skill_name.text = slot.skill_name.text;
                grade_LEVEL.text = slot.skill.Ability_Grade.ToString();
                Ability_LEVEL.text = slot.skill.Ability.ToString();
                _slider.value = slot._slider.value;
                _slider_percent.text = (_slider.value * 100).ToString();
                break;
            }
        }

    }

    public void Accumulate_Ability_Func()

    {

        GameObject monster = Managers.Monster_Info.Get_Monster_Info();

        if (monster == null)
        {
            return;
        }


        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value) && value.weapontype == WeaponType.One_Hand)
        {


            for (int i = 0; i < Ability_Slots.Length; i++)
            {

                foreach (var slot in Ability_Slots)
                {
                    if (slot.skill_name.text != "한손검")
                    {
                        continue;
                    }


                    if ((int)slot.skill.Ability >= Managers.Game.GetPlayer().GetComponent<Player_Class>().class_acquisition_required_Ability())
                    {

                        if (Managers.Game.GetPlayer().GetComponent<Player_Class>().Get_Player_Class() != Player_Class.ClassType.Warrior)
                        {
                            Print_Info_Text.Instance.PrintUserText("직업을 가져야 어빌을 올릴 수 있습니다.");
                            return;
                        }

                    }
                   

                    if (3 * (monster.GetComponent<Stat>().LEVEL) < (int)(slot.skill.Ability))
                    {
                        Print_Info_Text.Instance.PrintUserText("몬스터 레벨이 너무 낮습니다.");
                        return;
                    }

                    
                    int Ability_attack_value_before_change = stat.Onupdate_Ability_attack(); //어빌리티가 업데이트 되기 전의 변수를 저장합니다.

                    float LEVEL = float.Parse(slot.LEVEL.text);


                    float increaseAmount = 0f;

                    // 레벨에 따라 increaseAmount 값을 조정합니다.
                    if (LEVEL >= 90) increaseAmount = AN_INCREASE_MORE_THAN_Ability_90;
                    else if (LEVEL >= 80) increaseAmount = AN_INCREASE_MORE_THAN_Ability_80;
                    else if (LEVEL >= 70) increaseAmount = AN_INCREASE_MORE_THAN_Ability_70;
                    else if (LEVEL >= 60) increaseAmount = AN_INCREASE_MORE_THAN_Ability_60;
                    else if (LEVEL >= 50) increaseAmount = AN_INCREASE_MORE_THAN_Ability_50;
                    else if (LEVEL >= 40) increaseAmount = AN_INCREASE_MORE_THAN_Ability_40;
                    else if (LEVEL >= 30) increaseAmount = AN_INCREASE_MORE_THAN_Ability_30;
                    else if (LEVEL >= 20) increaseAmount = AN_INCREASE_MORE_THAN_Ability_20;
                    else if (LEVEL >= 10) increaseAmount = AN_INCREASE_MORE_THAN_Ability_10;
                    else if (LEVEL <= 10) increaseAmount = AN_INCREASE_LESS_THAN_10;


                    // 계산된 증가량을 적용합니다.
                    slot._slider.value += increaseAmount;


                    //계산된 증가량을 저장하여 차이 증가량을 남겨 다음 증가량에 보존시키기 위한 변수
                    potentialNewValue += increaseAmount;



                    if (potentialNewValue >= MAX_Ability_COUNT)
                    {

                        if (slot.skill.Ability == ABILITY_MASTER_LEVEL)
                        {
                            Print_Info_Text.Instance.PrintUserText("최대 어빌리티를 초과하여, 그레이드가 상승하였습니다.");
                            slot.skill.Ability_Grade++;
                            slot.skill.Ability = 0.0f;
                        }

                        float excess = potentialNewValue - MAX_Ability_COUNT;
                        excess = (float)Math.Round(excess, 2);
                        slot.LEVEL.text = (slot.skill.Ability + Ability_INCREASE_AMOUNT).ToString(); // 어빌리티 레벨 증가
                        slot.skill.Ability += Ability_INCREASE_AMOUNT; // LEVEL 변수도 증가시켜줍니다.

                        int Ability_attack_value_after_change = stat.Onupdate_Ability_attack();

                        stat.ATTACK += Ability_attack_value_after_change - Ability_attack_value_before_change;

                        stat.onchangestat.Invoke();

                        slot._slider.value = excess;
                        potentialNewValue = excess;
                    }

                    else
                    {
                        slot._slider.value = potentialNewValue;
                    }



                    PlayerAbility.Instance.onChangeSkill.Invoke();
                    OnUpdate_Ability_Interface(WeaponType.One_Hand);

                    return;
                }

                PlayerAbility.Instance.onChangeSkill.Invoke();
                OnUpdate_Ability_Interface(WeaponType.One_Hand);
                return;

            }


        }

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.Two_Hand) // 무기를 장착중이고, 두손검인경우
        {

            for (int i = 0; i < Ability_Slots.Length; i++)
            {

                foreach (var slot in Ability_Slots)
                {

                    if (slot.skill_name.text != "양손검")
                    {
                        continue;
                    }

                    if ((int)slot.skill.Ability >= Managers.Game.GetPlayer().GetComponent<Player_Class>().class_acquisition_required_Ability())
                    {

                        if (Managers.Game.GetPlayer().GetComponent<Player_Class>().Get_Player_Class() != Player_Class.ClassType.Paladin)
                        {
                            Print_Info_Text.Instance.PrintUserText("직업을 가져야 어빌을 올릴 수 있습니다.");
                            return;
                        }

                    }


                    if (3 * (monster.GetComponent<Stat>().LEVEL) < (int)(slot.skill.Ability))
                    {
                        Print_Info_Text.Instance.PrintUserText("몬스터 레벨이 너무 낮습니다.");
                        return;
                    }
                    // 어빌리티가 업데이트 되기 전의 변수를 저장합니다.
                    int Ability_attack_value_before_change = stat.Onupdate_Ability_attack();
                    // LEVEL.text를 float으로 변환합니다.
                    float LEVEL = float.Parse(slot.LEVEL.text);

                    // 증가할 value를 저장할 변수를 선언합니다.
                    float increaseAmount = 0f;

                    // 레벨에 따라 increaseAmount 값을 조정합니다.
                    if (LEVEL >= 90) increaseAmount = AN_INCREASE_MORE_THAN_Ability_90;
                    else if (LEVEL >= 80) increaseAmount = AN_INCREASE_MORE_THAN_Ability_80;
                    else if (LEVEL >= 70) increaseAmount = AN_INCREASE_MORE_THAN_Ability_70;
                    else if (LEVEL >= 60) increaseAmount = AN_INCREASE_MORE_THAN_Ability_60;
                    else if (LEVEL >= 50) increaseAmount = AN_INCREASE_MORE_THAN_Ability_50;
                    else if (LEVEL >= 40) increaseAmount = AN_INCREASE_MORE_THAN_Ability_40;
                    else if (LEVEL >= 30) increaseAmount = AN_INCREASE_MORE_THAN_Ability_30;
                    else if (LEVEL >= 20) increaseAmount = AN_INCREASE_MORE_THAN_Ability_20;
                    else if (LEVEL >= 10) increaseAmount = AN_INCREASE_MORE_THAN_Ability_10;
                    else if (LEVEL <= 10) increaseAmount = AN_INCREASE_LESS_THAN_10;


                    // 계산된 증가량을 적용합니다.
                    slot._slider.value += increaseAmount;


                    //계산된 증가량을 저장하여 차이 증가량을 남겨 다음 증가량에 보존시키기 위한 변수
                    potentialNewValue += increaseAmount;



                    if (potentialNewValue >= MAX_Ability_COUNT)
                    {

                        if (slot.skill.Ability == ABILITY_MASTER_LEVEL)
                        {
                            Print_Info_Text.Instance.PrintUserText("최대 어빌리티를 초과하여, 그레이드가 상승하였습니다.");
                            slot.skill.Ability_Grade++;
                            slot.skill.Ability = 0.0f;
                        }

                        float excess = potentialNewValue - MAX_Ability_COUNT;
                        excess = (float)Math.Round(excess, 2);
                        slot.LEVEL.text = (slot.skill.Ability + Ability_INCREASE_AMOUNT).ToString(); // 어빌리티 레벨 증가

                        slot.skill.Ability += Ability_INCREASE_AMOUNT; // LEVEL 변수도 증가시켜줍니다.
                        int Ability_attack_value_after_change = stat.Onupdate_Ability_attack();

                        stat.ATTACK += Ability_attack_value_after_change - Ability_attack_value_before_change;

                        stat.onchangestat.Invoke();


                        slot._slider.value = excess;
                        potentialNewValue = excess;
                    }

                    else
                    {
                        slot._slider.value = potentialNewValue;
                    }

                    PlayerAbility.Instance.onChangeSkill.Invoke();
                    OnUpdate_Ability_Interface(WeaponType.Two_Hand);

                    return;
                }
                PlayerAbility.Instance.onChangeSkill.Invoke();
                OnUpdate_Ability_Interface(WeaponType.Two_Hand);

                return;

            }


        }


    }

    public void Explaination_Panel_Open()
    {
        Ability_Explaination_Panel.SetActive(true);
    }

    public void Explaination_Panel_Close()
    {
        if (Ability_Explaination_Panel.activeSelf)
        {
            Ability_Explaination_Panel.SetActive(false);
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


