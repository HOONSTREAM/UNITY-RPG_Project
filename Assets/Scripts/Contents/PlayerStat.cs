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


    public int one_hand_sword_AbilityAttack = 0; //�����Ƽ �� �����ݷ� ���庯�� (�Ѽհ�)
    public int two_hand_sword_AbilityAttack = 0; //�����Ƽ �� �����ݷ� ���庯�� (��հ�)
    public int improvement_Ability_attack;
    public int buff_damage = 0; // ��ų ��� �� ����������
    public int buff_DEFENSE = 0; // ��ų ��� �� ��������

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


    public int EXP //�ڵ����� ������Ƽ  
    {

        get { return _exp; }

        set
        {
            _exp = value;
            //setter�� ���� ���� �Ҵ�Ǹ� ������üũ�� �Ѵ�.


            //������üũ����

            int level = LEVEL;
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalexp)
                    break;
                level++;

            }

            if (level != LEVEL)
            {
                LEVEL = level;              
                GameObject go = GameObject.Find("Level_Text").gameObject;
                go.GetComponent<TextMeshProUGUI>().text = $"{LEVEL}";
                Managers.Sound.Play("univ0007");
                Print_Info_Text.Instance.PrintUserText("������ �ö����ϴ�.");

                LEVEL_up_Effect();
       
                _exp -= (int)Managers.Data.StatDict[LEVEL].totalexp;


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

        NewGame_SetStat(START_USER_LEVEL);

        equipment = GetComponent<PlayerEquipment>();

        #region ���� 1ȸ �����Ͽ� ���� ����
        OnUpdateStatUI_UI_Interface();
        Equipment_UI equipment_ui = FindObjectOfType<Equipment_UI>();
        equipment_ui.OnUpdateEquip_Stat_Panel_UI();
        #endregion

        onchangestat += OnUpdateStatUI_UI_Interface;
        onchangestat += equipment_ui.OnUpdateEquip_Stat_Panel_UI;
       
    }


    #region ���ݼ��� (�������˻�,�����Ƽ�˻�)

    /// <summary>
    /// Dicitionary (key : �÷��̾� ����, value : json Data) �� ���� �⺻���� ������ �÷��̾� ������ ������.
    ///<param name="LEVEL">ù��° ���� :�÷��̾� ����</param>
    /// </summary>
    /// <returns>���ϰ�����</returns>    
    public void NewGame_SetStat(int LEVEL)  
    {

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[LEVEL];

        _hp = stat.MAXHP;
        _maxhp = stat.MAXHP;
        _mp = stat.maxMP;
        _maxMp = stat.maxMP;
        _defense = stat.defense;  
        _movespeed = stat.movespeed;
        _attack = stat.attack;
        

    }
  

    /// <summary>
    /// �÷��̾ Ư�� ���⸦ ���� �� �����Ƽ�� ���Ǿ� ����Ǵ� ���ݷ��� �������ִ� �Լ�.
    /// </summary>
    /// <returns> ���� ������ ������ </returns>
    public int Onupdate_Ability_attack()
    {

        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item One_hand_value) && One_hand_value.weapontype == WeaponType.One_Hand) // ���⸦ �������̰�, �Ѽհ��ΰ��
        {
           

            Ability_Script Ability_script = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetComponent<Ability_Script>();
            for (int i = 0; i < Ability_script.Ability_Slots.Length; i++)
            {
                
                if (Ability_script.Ability_Slots[i].skill_name.text == "�Ѽհ�")
                {
                    double Ability_attack_improvement = (double.Parse(Ability_script.Ability_Slots[i].LEVEL.text) * 5); //TODO :���⼭ Grade ��ġ * 500 �� ���ؾ��� 
                    double Abillity_Grade_improvement = (double.Parse(Ability_script.Ability_Slots[i].grade_amount.text)*500);
                    one_hand_sword_AbilityAttack = (int)Ability_attack_improvement + (int)Abillity_Grade_improvement;

                    break;
                }
            }

            return improvement_Ability_attack = one_hand_sword_AbilityAttack;
        }

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item two_hand_value) && two_hand_value.weapontype == WeaponType.Two_Hand)
        {

            Ability_Script Ability_script = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetComponent<Ability_Script>();
            for (int i = 0; i < Ability_script.Ability_Slots.Length; i++)
            {
                if (Ability_script.Ability_Slots[i].skill_name.text == "��հ�")
                {
                    double Ability_attack_improvement = (double.Parse(Ability_script.Ability_Slots[i].LEVEL.text) * 5); //TODO :���⼭ Grade ��ġ * 500 �� ���ؾ��� 

                    two_hand_sword_AbilityAttack = (int)Ability_attack_improvement;

                    break;
                }
            }

            return improvement_Ability_attack = two_hand_sword_AbilityAttack;
        }

        return improvement_Ability_attack = 0;

    }

  
    /// <summary>
    ///  �÷��̾��� ���� ��ü ���� ���θ� �˻��Ͽ� ������ �����ϴ� �޼���.
    /// <param name="stat">ù ��° ���� :�÷��̾� ����</param>
    /// <param name="item">�� ��° ���� : �ش� ������ </param>
    /// </summary>
    public void SetEquipmentValue(int LEVEL,Item item)
    {
        if (item == null)
        {
            return;
        }


        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[LEVEL];


        switch (item.equiptype)
        {
            case EquipType.Weapon:
                check_Weapon_equip(stat, item);
                break;
            default:
                check_DEFENSE_equip(stat, item);
                break;
        }
            
        return;
    }

    /// <summary>
    ///  �÷��̾��� ���� ���� ���θ� �˻��Ͽ� ������ �����ϴ� �޼���.
    /// <param name="stat">ù ��° ���� : JSON �÷��̾� ���� ������ </param>
    /// <param name="item">�� ��° ���� : �ش� ������ </param>
    /// </summary>
    public void check_Weapon_equip(Data.Stat stat, Item item)
    {
        if(item == null)
        {
            return;
        }

        if (item.equiptype == EquipType.Weapon)
        {
            gameObject.GetComponent<PlayerAnimController>().Get_request_Change_Weapon_EquipType(item); // AnimController�� ���� ������ ���Ⱑ �������� ���ε�
            gameObject.GetComponent<PlayerWeaponController>().Get_request_Change_Weapon_EquipType(item); // WeaponController�� ���� ������ ���Ⱑ �������� ���ε�

            if (equipment.player_equip.TryGetValue(EquipType.Weapon, out Item _attackitem)) //�������� �˻�
            {
                if (_attackitem.Equip) //���⸦ �������̸� ���⸦ ������ ��ġ�� �ݿ�
                {
                    
                    STR = STR - equipment.player_equip[EquipType.Weapon].num_2;
                    VIT = VIT - equipment.player_equip[EquipType.Weapon].num_3;
                    AGI = AGI - equipment.player_equip[EquipType.Weapon].num_4;
                    ATTACK = stat.attack; // ���� �����̹Ƿ� ���� ������ �ش��ϴ� ���ü�ġ�� ������.


                    gameObject.GetComponent<PlayerAnimController>().Change_No_Weapon_animClip();

                }
                else if (_attackitem.Equip == false) //���Ⱑ �����Ǿ� ���� �ʴٸ� ���� ������ ����
                {

                    STR = STR + equipment.player_equip[EquipType.Weapon].num_2;
                    VIT = VIT + equipment.player_equip[EquipType.Weapon].num_3;
                    AGI = AGI + equipment.player_equip[EquipType.Weapon].num_4;
                    ATTACK += equipment.player_equip[EquipType.Weapon].num_1 + Onupdate_Ability_attack(); //�� STR�� 1/10�� �������� �⿩�� + ���� �����Ƽ�� ��󵥹���

                    //TESTCODE ; ���ⱳü �ִϸ��̼�
                    if (_attackitem.weapontype == WeaponType.One_Hand)
                    {
                        gameObject.GetComponent<PlayerAnimController>().Change_oneHand_weapon_animClip();
                    }
                    else if (_attackitem.weapontype == WeaponType.Two_Hand)
                    {
                        gameObject.GetComponent<PlayerAnimController>().Change_TwoHand_weapon_animClip();
                    }

                }
            }
        }

  
    }

    /// <summary>
    ///  �÷��̾��� ������ �� ���� ���θ� �˻��Ͽ� ������ �����ϴ� �޼���.
    /// <param name="stat">ù ��° ���� : JSON �÷��̾� ���� ������ </param>
    /// <param name="item">�� ��° ���� : �ش� ������ </param>
    /// </summary>
    private void check_DEFENSE_equip(Data.Stat stat, Item item)
    {
        if (item == null)
        {
            return;
        }

        if (item.equiptype == EquipType.outter_plate)
        {
            if (equipment.player_equip.TryGetValue(EquipType.outter_plate, out Item _chest_def_item)) //������ �˻�
            {
                if (_chest_def_item.Equip)
                {

                    VIT = VIT - equipment.player_equip[EquipType.outter_plate].num_3;
                    DEFENSE -= equipment.player_equip[EquipType.outter_plate].num_1;
                    INT = INT - equipment.player_equip[EquipType.outter_plate].num_2;
                    AGI = AGI - equipment.player_equip[EquipType.outter_plate].num_4;

                }
                else if (_chest_def_item.Equip == false)
                {

                    VIT = VIT + equipment.player_equip[EquipType.outter_plate].num_3;
                    DEFENSE += equipment.player_equip[EquipType.outter_plate].num_1;
                    INT = INT + equipment.player_equip[EquipType.outter_plate].num_2;
                    AGI = AGI + equipment.player_equip[EquipType.outter_plate].num_4;
                }
            }
        }
        if (item.equiptype == EquipType.Chest)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Chest, out Item _chest_def_item)) //������ �˻�
            {
                if (_chest_def_item.Equip)
                {

                    VIT = VIT - equipment.player_equip[EquipType.Chest].num_3;
                    DEFENSE -= equipment.player_equip[EquipType.Chest].num_1;                
                    INT = INT - equipment.player_equip[EquipType.Chest].num_2;
                    AGI = AGI - equipment.player_equip[EquipType.Chest].num_4;

                }
                else if (_chest_def_item.Equip == false)
                {
                    VIT = VIT + equipment.player_equip[EquipType.Chest].num_3;
                    DEFENSE += equipment.player_equip[EquipType.Chest].num_1;          
                    INT = INT + equipment.player_equip[EquipType.Chest].num_2;
                    AGI = AGI + equipment.player_equip[EquipType.Chest].num_4;

                }
            }
        }
        if (item.equiptype == EquipType.pants)
        {
            if (equipment.player_equip.TryGetValue(EquipType.pants, out Item _pants_def_item)) //������ �˻�
            {
                if (_pants_def_item.Equip)
                {

                    VIT = VIT - equipment.player_equip[EquipType.pants].num_3;
                    DEFENSE -= equipment.player_equip[EquipType.pants].num_1;
                    INT = INT - equipment.player_equip[EquipType.pants].num_2;
                    AGI = AGI - equipment.player_equip[EquipType.pants].num_4;

                }
                else if (_pants_def_item.Equip == false)
                {
                    VIT = VIT + equipment.player_equip[EquipType.pants].num_3;
                    DEFENSE += equipment.player_equip[EquipType.pants].num_1;
                    INT = INT + equipment.player_equip[EquipType.pants].num_2;
                    AGI = AGI + equipment.player_equip[EquipType.pants].num_4;

                }
            }
        }
        if (item.equiptype == EquipType.Head)
            {
                if (equipment.player_equip.TryGetValue(EquipType.Head, out Item _head_def_item))
                {
                    if (_head_def_item.Equip)
                    {
                       VIT = VIT - equipment.player_equip[EquipType.Head].num_3;
                       DEFENSE -= equipment.player_equip[EquipType.Head].num_1;                
                       INT = INT - equipment.player_equip[EquipType.Head].num_2;
                       AGI = AGI - equipment.player_equip[EquipType.Head].num_4;

                    }

                    else if (_head_def_item.Equip == false)
                    {
                       VIT = VIT + equipment.player_equip[EquipType.Head].num_3;
                       DEFENSE += equipment.player_equip[EquipType.Head].num_1;                
                       INT = INT + equipment.player_equip[EquipType.Head].num_2;
                       AGI = AGI + equipment.player_equip[EquipType.Head].num_4;
  
                    }

                }
            }
        if (item.equiptype == EquipType.shoes)
            {
                if (equipment.player_equip.TryGetValue(EquipType.shoes, out Item _shoes_def_item))
                {
                   if (_shoes_def_item.Equip)
                  {

                    VIT = VIT - equipment.player_equip[EquipType.shoes].num_3;
                    DEFENSE -= equipment.player_equip[EquipType.shoes].num_1;//�� DEX�� 1/10�� �������� �⿩��;                  
                    INT = INT - equipment.player_equip[EquipType.shoes].num_2;
                    AGI = AGI - equipment.player_equip[EquipType.shoes].num_4;

                   }

                   else if (_shoes_def_item.Equip == false)
                   {

                    VIT = VIT + equipment.player_equip[EquipType.shoes].num_3;
                    DEFENSE = DEFENSE + equipment.player_equip[EquipType.shoes].num_1;  //�� DEX�� 1/10�� �������� �⿩��;                  
                    INT = INT + equipment.player_equip[EquipType.shoes].num_2;
                    AGI = AGI + equipment.player_equip[EquipType.shoes].num_4;
                
                   }

                }
            }
        if (item.equiptype == EquipType.Shield)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Shield, out Item _shield_def_item)) //������ �˻�
            {
                
                if (_shield_def_item.Equip)
                {

                    VIT = VIT - equipment.player_equip[EquipType.Shield].num_3;
                    DEFENSE -= equipment.player_equip[EquipType.Shield].num_1;
                    INT = INT - equipment.player_equip[EquipType.Shield].num_2;
                    AGI = AGI - equipment.player_equip[EquipType.Shield].num_4;

                }
                else if (_shield_def_item.Equip == false)
                {
                    VIT = VIT + equipment.player_equip[EquipType.Shield].num_3;
                    DEFENSE += equipment.player_equip[EquipType.Shield].num_1;
                    INT = INT + equipment.player_equip[EquipType.Shield].num_2;
                    AGI = AGI + equipment.player_equip[EquipType.Shield].num_4;

                }
            }
        }
        if (item.equiptype == EquipType.Head_decoration)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Head_decoration, out Item _head_deco_def_item))
            {
                if (_head_deco_def_item.Equip)
                {

                    MAXHP = MAXHP - equipment.player_equip[EquipType.Head_decoration].num_1;
                    INT = INT - equipment.player_equip[EquipType.Head_decoration].num_2;
                    VIT = VIT - equipment.player_equip[EquipType.Head_decoration].num_3;
                    AGI = AGI - equipment.player_equip[EquipType.Head_decoration].num_4;

                }

                else if (_head_deco_def_item.Equip == false)
                {

                    MAXHP = MAXHP + equipment.player_equip[EquipType.Head_decoration].num_1;
                    INT = INT + equipment.player_equip[EquipType.Head_decoration].num_2;
                    VIT = VIT + equipment.player_equip[EquipType.Head_decoration].num_3;
                    AGI = AGI + equipment.player_equip[EquipType.Head_decoration].num_4;

                }

            }
        }
        if (item.equiptype == EquipType.necklace)
        {
            if (equipment.player_equip.TryGetValue(EquipType.necklace, out Item _neck_deco_def_item))
            {
                if (_neck_deco_def_item.Equip)
                {

                    MaxMp = MaxMp - equipment.player_equip[EquipType.necklace].num_1;
                    INT = INT - equipment.player_equip[EquipType.necklace].num_2;
                    VIT = VIT - equipment.player_equip[EquipType.necklace].num_3;
                    AGI = AGI - equipment.player_equip[EquipType.necklace].num_4;

                }

                else if (_neck_deco_def_item.Equip == false)
                {

                    MaxMp = MaxMp + equipment.player_equip[EquipType.necklace].num_1;
                    INT = INT + equipment.player_equip[EquipType.necklace].num_2;
                    VIT = VIT + equipment.player_equip[EquipType.necklace].num_3;
                    AGI = AGI + equipment.player_equip[EquipType.necklace].num_4;

                }

            }
        }
        if (item.equiptype == EquipType.Ring)
        {
            //TODO : 2������ �������� �˻�
        }
        if (item.equiptype == EquipType.cape)
        {
            if (equipment.player_equip.TryGetValue(EquipType.cape, out Item _cape_def_item))
            {
                if (_cape_def_item.Equip)
                {

                    STR = STR - equipment.player_equip[EquipType.cape].num_1;
                    INT = INT - equipment.player_equip[EquipType.cape].num_2;
                    VIT = VIT - equipment.player_equip[EquipType.cape].num_3;
                    AGI = AGI - equipment.player_equip[EquipType.cape].num_4;

                }

                else if (_cape_def_item.Equip == false)
                {

                    STR = STR + equipment.player_equip[EquipType.cape].num_1;
                    INT = INT + equipment.player_equip[EquipType.cape].num_2;
                    VIT = VIT + equipment.player_equip[EquipType.cape].num_3;
                    AGI = AGI + equipment.player_equip[EquipType.cape].num_4;

                }

            }
        }
        if (item.equiptype == EquipType.vehicle)
        {
            //TODO
        }

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
    /// ���� �����, ��Ÿ���� ���� �������̽��� �����ϴ�, ��÷� ���ִ� �÷��̾��� ������ ������Ʈ �մϴ�.
    /// </summary>
    /// 
    /// 
    /// <returns>���ϰ��� ����, �÷��̾� ����â�� ��� ������Ʈ ��.</returns>
    private void OnUpdateStatUI_UI_Interface()
    {

        GameObject inttxt = GameObject.Find("INTnum").gameObject;
        GameObject strtxt = GameObject.Find("STRnum").gameObject;
        GameObject atktxt = GameObject.Find("ATKnum").gameObject;
        GameObject deftxt = GameObject.Find("DEFnum").gameObject;
        GameObject goldtxt = GameObject.Find("Goldnum").gameObject;
        GameObject vittxt = GameObject.Find("VITnum").gameObject;
        GameObject agitxt = GameObject.Find("AGInum").gameObject;

        atktxt.GetComponent<TextMeshProUGUI>().text = ATTACK.ToString();
        deftxt.GetComponent<TextMeshProUGUI>().text = DEFENSE.ToString();
        goldtxt.GetComponent<TextMeshProUGUI>().text = Gold.ToString();
        strtxt.GetComponent<TextMeshProUGUI>().text = STR.ToString();
        inttxt.GetComponent<TextMeshProUGUI>().text = INT.ToString();
        vittxt.GetComponent<TextMeshProUGUI>().text = VIT.ToString();
        agitxt.GetComponent<TextMeshProUGUI>().text = AGI.ToString();

    }
    /// <summary>
    /// �÷��̾� ������ ����Ʈ�� �����ϴ� �޼���.
    /// 
    /// </summary>
    private void LEVEL_up_Effect()
    {
        Managers.Sound.Play("change_scene", Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate("Skill_Effect/LEVEL_up_Effect");
        GameObject effect_3d = Managers.Resources.Instantiate("Skill_Effect/LEVEL_up_3D_Effect");


        effect.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 2.2f, 0.0f);

        effect_3d.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
        effect_3d.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 0.0f, 0.0f);


        Destroy(effect, 5.0f);
        Destroy(effect_3d, 5.0f);

    }

}