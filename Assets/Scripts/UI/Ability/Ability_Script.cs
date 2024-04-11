using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SerializableDictionary;

public class Ability_Script : MonoBehaviour
{
    public bool active_Ability_panel = false;
    public GameObject Ability_Panel;
    public GameObject Ability_canvas;
    public GameObject Ability_Explaination_Panel;
    public GameObject select_quickslot_register;


    private PlayerAbility Ability; 
    private PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)

    public delegate void OnUpdateAbility();
    public OnUpdateAbility onupdate_Ability;

    public Ability_Slot[] Ability_Slots;
    public Transform Ability_slotHolder;


    #region �����Ƽ �������̽� ������Ʈ

    public Skill skill;
    public Image skill_icon;
    public TextMeshProUGUI skill_name;
    public TextMeshProUGUI grade_level;
    public TextMeshProUGUI Ability_Level;
    public TextMeshProUGUI _slider_percent;
    public Slider _slider;
    public GameObject Ability_Interface_Panel;
   


    private readonly double Ability_INCREASE_AMOUNT = Math.Round(1.0f,2);
    private const float MAX_Ability_COUNT = 1.0f;
    private const float Ability_INTERMEDIATE_LEVEL = 50.00f;
    private const float Ability_MASTER_LEVEL = 100.00f;


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

    #region ������ų ���ӽð� UI ���� ����

    private int skill_slot_number;
    private Skill skill_info;
    public Buff_Slot[] buff_slot;
    public Transform buff_slot_holder;
    #endregion


    private void Start() //Player Controller�� ���̸�, �������̹Ƿ� Start ���� �������� �پ slot�� ������Ʈ�� �ȵ�. 
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        Ability = PlayerAbility.Instance;
        Ability_Slots = Ability_slotHolder.GetComponentsInChildren<Ability_Slot>();       
        Ability.onChangeSkill += RedrawSlotUI;
        UI_Base.BindEvent(Ability_Panel, (PointerEventData data) => {Ability_Panel.transform.position = data.position; }, Define.UIEvent.Drag);
        Managers.UI.SetCanvas(Ability_canvas, true);
        buff_slot = buff_slot_holder.GetComponentsInChildren<Buff_Slot>();

        Ability_Panel.SetActive(active_Ability_panel);

        onupdate_Ability += Accumulate_Ability_Func; //delegate
        



        //�⺻���� ���� (�Ѽհ�,��հ�)
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[0]);
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[1]);
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[2]);
        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[3]);

        Ability_Interface_Panel.gameObject.SetActive(false); // �����Ƽ �������̽� �г� �ʱ�ȭ 

}

