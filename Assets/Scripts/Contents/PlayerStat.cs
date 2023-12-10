using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    protected int _dex;


    private const int start_user_level = 1;
    private const int start_user_gold = 0;
    private const int start_user_exp = 0;
    private const int start_user_str = 5;
    private const int start_user_dex = 5;
   


    private PlayerEquipment equipment;

    public delegate void OnChangePlayerStat();

    public OnChangePlayerStat onchangestat;

    #region ���� �÷��̽� GUISTATâ ����
    private void OnUpdateStatUI()
    {

        GameObject dextxt = GameObject.Find("DEXnum").gameObject;
        GameObject strtxt = GameObject.Find("STRnum").gameObject;
        GameObject atktxt = GameObject.Find("ATKnum").gameObject;
        GameObject deftxt = GameObject.Find("DEFnum").gameObject;
        GameObject goldtxt = GameObject.Find("Goldnum").gameObject;

        atktxt.GetComponent<TextMeshProUGUI>().text = Attack.ToString();
        deftxt.GetComponent<TextMeshProUGUI>().text = Defense.ToString();
        goldtxt.GetComponent<TextMeshProUGUI>().text = Gold.ToString();
        strtxt.GetComponent<TextMeshProUGUI>().text = STR.ToString();
        dextxt.GetComponent<TextMeshProUGUI>().text = DEX.ToString();

    } //string �����̽�
    #endregion

    #region PrintUserText
    private void TextClear()
    {
        GameObject text = GameObject.Find("Text_User").gameObject;
        text.GetComponent<TextMeshProUGUI>().text = " ";
    }

    public void PrintUserText(string Input)
    {
        GameObject text = GameObject.Find("Text_User").gameObject;
        text.GetComponent<TextMeshProUGUI>().text = Input;
        Managers.Sound.Play("Coin", Define.Sound.Effect);
        Invoke("TextClear", 2.0f);
    } //TODO : ���� �̰��� ���� �ִ� �� �´°� ?
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
                PrintUserText("������ �ö����ϴ�.");
                Invoke("TextClear", 2.0f);
                //23.09.26 ���� (exp �ʱ�ȭ)
                _exp -= (int)Managers.Data.StatDict[level].totalexp;


                onchangestat.Invoke();
            
            }

        }



    }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public int STR { get { return _str; } set { _str = value; } }
    public int DEX { get { return _dex; } set { _dex = value; } }


    private void Start()
    {
        
        _level = start_user_level;
        _gold = start_user_gold;
        _exp = start_user_exp;
        _str = start_user_str;
        _dex = start_user_dex;

        SetStat(_level);
        equipment = GetComponent<PlayerEquipment>();

        OnUpdateStatUI();
        onchangestat += OnUpdateStatUI;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");

    }

    #region ���ݼ��� (�������˻����)

    private int WeaponAttackValue = 0; //�������� ���ݽ��� ���庯��
    private int ChestDEFvalue = 0; //�������� ���� ���庯��
    private int WeaponSTRValue = 0; //�������� STR ���� ���庯��
    private int DEXValue = 0; //�������� DEX ���� ���庯��
    public void SetStat(int level)
    {

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[level];

        _hp = stat.maxHP;
        _maxHp = stat.maxHP;
        _defense = stat.defense + ChestDEFvalue + (DEXValue / 10); //�� DEX�� 1/10�� �������� �⿩��
        _movespeed = stat.movespeed; 
        _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue/10); //�� STR�� 1/10�� �������� �⿩��

    }

    public void SetEquipmentValue(int level, Item item)
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
                    _str = stat.STR + WeaponSTRValue;
                    _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10); //�� STR�� 1/10�� �������� �⿩��

                }
                else if (_attackitem.Equip == false)
                {
                    WeaponAttackValue += equipment.player_equip[EquipType.Weapon].num_1;                 
                    WeaponSTRValue += equipment.player_equip[EquipType.Weapon].num_2;
                    _str = stat.STR + WeaponSTRValue;
                    _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10); //�� STR�� 1/10�� �������� �⿩��
                }
            }
        }
       
        #endregion

        #region �������˻�
        if(item.equiptype == EquipType.Chest)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Chest, out Item _chest_def_item)) //������ �˻�
            {
                if (_chest_def_item.Equip)
                {
                    ChestDEFvalue -= equipment.player_equip[EquipType.Chest].num_1;
                    DEXValue -= equipment.player_equip[EquipType.Chest].num_2;
                    _defense = stat.defense + ChestDEFvalue + (DEXValue / 10); //�� DEX�� 1/10�� �������� �⿩��                   
                    _dex = stat.DEX + DEXValue;
                }
                else if(_chest_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.Chest].num_1;
                    DEXValue += equipment.player_equip[EquipType.Chest].num_2;
                    _defense = stat.defense + ChestDEFvalue + (DEXValue / 10); //�� DEX�� 1/10�� �������� �⿩��                   
                    _dex = stat.DEX + DEXValue;
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
                    DEXValue -= equipment.player_equip[EquipType.Head].num_2;
                    _defense = stat.defense + ChestDEFvalue + (DEXValue / 10); //�� DEX�� 1/10�� �������� �⿩��;
                    _dex = stat.DEX + DEXValue;
                }

                else if (_head_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.Head].num_1;         
                    DEXValue += equipment.player_equip[EquipType.Head].num_2;
                    _defense = stat.defense + ChestDEFvalue + (DEXValue / 10); //�� DEX�� 1/10�� �������� �⿩��;
                    _dex = stat.DEX + DEXValue;
                }

            }
        }
       
        #endregion
      

        return;
    }


    #endregion

}