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
    private PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)

    public delegate void OnUpdateAbillity();
    public OnUpdateAbillity onupdate_abillity;

    public Abillity_Slot[] abillity_Slots;
    public Transform abillity_slotHolder;


    #region �����Ƽ �������̽� ������Ʈ

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

    #region ������ų ���ӽð� UI ���� ����

    private int skill_slot_number;
    private Skill skill_info;
    public Buff_Slot[] buff_slot;
    public Transform buff_slot_holder;
    #endregion


    private void Start() //Player Controller�� ���̸�, �������̹Ƿ� Start ���� �������� �پ slot�� ������Ʈ�� �ȵ�. 
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        abillity = PlayerAbillity.Instance;
        abillity_Slots = abillity_slotHolder.GetComponentsInChildren<Abillity_Slot>();       
        abillity.onChangeSkill += RedrawSlotUI;
        UI_Base.BindEvent(Abillity_Panel, (PointerEventData data) => {Abillity_Panel.transform.position = data.position; }, Define.UIEvent.Drag);
        Managers.UI.SetCanvas(Abillity_canvas, true);
        buff_slot = buff_slot_holder.GetComponentsInChildren<Buff_Slot>();

        Abillity_Panel.SetActive(active_abillity_panel);

        onupdate_abillity += Accumulate_abillity_Func; //delegate
        



        //�⺻���� ���� (�Ѽհ�,��հ�)
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[0]);
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[1]);
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[2]);
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[3]);

        Abillity_Interface_Panel.gameObject.SetActive(false); // �����Ƽ �������̽� �г� �ʱ�ȭ 

}

private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("��ųâ�� ���� ���� KŰ �Է� �Ϸ�");
            active_abillity_panel = !active_abillity_panel;
            Abillity_Panel.SetActive(active_abillity_panel);
            Managers.UI.SetCanvas(Abillity_canvas, true); // ĵ���� SortOrder ������ ������ ���� ������. (���� �������� �������� ���� ����)
            Managers.Sound.Play("Inven_Open");

            if (Abillity_Panel.activeSelf == false)
            {
                select_quickslot_register.gameObject.SetActive(false);

            }

        }
    }
    public Skill Get_Slotnum(int slotnum) //���Կ� �ִ� ��ų �� �����޾� ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        skill_slot_number = slotnum;
        return skill_info = abillity_Slots[slotnum].skill;

    }


    public void Button_Function()
    {

        active_abillity_panel = !active_abillity_panel;
        Abillity_Panel.SetActive(active_abillity_panel);
        Managers.UI.SetCanvas(Abillity_canvas, true); // ĵ���� SortOrder ������ ������ ���� ������. (���� �������� �������� ���� ����)
        Managers.Sound.Play("Inven_Open");

        return;

    } // �����Ƽ ����ư

    public void X_Button_Exit()
    {
        if (Abillity_Panel.activeSelf)
        {
            Abillity_Panel.SetActive(false);
            active_abillity_panel = false;
            Managers.Sound.Play("Inven_Open");
        }

        return;
    } // �����Ƽâ off

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
                    if (abillity_Slots[i].skill_name.text == "�Ѽհ�")
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
                    if (abillity_Slots[i].skill_name.text == "��հ�")
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
                if (abillity_Slots[i].skill_name.text == "�Ѽհ�")
                {
                    if (double.Parse(abillity_Slots[i].Level.text) == ABILLITY_INTERMEDIATE_LEVEL)
                    {
                        abillity_Slots[i].Name_grade.text = "SENIOR";
                    }
                    if (double.Parse(abillity_Slots[i].Level.text) == ABILLITY_MASTER_LEVEL)
                    {                   
                        // TODO : �׷��̵� ����
                        abillity_Slots[i].Name_grade.text = "MASTER";
                        return;
                    }

                    foreach (var slot in abillity_Slots)
                    {
                        if (slot.skill_name.text != "�Ѽհ�")
                        {
                            continue;
                        }
                                                        
                        // Level.text�� float���� ��ȯ�մϴ�.
                        float level = float.Parse(slot.Level.text);

                        // ������ value�� ������ ������ �����մϴ�.
                        float increaseAmount = 0f;

                        // ������ ���� increaseAmount ���� �����մϴ�.
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

                        
                        // ���� �������� �����մϴ�.
                        slot._slider.value += increaseAmount;


                        //���� �������� �����Ͽ� ���� �������� ���� ���� �������� ������Ű�� ���� ����
                        potentialNewValue += increaseAmount;



                        if (potentialNewValue >= MAX_ABILLITY_COUNT)
                        {
                            float excess = potentialNewValue - MAX_ABILLITY_COUNT;
                            excess = (float)Math.Round(excess, 2);
                            slot.Level.text = (slot.skill.abillity + ABILLITY_INCREASE_AMOUNT).ToString(); // �����Ƽ ���� ����
                            slot.skill.abillity += ABILLITY_INCREASE_AMOUNT; // level ������ ���������ݴϴ�.

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

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.Two_Hand) // ���⸦ �������̰�, �μհ��ΰ��
        {

            for (int i = 0; i < abillity_Slots.Length; i++)
            {
                if (abillity_Slots[i].skill_name.text == "��հ�")
                {
                    if (double.Parse(abillity_Slots[i].Level.text) == ABILLITY_INTERMEDIATE_LEVEL)
                    {
                        abillity_Slots[i].Name_grade.text = "SENIOR";
                    }
                    if (double.Parse(abillity_Slots[i].Level.text) == ABILLITY_MASTER_LEVEL)
                    {
                        Debug.Log("����� 100�� �޼��Ͽ����ϴ�.");
                        // TODO : �׷��̵� ����
                        abillity_Slots[i].Name_grade.text = "MASTER";
                        return;
                    }

                    foreach (var slot in abillity_Slots)
                    {
                        // Level.text�� float���� ��ȯ�մϴ�.
                        float level = float.Parse(slot.Level.text);

                        // ������ value�� ������ ������ �����մϴ�.
                        float increaseAmount = 0f;

                        // ������ ���� increaseAmount ���� �����մϴ�.
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

                        // ���� �������� �����մϴ�.
                        // ���� �������� �����մϴ�.
                        slot._slider.value += increaseAmount;


                        //���� �������� �����Ͽ� ���� �������� ���� ���� �������� ������Ű�� ���� ����
                        potentialNewValue += increaseAmount;



                        if (potentialNewValue >= MAX_ABILLITY_COUNT)
                        {
                            float excess = potentialNewValue - MAX_ABILLITY_COUNT;
                            excess = (float)Math.Round(excess, 2);
                            slot.Level.text = (slot.skill.abillity + ABILLITY_INCREASE_AMOUNT).ToString(); // �����Ƽ ���� ����
                            slot.skill.abillity += ABILLITY_INCREASE_AMOUNT; // level ������ ���������ݴϴ�.

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
            Debug.Log("�Ǽ� �Դϴ�.");

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
