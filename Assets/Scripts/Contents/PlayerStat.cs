using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

/// <summary>
/// �� Ŭ������ 1. �÷��̾� ������ ���� ////
///             2.�÷��� �� �����ϴ� USER_STATâ ������Ʈ, ������ ���� ////
///             3. ���� ���� �� �����Ǵ� ������ �����մϴ�. ////
///             
/// 
/// Stat Ŭ������ ��ӹ޽��ϴ�.
/// 
/// </summary>        
/// 
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

    private const int START_USER_LEVEL = 1;
    private const int START_USER_GOLD = 0;
    private const int START_USER_EXP = 0;
    private const int START_USER_STR = 5;
    private const int START_USER_INT = 5;
    private const int START_USER_VIT = 5;
    private const int START_USER_AGI = 5;


    private PlayerEquipment equipment;

    public delegate void OnChangePlayerStat();

    public OnChangePlayerStat onchangestat;


    /// <summary>
    /// ���� �����, ��Ÿ���� ���� �������̽��� �����ϴ�, ��÷� ���ִ� �÷��̾��� ������ ������Ʈ �մϴ�.
    /// </summary>
    /// 
    /// 
    /// <returns>���ϰ��� ����, �÷��̾� ����â�� ��� ������Ʈ ��.</returns>
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

    } 


    public int EXP //�ڵ����� ������Ƽ  
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

                Level_up_Effect();
       
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
        
        _level = START_USER_LEVEL;
        _gold = START_USER_GOLD;
        _exp = START_USER_EXP;
        _str = START_USER_STR;
        _int = START_USER_INT;
        _vit = START_USER_VIT;
        _agi = START_USER_AGI;

        SetStat(START_USER_LEVEL);
        equipment = GetComponent<PlayerEquipment>();

        #region ���� 1ȸ �����Ͽ� ���� ����
        OnUpdateStatUI();
        Equipment_UI equipment_ui = FindObjectOfType<Equipment_UI>();
        equipment_ui.OnUpdateEquip_Stat_Panel_UI();
        #endregion

        onchangestat += OnUpdateStatUI;
        onchangestat += equipment_ui.OnUpdateEquip_Stat_Panel_UI;
    }

    #region ���ݼ��� (�������˻�,�����Ƽ�˻�)

    /// <summary>
    ///  �÷��̾��� ������ �� ���� ���θ� �˻��Ͽ� ������ �����ϴ� �޼���.
    /// <param name="stat">ù ��° ���� : JSON �÷��̾� ���� ������ </param>
    /// <param name="item">�� ��° ���� : �ش� ������ </param>
    /// </summary>
    private void check_Defense_equip(Data.Stat stat, Item item)
    {
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
                else if (_chest_def_item.Equip == false)
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

        if (item.equiptype == EquipType.Head)
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

    }

    /// <summary>
    ///  �÷��̾��� ���� ���� ���θ� �˻��Ͽ� ������ �����ϴ� �޼���.
    /// <param name="stat">ù ��° ���� : JSON �÷��̾� ���� ������ </param>
    /// <param name="item">�� ��° ���� : �ش� ������ </param>
    /// </summary>
    private void check_Weapon_equip(Data.Stat stat, Item item)
    {
        if (item.equiptype == EquipType.Weapon)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Weapon, out Item _attackitem)) //�������� �˻�
            {
                if (_attackitem.Equip) //���⸦ �������̸� ���⸦ ������ ��ġ�� �ݿ�
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
                else if (_attackitem.Equip == false) //���Ⱑ �����Ǿ� ���� �ʴٸ� ���� ������ ����
                {
                    WeaponAttackValue = equipment.player_equip[EquipType.Weapon].num_1;
                    WeaponSTRValue = equipment.player_equip[EquipType.Weapon].num_2;
                    VITvalue = equipment.player_equip[EquipType.Weapon].num_3;
                    AGIvalue = equipment.player_equip[EquipType.Weapon].num_4;
                    _str = stat.STR + WeaponSTRValue;
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                    _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10) + Onupdate_Abillity_attack(); //�� STR�� 1/10�� �������� �⿩�� + ���� �����Ƽ�� ��󵥹���

                    //TESTCODE ; ���ⱳü �ִϸ��̼�
                    if (_attackitem.weapontype == WeaponType.One_Hand)
                    {
                        gameObject.GetComponent<PlayerAnimController>().Change_oneHand_weapon_animClip();
                    }


                }
            }
        }
    }

    /// <summary>
    /// Dicitionary (key : �÷��̾� ����, value : json Data) �� ���� �⺻���� ������ �÷��̾� ������ �����ϰ�,
    /// �߰����� ����(����,������ / ����) �� ����Ͽ� �������� ������ ������.
    /// 
    ///<param name="level">ù��° ���� :�÷��̾� ����</param>
    /// </summary>
    /// <returns>���ϰ�����</returns>
    public void SetStat(int level)  
    {

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[level];

        _hp = stat.maxHP;
        _maxHp = stat.maxHP;
        _defense = stat.defense + ChestDEFvalue + (VIT / 10) + buff_defense; //�� DEX�� 1/10�� �������� �⿩��
        _movespeed = stat.movespeed;
        _attack = stat.attack + WeaponAttackValue + (STR / 10) + Onupdate_Abillity_attack() + buff_damage; //�� STR�� 1/10�� �������� �⿩��+ ���� �����Ƽ�� ��󵥹���

    }

    /// <summary>
    /// Dicitionary (key : �÷��̾� ����, value : json Data) �� ���� 
    /// ATTACK �� DEFENSE ���� ��� �� ���ֵ��� ���� �޼��带 ������.
    ///<param name="level">ù��° ���� :�÷��̾� ����</param>
    /// </summary>
    /// <returns></returns>
    public void SetAttack_and_Defanse_value(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[level];
        _defense = stat.defense + ChestDEFvalue + (VITvalue / 10) + buff_defense; //�� DEX�� 1/10�� �������� �⿩��
        _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10) + Onupdate_Abillity_attack() + buff_damage; //�� STR�� 1/10�� �������� �⿩��+ ���� �����Ƽ�� ��󵥹���

        return;
    }

    /// <summary>
    ///  �÷��̾��� ���� ��ü ���� ���θ� �˻��Ͽ� ������ �����ϴ� �޼���.
    /// <param name="stat">ù ��° ���� :�÷��̾� ����</param>
    /// <param name="item">�� ��° ���� : �ش� ������ </param>
    /// </summary>
    public void SetEquipmentValue(int level,Item item)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[level];

        check_Weapon_equip(stat, item);
        check_Defense_equip(stat, item);

        return;
    }

    /// <summary>
    /// �÷��̾ Ư�� ���⸦ ���� �� �����Ƽ�� ���Ǿ� ����Ǵ� ���ݷ��� �������ִ� �Լ�.
    /// 
    ///
    /// </summary>
    /// <returns> ���� ������ ������ </returns>
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
                  
                    two_hand_sword_abillityAttack = (int)abillity_attack_improvement;

                    break;
                }
            }

            return improvement_abillity_attack = two_hand_sword_abillityAttack;
        }


        return improvement_abillity_attack = 0;

    }

    #endregion


    /// <summary>
    /// �÷��̾ ��� �� ���� ó���� �ϴ� �޼���.
    /// 
    /// </summary>
    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
    }


    /// <summary>
    /// �÷��̾� ������ ����Ʈ�� �����ϴ� �޼���.
    /// 
    /// </summary>
    private void Level_up_Effect()
    {
        Managers.Sound.Play("change_scene", Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Unlock_FX_7");


        effect.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 2.2f, 0.0f);

        Destroy(effect, 3.0f);
    }

}