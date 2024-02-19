using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : Stat

{

    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;
    [SerializeField]
    protected int _str;
    [SerializeField]
    protected int _int;
    [SerializeField]
    protected int _vit;
    [SerializeField]
    protected int _agi;

    private int WeaponAttackValue = 0; //�������� ���ݽ��� ���庯��
    private int ChestDEFvalue = 0; //�������� ���� ���庯��
    private int WeaponSTRValue = 0; //�������� STR ���� ���庯�� (���ݷ�)
    private int VITvalue = 0; // VIT ���� ���庯�� (����,ü��,ü��ȸ���ӵ�)
    private int AGIvalue = 0; // AGI ���� ���庯�� (���ݼӵ�, ȸ����, �̵��ӵ�)
    private int INTValue = 0; //INT �������ݷ�
    public int one_hand_sword_abillityAttack = 0; //�����Ƽ �� �����ݷ� ���庯�� (�Ѽհ�)
    public int two_hand_sword_abillityAttack = 0; //�����Ƽ �� �����ݷ� ���庯�� (��հ�)
    public int improvement_abillity_attack;
    public int buff_damage = 0; // ��ų ��� �� ����������
    public int buff_defense = 0; // ��ų ��� �� ��������

    private const int start_user_level = 1;
    private const int start_user_gold = 0;
    private const int start_user_exp = 0;
    private const int start_user_str = 5;
    private const int start_user_int = 5;
    private const int start_user_vit = 5;
    private const int start_user_agi = 5;


    private PlayerEquipment equipment;

    public delegate void OnChangePlayerStat();

    public OnChangePlayerStat onchangestat;

  
    #region ���� �÷��̽� GUISTATâ, ���â STATâ  ����
    private void OnUpdateStatUI()
    {

        GameObject inttxt = GameObject.Find("INTnum").gameObject;
        GameObject strtxt = GameObject.Find("STRnum").gameObject;
        GameObject atktxt = GameObject.Find("ATKnum").gameObject;
        GameObject deftxt = GameObject.Find("DEFnum").gameObject;
        GameObject goldtxt = GameObject.Find("Goldnum").gameObject;
        GameObject vittxt = GameObject.Find("VITnum").gameObject;
        GameObject agitxt = GameObject.Find("AGInum").gameObject;

        atktxt.GetComponent<TextMeshProUGUI>().text = Attack.ToString();
        deftxt.GetComponent<TextMeshProUGUI>().text = Defense.ToString();
        goldtxt.GetComponent<TextMeshProUGUI>().text = Gold.ToString();
        strtxt.GetComponent<TextMeshProUGUI>().text = STR.ToString();
        inttxt.GetComponent<TextMeshProUGUI>().text = INT.ToString();
        vittxt.GetComponent<TextMeshProUGUI>().text = VIT.ToString();
        agitxt.GetComponent<TextMeshProUGUI>().text = AGI.ToString();

    } //string �����̽�

  
    #endregion


    /*https://jeongkyun-it.tistory.com/23 */
    public int EXP //������üũ ���� ����  
    {

        get { return _exp; }

        set
        {
            _exp = value;
            //setter�� ���� ���� �Ҵ�Ǹ� ������üũ�� �Ѵ�.


            //������üũ����

            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalexp)
                    break;
                level++;

            }

            if (level != Level)
            {
                Level = level;
                SetStat(Level);
                GameObject go = GameObject.Find("Level_Text").gameObject;
                go.GetComponent<TextMeshProUGUI>().text = $"Lv . {Level}";
                Managers.Sound.Play("univ0007");
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("������ �ö����ϴ�.");               
                #region Level Up EFFECT
                Managers.Sound.Play("change_scene", Define.Sound.Effect);

                GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Unlock_FX_7");


                effect.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
                effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 2.2f, 0.0f);

                Destroy(effect, 3.0f);
                #endregion              
                _exp -= (int)Managers.Data.StatDict[level].totalexp;


                onchangestat.Invoke();
            
            }

        }



    }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public int STR { get { return _str; } set { _str = value; } }
    public int INT { get { return _int; } set { _int = value; } }    
    public int VIT { get { return _vit; } set { _vit = value; } }
    public int AGI { get { return _agi; } set { _agi = value; } }


    private void Start()
    {
        
        Equipment_UI equipment_ui = FindObjectOfType<Equipment_UI>();

        _level = start_user_level;
        _gold = start_user_gold;
        _exp = start_user_exp;
        _str = start_user_str;
        _int = start_user_int;
        _vit = start_user_vit;
        _agi = start_user_agi;

        SetStat(_level);
        equipment = GetComponent<PlayerEquipment>();

        OnUpdateStatUI();
        equipment_ui.OnUpdateEquip_Stat_Panel_UI();
        onchangestat += OnUpdateStatUI;
        onchangestat += equipment_ui.OnUpdateEquip_Stat_Panel_UI;
    }
   
    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");

    }

    #region ���ݼ��� (�������˻�,�����Ƽ�˻�)
    

    public void SetStat(int level)  // INT , AGI ���ݼ��� �ʿ� TODO
    {

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[level];

        _hp = stat.maxHP;
        _maxHp = stat.maxHP;
        _defense = stat.defense + ChestDEFvalue + (_vit / 10) + buff_defense; //�� DEX�� 1/10�� �������� �⿩��
        _movespeed = stat.movespeed;
        _attack = stat.attack + WeaponAttackValue + (_str / 10) + Onupdate_Abillity_attack() + buff_damage; //�� STR�� 1/10�� �������� �⿩��+ ���� �����Ƽ�� ��󵥹���

    }

    public void SetAttack_and_Defanse_value(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[level];
        _defense = stat.defense + ChestDEFvalue + (VITvalue / 10) + buff_defense; //�� DEX�� 1/10�� �������� �⿩��
        _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10) + Onupdate_Abillity_attack() + buff_damage; //�� STR�� 1/10�� �������� �⿩��+ ���� �����Ƽ�� ��󵥹���

        return;
    }

    public void SetEquipmentValue(int level,Item item)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[level];

        #region ���������˻�
        if(item.equiptype == EquipType.Weapon)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Weapon, out Item _attackitem)) //�������� �˻�
            {
                if (_attackitem.Equip)
                {
                    WeaponAttackValue -= equipment.player_equip[EquipType.Weapon].num_1;                  
                    WeaponSTRValue -= equipment.player_equip[EquipType.Weapon].num_2;
                    VITvalue -= equipment.player_equip[EquipType.Weapon].num_3;
                    AGIvalue -= equipment.player_equip[EquipType.Weapon].num_4;
                    _str = stat.STR + WeaponSTRValue;
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                    _attack = stat.attack; // ���� �����̹Ƿ� ���� ������ �ش��ϴ� ���ü�ġ�� ������.

                }
                else if (_attackitem.Equip == false)
                {
                    WeaponAttackValue = equipment.player_equip[EquipType.Weapon].num_1;                 
                    WeaponSTRValue = equipment.player_equip[EquipType.Weapon].num_2;
                    VITvalue = equipment.player_equip[EquipType.Weapon].num_3;
                    AGIvalue = equipment.player_equip[EquipType.Weapon].num_4;
                    _str = stat.STR + WeaponSTRValue;
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                    _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10) + Onupdate_Abillity_attack(); //�� STR�� 1/10�� �������� �⿩�� + ���� �����Ƽ�� ��󵥹���
                }
            }
        }

        #endregion

        #region �������˻�

        if (item.equiptype == EquipType.outter_plate)
        {
            if (equipment.player_equip.TryGetValue(EquipType.outter_plate, out Item _chest_def_item)) //������ �˻�
            {
                if (_chest_def_item.Equip)
                {
                    ChestDEFvalue -= equipment.player_equip[EquipType.outter_plate].num_1;
                    INTValue -= equipment.player_equip[EquipType.outter_plate].num_2;
                    VITvalue -= equipment.player_equip[EquipType.outter_plate].num_3;
                    AGIvalue -= equipment.player_equip[EquipType.outter_plate].num_4;

                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //�� VIT�� 1/10�� ���¿� �⿩��                                      
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                }
                else if (_chest_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.outter_plate].num_1;                    
                    VITvalue += equipment.player_equip[EquipType.outter_plate].num_3;
                    AGIvalue += equipment.player_equip[EquipType.outter_plate].num_4;

                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //�� DEX�� 1/10�� �������� �⿩��                                      
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                }
            }
        }

        if (item.equiptype == EquipType.Chest)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Chest, out Item _chest_def_item)) //������ �˻�
            {
                if (_chest_def_item.Equip)
                {
                    ChestDEFvalue -= equipment.player_equip[EquipType.Chest].num_1;                  
                    VITvalue -= equipment.player_equip[EquipType.Chest].num_3;
                    AGIvalue -= equipment.player_equip[EquipType.Chest].num_4;
                   
                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //�� DEX�� 1/10�� �������� �⿩��                                       
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                }
                else if(_chest_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.Chest].num_1;               
                    VITvalue += equipment.player_equip[EquipType.Chest].num_3;
                    AGIvalue += equipment.player_equip[EquipType.Chest].num_4;

                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //�� DEX�� 1/10�� �������� �⿩��                                       
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                }
            }
        }
       
        if(item.equiptype == EquipType.Head)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Head, out Item _head_def_item))
            {
                if (_head_def_item.Equip)
                {
                    ChestDEFvalue -= equipment.player_equip[EquipType.Head].num_1;                                  
                    VITvalue -= equipment.player_equip[EquipType.Head].num_3;
                    AGIvalue -= equipment.player_equip[EquipType.Head].num_4;
                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //�� DEX�� 1/10�� �������� �⿩��;                  
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;

                }

                else if (_head_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.Head].num_1;                        
                    VITvalue += equipment.player_equip[EquipType.Head].num_3;
                    AGIvalue += equipment.player_equip[EquipType.Head].num_4;
                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //�� DEX�� 1/10�� �������� �⿩��;                    
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;

                }

            }
        }
       
        #endregion
      

        return;
    }


    public int Onupdate_Abillity_attack() 
    {

        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item One_hand_value) && One_hand_value.weapontype == WeaponType.One_Hand) // ���⸦ �������̰�, �Ѽհ��ΰ��
        {

            Abillity_Script abillity_script = FindObjectOfType<Abillity_Script>();
            for(int i = 0; i < abillity_script.abillity_Slots.Length; i++)
            {
                if (abillity_script.abillity_Slots[i].skill_name.text == "�Ѽհ�")
                {
                    double abillity_attack_improvement = (double.Parse(abillity_script.abillity_Slots[i].Level.text)*5); //TODO :���⼭ Grade ��ġ * 500 �� ���ؾ��� 

                    Debug.Log($"���� ������, �������� :{abillity_script.abillity_Slots[i].skill_name.text}, {abillity_attack_improvement}");
                    one_hand_sword_abillityAttack = (int)abillity_attack_improvement;
                    
                 break;
                }
            }
            
            return improvement_abillity_attack = one_hand_sword_abillityAttack;
        }
        
        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item two_hand_value) && two_hand_value.weapontype == WeaponType.Two_Hand)
        {

            Abillity_Script abillity_script = FindObjectOfType<Abillity_Script>();
            for (int i = 0; i < abillity_script.abillity_Slots.Length; i++)
            {
                if (abillity_script.abillity_Slots[i].skill_name.text == "��հ�")
                {
                    double abillity_attack_improvement = (double.Parse(abillity_script.abillity_Slots[i].Level.text) * 5); //TODO :���⼭ Grade ��ġ * 500 �� ���ؾ��� 

                    Debug.Log($"���� ������, �������� :{abillity_script.abillity_Slots[i].skill_name.text}, {abillity_attack_improvement}");
                    two_hand_sword_abillityAttack = (int)abillity_attack_improvement;

                    break;
                }
            }

            return improvement_abillity_attack = two_hand_sword_abillityAttack;
        }


        return improvement_abillity_attack = 0;

    }

    #endregion

}