private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Button_Function();

            if (Ability_Panel.activeSelf == false)
            {
                select_quickslot_register.gameObject.SetActive(false);

            }

        }
    }
    public Skill Get_Slotnum(int slotnum) //���Կ� �ִ� ��ų �� �����޾� ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        skill_slot_number = slotnum;
        return skill_info = Ability_Slots[slotnum].skill;

    }


    public void Button_Function()
    {

        active_Ability_panel = !active_Ability_panel;
        Ability_Panel.SetActive(active_Ability_panel);
        Managers.UI.SetCanvas(Ability_canvas, true); // ĵ���� SortOrder ������ ������ ���� ������. (���� �������� �������� ���� ����)
        Managers.Sound.Play("Inven_Open");

        return;

    } // �����Ƽ ����ư

    public void X_Button_Exit()
    {
        if (Ability_Panel.activeSelf)
        {
            Ability_Panel.SetActive(false);
            active_Ability_panel = false;
            Managers.Sound.Play("Inven_Open");
        }

        return;
    } // �����Ƽâ off

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

        switch (weapontype)
        {
            case WeaponType.One_Hand:

                for (int i = 0; i < Ability_Slots.Length; i++)
                {
                    if (Ability_Slots[i].skill_name.text == "�Ѽհ�")
                    {
                        skill_icon.sprite = Ability_Slots[i].skill_icon.sprite;
                        skill_name.text = Ability_Slots[i].skill_name.text;
                        grade_level = Ability_Slots[i].grade_amount;
                        Ability_Level.text = Ability_Slots[i].skill.Ability.ToString();
                        _slider.value = Ability_Slots[i]._slider.value;
                        _slider_percent.text = ((_slider.value) * 100).ToString();
                        break;
                    }
                }

                        break;

            case WeaponType.Two_Hand:


                for (int i = 0; i < Ability_Slots.Length; i++)
                {
                    if (Ability_Slots[i].skill_name.text == "��հ�")
                    {
                        skill_icon.sprite = Ability_Slots[i].skill_icon.sprite;
                        skill_name.text = Ability_Slots[i].skill_name.text;
                        grade_level = Ability_Slots[i].grade_amount;
                        Ability_Level.text = Ability_Slots[i].skill.Ability.ToString();
                        _slider.value = Ability_Slots[i]._slider.value;
                        _slider_percent.text = ((_slider.value) * 100).ToString();
                        break;
                    }
                }


                break;

        }


    }
    public void Accumulate_Ability_Func()

      
    {
        GameObject monster = Managers.Monster_Info.Get_Monster_Info();
        
        if(monster == null)
        {
            return;
        }


        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value) && value.weapontype == WeaponType.One_Hand) 
        {

            for (int i = 0; i < Ability_Slots.Length; i++)
            {
                if (Ability_Slots[i].skill_name.text == "�Ѽհ�")
                {
                    if (double.Parse(Ability_Slots[i].Level.text) == Ability_INTERMEDIATE_LEVEL)
                    {
                        Ability_Slots[i].Name_grade.text = "SENIOR";
                    }
                    if (double.Parse(Ability_Slots[i].Level.text) == Ability_MASTER_LEVEL)
                    {                   
                        // TODO : �׷��̵� ����
                        Ability_Slots[i].Name_grade.text = "MASTER";
                        return;
                    }

                    foreach (var slot in Ability_Slots)
                    {
                        if (slot.skill_name.text != "�Ѽհ�")
                        {
                            continue;
                        }

                        if(3*(monster.GetComponent<Stat>().Level) < (int)(slot.skill.Ability))
                        {
                            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("���� ������ �ʹ� �����ϴ�.");
                            return;
                        }
                       
                        
                        float level = float.Parse(slot.Level.text);

                        
                        float increaseAmount = 0f;

                        // ������ ���� increaseAmount ���� �����մϴ�.
                        if (level >= 90) increaseAmount = AN_INCREASE_MORE_THAN_Ability_90;
                        else if (level >= 80) increaseAmount = AN_INCREASE_MORE_THAN_Ability_80;
                        else if (level >= 70) increaseAmount = AN_INCREASE_MORE_THAN_Ability_70;
                        else if (level >= 60) increaseAmount = AN_INCREASE_MORE_THAN_Ability_60;
                        else if (level >= 50) increaseAmount = AN_INCREASE_MORE_THAN_Ability_50;
                        else if (level >= 40) increaseAmount = AN_INCREASE_MORE_THAN_Ability_40;
                        else if (level >= 30) increaseAmount = AN_INCREASE_MORE_THAN_Ability_30;
                        else if (level >= 20) increaseAmount = AN_INCREASE_MORE_THAN_Ability_20;
                        else if (level >= 10) increaseAmount = AN_INCREASE_MORE_THAN_Ability_10;
                        else if (level <= 10) increaseAmount = AN_INCREASE_LESS_THAN_10;

                        
                        // ���� �������� �����մϴ�.
                        slot._slider.value += increaseAmount;


                        //���� �������� �����Ͽ� ���� �������� ���� ���� �������� ������Ű�� ���� ����
                        potentialNewValue += increaseAmount;



                        if (potentialNewValue >= MAX_Ability_COUNT)
                        {
                            float excess = potentialNewValue - MAX_Ability_COUNT;
                            excess = (float)Math.Round(excess, 2);
                            slot.Level.text = (slot.skill.Ability + Ability_INCREASE_AMOUNT).ToString(); // �����Ƽ ���� ����
                            slot.skill.Ability += Ability_INCREASE_AMOUNT; // level ������ ���������ݴϴ�.

                            slot._slider.value = excess;
                            potentialNewValue = excess;
                        }

                        else
                        {
                            slot._slider.value = potentialNewValue;
                        }




                        OnUpdate_Ability_Interface(WeaponType.One_Hand);

                        return;
                    }
                    

                    OnUpdate_Ability_Interface(WeaponType.One_Hand);
                    return;

                }

                
            }

        }

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.Two_Hand) // ���⸦ �������̰�, �μհ��ΰ��
        {

            for (int i = 0; i < Ability_Slots.Length; i++)
            {
                if (Ability_Slots[i].skill_name.text == "��հ�")
                {
                    if (double.Parse(Ability_Slots[i].Level.text) == Ability_INTERMEDIATE_LEVEL)
                    {
                        Ability_Slots[i].Name_grade.text = "SENIOR";
                    }
                    if (double.Parse(Ability_Slots[i].Level.text) == Ability_MASTER_LEVEL)
                    {
                        Debug.Log("����� 100�� �޼��Ͽ����ϴ�.");
                        // TODO : �׷��̵� ����
                        Ability_Slots[i].Name_grade.text = "MASTER";
                        return;
                    }

                    foreach (var slot in Ability_Slots)
                    {

                        if (slot.skill_name.text != "��հ�")
                        {
                            continue;
                        }

                        if (3 * (monster.GetComponent<Stat>().Level) < (int)(slot.skill.Ability))
                        {
                            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("���� ������ �ʹ� �����ϴ�.");
                            return;
                        }

                        // Level.text�� float���� ��ȯ�մϴ�.
                        float level = float.Parse(slot.Level.text);

                        // ������ value�� ������ ������ �����մϴ�.
                        float increaseAmount = 0f;

                        // ������ ���� increaseAmount ���� �����մϴ�.
                        if (level >= 90) increaseAmount = AN_INCREASE_MORE_THAN_Ability_90;
                        else if (level >= 80) increaseAmount = AN_INCREASE_MORE_THAN_Ability_80;
                        else if (level >= 70) increaseAmount = AN_INCREASE_MORE_THAN_Ability_70;
                        else if (level >= 60) increaseAmount = AN_INCREASE_MORE_THAN_Ability_60;
                        else if (level >= 50) increaseAmount = AN_INCREASE_MORE_THAN_Ability_50;
                        else if (level >= 40) increaseAmount = AN_INCREASE_MORE_THAN_Ability_40;
                        else if (level >= 30) increaseAmount = AN_INCREASE_MORE_THAN_Ability_30;
                        else if (level >= 20) increaseAmount = AN_INCREASE_MORE_THAN_Ability_20;
                        else if (level >= 10) increaseAmount = AN_INCREASE_MORE_THAN_Ability_10;
                        else if (level <= 10) increaseAmount = AN_INCREASE_LESS_THAN_10;

                        // ���� �������� �����մϴ�.
                        // ���� �������� �����մϴ�.
                        slot._slider.value += increaseAmount;


                        //���� �������� �����Ͽ� ���� �������� ���� ���� �������� ������Ű�� ���� ����
                        potentialNewValue += increaseAmount;



                        if (potentialNewValue >= MAX_Ability_COUNT)
                        {
                            float excess = potentialNewValue - MAX_Ability_COUNT;
                            excess = (float)Math.Round(excess, 2);
                            slot.Level.text = (slot.skill.Ability + Ability_INCREASE_AMOUNT).ToString(); // �����Ƽ ���� ����
                            slot.skill.Ability += Ability_INCREASE_AMOUNT; // level ������ ���������ݴϴ�.

                            slot._slider.value = excess;
                            potentialNewValue = excess;
                        }

                        else
                        {
                            slot._slider.value = potentialNewValue;
                        }


                        OnUpdate_Ability_Interface(WeaponType.Two_Hand);

                        return;
                    }

                    OnUpdate_Ability_Interface(WeaponType.Two_Hand);

                    return;

                }

              
            }

        }

        else
        {
            Debug.Log("�Ǽ� �Դϴ�.");

            return;
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
