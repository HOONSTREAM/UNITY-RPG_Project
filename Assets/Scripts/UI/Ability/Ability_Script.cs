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

    #region ���� Ÿ��

    private readonly string ONE_HAND_SWORD = "�Ѽհ�";
    private readonly string TWO_HAND_SWORD = "��հ�";

    #endregion

    public bool active_Ability_panel = false;
    public GameObject Ability_Panel;
    public GameObject Ability_canvas;
    public GameObject Ability_Explaination_Panel;
    public GameObject select_quickslot_register;


    private PlayerAbility Ability; 
    private PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)

    public Ability_Slot[] Ability_Slots;
    public Transform Ability_slotHolder;


    #region �����Ƽ �������̽� ������Ʈ

    public Skill skill;
    public UnityEngine.UI.Image skill_icon;
    public TextMeshProUGUI skill_name;
    public TextMeshProUGUI grade_LEVEL;
    public TextMeshProUGUI Ability_LEVEL;
    public TextMeshProUGUI _slider_percent;
    public UnityEngine.UI.Slider _slider;
    public GameObject Ability_Interface_Panel;
   


    private readonly double ABILITY_INCREASE_AMOUNT = Math.Round(1.0f,2);
    private readonly double ABILITY_MASTER_LEVEL = 100.00f;
    private const float MAX_ABILITY_COUNT = 1.0f;

    /// <summary>
    /// �����Ƽ ������, ����ϴ� ī��Ʈ ������� �����մϴ�.
    /// ī��Ʈ ������̶�, ī��Ʈ�� 100% ��� �������� �Ǹ�, �����Ƽ ��°�(ABILITY_INCREASE_AMOUNT)�� ����Ǿ� �����Ƽ�� ����մϴ�.
    /// </summary>
    private static readonly Dictionary<int, float> LEVEL_INCREASES = new Dictionary<int, float>
    {
        { 90, 0.05f },
        { 80, 0.15f },
        { 70, 0.2f },
        { 60, 0.35f },
        { 50, 0.4f },
        { 40, 0.4f },
        { 30, 0.4f },
        { 20, 0.5f },
        { 10, 0.5f },
        { 0, 0.7f }

    };


    /// <summary>
    /// ī��Ʈ������� 100%�ʰ��ϴ� �п� ���ؼ� �ӽ÷� �����ϴ� �����Դϴ�.
    /// </summary>
    private float potentialNewValue = 0.0f;

    #endregion

    #region ������ų ���ӽð� UI ���� ����

    private int skill_slot_number;
    private Skill skill_info;
    public Buff_Slot[] buff_slot;
    public Transform buff_slot_holder;
    public Skill_Quick_Slot[] skill_quick_slot;
    public Transform skill_quick_slot_holder;

    #endregion


    private void Start()
    {
        stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>(); 
        Ability = PlayerAbility.Instance;
        Ability_Slots = Ability_slotHolder.GetComponentsInChildren<Ability_Slot>();       
        Ability.onChangeSkill += RedrawSlotUI;
        UI_Base.BindEvent(Ability_Panel, (PointerEventData data) => {Ability_Panel.transform.position = data.position; }, Define.UIEvent.Drag);
        Managers.UI.SetCanvas(Ability_canvas, true);
        buff_slot = buff_slot_holder.GetComponentsInChildren<Buff_Slot>();
        skill_quick_slot = skill_quick_slot_holder.GetComponentsInChildren<Skill_Quick_Slot>();

        Ability_Panel.SetActive(active_Ability_panel);


        //�⺻���� ���� (�Ѽհ�,��հ�)
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[0]);
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[1]);
        Ability_Interface_Panel.gameObject.SetActive(false); // �����Ƽ �������̽� �г� �ʱ�ȭ 

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

    /// <summary>
    /// ���Կ� �ִ� ��ų �� �����޾� ������ �����صΰ�, �� ������ �ѹ��� �����ϴ� ������ �޼����Դϴ�.
    /// </summary>
    /// <param name="slotnum"></param>
    /// <returns></returns>
    public Skill Get_Slotnum(int slotnum) 
    {
        skill_slot_number = slotnum;
        return skill_info = Ability_Slots[slotnum].skill;

    }
    public void Toggle_Ability_Skill_UI()
    {
        active_Ability_panel = !active_Ability_panel;
        Ability_Panel.SetActive(active_Ability_panel);
        RedrawSlotUI();
        Managers.UI.SetCanvas(Ability_canvas, true); // ĵ���� SortOrder ������ ���� ������ ������. (���� �������� ���� ���� ���� ����)
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
    } // �����Ƽâ off

    /// <summary>
    /// ���� Ÿ�� �� �ڵ����� ������ �����Ƽ ���� ���â�� �� �� �ִ� ��Ŭ X��ư�� �����ϴ� �޼��� �Դϴ�.
    /// </summary>
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

        for (int i = 0; i < Ability_Slots.Length; i++) //�� �о������
        {
           Ability_Slots[i].RemoveSlot();
        }

        for (int i = 0; i < Ability.PlayerSkill.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
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
            if ((weapontype == WeaponType.One_Hand && slot.skill_name.text == "�Ѽհ�") ||
                (weapontype == WeaponType.Two_Hand && slot.skill_name.text == "��հ�"))
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

        if (monster == null) return;

        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value) &&
            value.weapontype == WeaponType.One_Hand)
        {
            ProcessAbilitySlots(monster, WeaponType.One_Hand);
        }
        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) &&
                 value2.weapontype == WeaponType.Two_Hand)
        {
            ProcessAbilitySlots(monster, WeaponType.Two_Hand);
        }
    

    }

    IEnumerator Start_Skill_Duration_Time_Countdown()
    {
        float currentTime = skill_info.skill_duration_time;
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

    IEnumerator Start_Skill_Cool_Time_Countdown()
    {
        float currentTime = skill_info.skill_cool_time;
        Skill currentskill = skill_info;

        while (currentTime > 0)
        {
            if (Time.timeScale > 0)
            {
                for (int i = 0; i < skill_quick_slot.Length; i++)
                {
                    if (skill_quick_slot[i].skill == currentskill)
                    {
                        skill_quick_slot[i].skill_cool_time.text = currentTime.ToString("0");
                        currentTime -= Time.deltaTime;
                        yield return null;
                    }
                }
            }

        }


        for (int i = 0; i < buff_slot.Length; i++)
        {
            if (skill_quick_slot[i].skill_cool_time.text == "0")
            {
                skill_quick_slot[i].skill_cool_time.text = default;
            }
        }


    }

    public void start_buff_skill(Skill _skill_info)
    {
        skill_info = _skill_info;
        PlayerBuff_Slot.Instance.Buff_slot_AddBuffSkill(_skill_info);
        StartCoroutine("Start_Skill_Duration_Time_Countdown");

        if(skill_quick_slot.Length != 0)
        {
           // StartCoroutine("Start_Skill_Cool_Time_Countdown");
        }
        

        return;
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

        

    //TEST

    private void ProcessAbilitySlots(GameObject monster, WeaponType weaponType)
    {
        foreach (var slot in Ability_Slots)
        {
            if ((weaponType == WeaponType.One_Hand && slot.skill_name.text != ONE_HAND_SWORD) ||
                (weaponType == WeaponType.Two_Hand && slot.skill_name.text != TWO_HAND_SWORD))
            {
                continue;
            }

            // ������ ������ ���� ���¿��� 10.00 �̻� ����� �޼����� ���ϰ� �մϴ�.
            if (IsMaxAbilityReached(slot, weaponType)) return; 
            // ���� ������ �ʹ� ���� ���θ� ����� �ø� �� ������ �մϴ�.
            if (!IsValidMonsterLevel(monster, slot)) return;

            UpdateAbilitySlot(slot);

            PlayerAbility.Instance.onChangeSkill.Invoke();
            OnUpdate_Ability_Interface(weaponType);
        }
    }

    private bool IsMaxAbilityReached(Ability_Slot slot, WeaponType weaponType)
    {
        if (slot.skill.Ability >= Managers.Game.GetPlayer().GetComponent<Player_Class>().class_acquisition_required_Ability())
        {
            if ((weaponType == WeaponType.One_Hand && Managers.Game.GetPlayer().GetComponent<Player_Class>().Get_Player_Class() != Player_Class.ClassType.Warrior) ||
                (weaponType == WeaponType.Two_Hand && Managers.Game.GetPlayer().GetComponent<Player_Class>().Get_Player_Class() != Player_Class.ClassType.Paladin))
            {
                Print_Info_Text.Instance.PrintUserText("������ ������ ����� �ø� �� �ֽ��ϴ�.");
                return true;
            }
        }
        return false;
    }

    private bool IsValidMonsterLevel(GameObject monster, Ability_Slot slot)
    {
        if (3 * monster.GetComponent<Stat>().LEVEL < (int)slot.skill.Ability)
        {
            Print_Info_Text.Instance.PrintUserText("���� ������ �ʹ� �����ϴ�.");
            return false;
        }

        return true;
    }

    private void UpdateAbilitySlot(Ability_Slot slot)
    {
        int abilityAttackValueBeforeChange = stat.Onupdate_Ability_attack();
        float level = float.Parse(slot.LEVEL.text);

        float increaseAmount = GetIncreaseAmount(level);

        slot._slider.value += increaseAmount;
        potentialNewValue += increaseAmount;

        if (potentialNewValue >= MAX_ABILITY_COUNT)
        {
            HandleAbilityOverflow(slot);
        }
        else
        {
            slot._slider.value = potentialNewValue;
        }

        int abilityAttackValueAfterChange = stat.Onupdate_Ability_attack();
        stat.ATTACK += abilityAttackValueAfterChange - abilityAttackValueBeforeChange;
        stat.onchangestat.Invoke();
    }

    private float GetIncreaseAmount(float level)
    {
        foreach (var kvp in LEVEL_INCREASES)
        {
            if (level >= kvp.Key)
            {
                return kvp.Value;
            }
        }
        return 0.0f;
    }

    private void HandleAbilityOverflow(Ability_Slot slot)
    {
        if (slot.skill.Ability == ABILITY_MASTER_LEVEL)
        {
            Print_Info_Text.Instance.PrintUserText("�ִ� �����Ƽ�� �ʰ��Ͽ�, �׷��̵尡 ����Ͽ����ϴ�.");
            slot.skill.Ability_Grade++;
            slot.skill.Ability = 0.0f;
        }

        float excess = potentialNewValue - MAX_ABILITY_COUNT;
        slot.LEVEL.text = (slot.skill.Ability + ABILITY_INCREASE_AMOUNT).ToString();
        slot.skill.Ability += ABILITY_INCREASE_AMOUNT;
        slot._slider.value = (float)Math.Round(excess, 2);
        potentialNewValue = excess;
    }

}


