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

    private int WeaponAttackValue = 0; //장착무기 공격스텟 저장변수
    private int ChestDEFvalue = 0; //장착갑옷 방어스텟 저장변수
    private int WeaponSTRValue = 0; //장착무기 STR 스텟 저장변수
    private int DEXValue = 0; //장착갑옷 DEX 스텟 저장변수   
    public int one_hand_sword_abillityAttack = 0; //어빌리티 별 향상공격력 저장변수 (한손검)
    public int two_hand_sword_abillityAttack = 0; //어빌리티 별 향상공격력 저장변수 (양손검)
    public int improvement_abillity_attack;
    public int buff_damage = 0; // 스킬 사용 시 버프데미지
    public int buff_defense = 0; // 스킬 사용 시 버프방어력

    private const int start_user_level = 1;
    private const int start_user_gold = 0;
    private const int start_user_exp = 0;
    private const int start_user_str = 5;
    private const int start_user_dex = 5;
   


    private PlayerEquipment equipment;

    public delegate void OnChangePlayerStat();

    public OnChangePlayerStat onchangestat;

    #region 게임 플레이시 GUISTAT창 세팅
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

    } //string 성능이슈
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
    } //TODO : 과연 이것이 여기 있는 게 맞는가 ?
    #endregion

    /*https://jeongkyun-it.tistory.com/23 */
    public int EXP //레벨업체크 까지 관리  
    {

        get { return _exp; }

        set
        {
            _exp = value;
            //setter에 의해 값이 할당되면 레벨업체크를 한다.


            //레벨업체크로직

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
                PrintUserText("레벨이 올랐습니다.");
                #region Level Up EFFECT
                Managers.Sound.Play("change_scene", Define.Sound.Effect);

                GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Unlock_FX_7");


                effect.transform.parent = Managers.Game.GetPlayer().transform; // 부모설정
                effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 2.2f, 0.0f);

                Destroy(effect, 3.0f);
                #endregion
                Invoke("TextClear", 2.0f);
                //23.09.26 수정 (exp 초기화)
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

    #region 스텟세팅 (장착장비검사,어빌리티검사)
    //TODO : 어빌리티검사 필요

    public void SetStat(int level)
    {

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //키가 레벨 
        Data.Stat stat = dict[level];

        _hp = stat.maxHP;
        _maxHp = stat.maxHP;
        _defense = stat.defense + ChestDEFvalue + (DEXValue / 10) + buff_defense; //총 DEX의 1/10을 데미지에 기여함
        _movespeed = stat.movespeed;
        _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10) + Onupdate_Abillity_attack() + buff_damage; //총 STR의 1/10을 데미지에 기여함+ 무기 어빌리티별 향상데미지

    }

    public void SetEquipmentValue(int level,Item item)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //키가 레벨 
        Data.Stat stat = dict[level];

        #region 무기장착검사
        if(item.equiptype == EquipType.Weapon)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Weapon, out Item _attackitem)) //장착무기 검사
            {
                if (_attackitem.Equip)
                {
                    WeaponAttackValue -= equipment.player_equip[EquipType.Weapon].num_1;                  
                    WeaponSTRValue -= equipment.player_equip[EquipType.Weapon].num_2;
                    _str = stat.STR + WeaponSTRValue;
                    _attack = stat.attack; // 무기 해제이므로 순수 레벨에 해당하는 무기수치로 기록함.

                }
                else if (_attackitem.Equip == false)
                {
                    WeaponAttackValue = equipment.player_equip[EquipType.Weapon].num_1;                 
                    WeaponSTRValue = equipment.player_equip[EquipType.Weapon].num_2;
                    _str = stat.STR + WeaponSTRValue;
                    _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10) + Onupdate_Abillity_attack(); //총 STR의 1/10을 데미지에 기여함 + 무기 어빌리티별 향상데미지
                }
            }
        }
       
        #endregion

        #region 방어구장착검사
        if(item.equiptype == EquipType.Chest)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Chest, out Item _chest_def_item)) //장착방어구 검사
            {
                if (_chest_def_item.Equip)
                {
                    ChestDEFvalue -= equipment.player_equip[EquipType.Chest].num_1;
                    DEXValue -= equipment.player_equip[EquipType.Chest].num_2;
                    _defense = stat.defense + ChestDEFvalue + (DEXValue / 10); //총 DEX의 1/10을 데미지에 기여함                   
                    _dex = stat.DEX + DEXValue;
                }
                else if(_chest_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.Chest].num_1;
                    DEXValue += equipment.player_equip[EquipType.Chest].num_2;
                    _defense = stat.defense + ChestDEFvalue + (DEXValue / 10); //총 DEX의 1/10을 데미지에 기여함                   
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
                    _defense = stat.defense + ChestDEFvalue + (DEXValue / 10); //총 DEX의 1/10을 데미지에 기여함;
                    _dex = stat.DEX + DEXValue;
                }

                else if (_head_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.Head].num_1;         
                    DEXValue += equipment.player_equip[EquipType.Head].num_2;
                    _defense = stat.defense + ChestDEFvalue + (DEXValue / 10); //총 DEX의 1/10을 데미지에 기여함;
                    _dex = stat.DEX + DEXValue;
                }

            }
        }
       
        #endregion
      

        return;
    }


    public int Onupdate_Abillity_attack() //각 도구별 어빌리티 검사하고, 능력치 향상 , TODO : 그리고 향상 되면 바로 업데이트 되도록
    {

        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item One_hand_value) && One_hand_value.weapontype == WeaponType.One_Hand) // 무기를 장착중이고, 한손검인경우
        {
            Abillity_Script abillity_script = FindObjectOfType<Abillity_Script>();
            for(int i = 0; i < abillity_script.abillity_Slots.Length; i++)
            {
                if (abillity_script.abillity_Slots[i].skill_name.text == "한손검")
                {
                    double abillity_attack_improvement = (double.Parse(abillity_script.abillity_Slots[i].Level.text)*5); //TODO :여기서 Grade 수치 * 500 도 더해야함 

                    Debug.Log($"향상된 데미지, 무기종류 :{abillity_script.abillity_Slots[i].skill_name.text}, {abillity_attack_improvement}");
                    one_hand_sword_abillityAttack = (int)abillity_attack_improvement;
                    
                 break;
                }
            }

            
            return improvement_abillity_attack = one_hand_sword_abillityAttack;
        }
        
        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item two_hand_value) && two_hand_value.weapontype == WeaponType.Two_Hand)
        {

            return improvement_abillity_attack = two_hand_sword_abillityAttack;
        }


        return improvement_abillity_attack = 0;

    }

    #endregion

}