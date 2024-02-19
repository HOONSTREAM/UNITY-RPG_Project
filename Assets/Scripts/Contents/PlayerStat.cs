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

    private int WeaponAttackValue = 0; //장착무기 공격스텟 저장변수
    private int ChestDEFvalue = 0; //장착갑옷 방어스텟 저장변수
    private int WeaponSTRValue = 0; //장착무기 STR 스텟 저장변수 (공격력)
    private int VITvalue = 0; // VIT 스텟 저장변수 (방어력,체력,체력회복속도)
    private int AGIvalue = 0; // AGI 스텟 저장변수 (공격속도, 회피율, 이동속도)
    private int INTValue = 0; //INT 마법공격력
    public int one_hand_sword_abillityAttack = 0; //어빌리티 별 향상공격력 저장변수 (한손검)
    public int two_hand_sword_abillityAttack = 0; //어빌리티 별 향상공격력 저장변수 (양손검)
    public int improvement_abillity_attack;
    public int buff_damage = 0; // 스킬 사용 시 버프데미지
    public int buff_defense = 0; // 스킬 사용 시 버프방어력

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

  
    #region 게임 플레이시 GUISTAT창, 장비창 STAT창  세팅
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

    } //string 성능이슈

  
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
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("레벨이 올랐습니다.");               
                #region Level Up EFFECT
                Managers.Sound.Play("change_scene", Define.Sound.Effect);

                GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Unlock_FX_7");


                effect.transform.parent = Managers.Game.GetPlayer().transform; // 부모설정
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

    #region 스텟세팅 (장착장비검사,어빌리티검사)
    

    public void SetStat(int level)  // INT , AGI 스텟세팅 필요 TODO
    {

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //키가 레벨 
        Data.Stat stat = dict[level];

        _hp = stat.maxHP;
        _maxHp = stat.maxHP;
        _defense = stat.defense + ChestDEFvalue + (_vit / 10) + buff_defense; //총 DEX의 1/10을 데미지에 기여함
        _movespeed = stat.movespeed;
        _attack = stat.attack + WeaponAttackValue + (_str / 10) + Onupdate_Abillity_attack() + buff_damage; //총 STR의 1/10을 데미지에 기여함+ 무기 어빌리티별 향상데미지

    }

    public void SetAttack_and_Defanse_value(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //키가 레벨 
        Data.Stat stat = dict[level];
        _defense = stat.defense + ChestDEFvalue + (VITvalue / 10) + buff_defense; //총 DEX의 1/10을 데미지에 기여함
        _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10) + Onupdate_Abillity_attack() + buff_damage; //총 STR의 1/10을 데미지에 기여함+ 무기 어빌리티별 향상데미지

        return;
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
                    VITvalue -= equipment.player_equip[EquipType.Weapon].num_3;
                    AGIvalue -= equipment.player_equip[EquipType.Weapon].num_4;
                    _str = stat.STR + WeaponSTRValue;
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                    _attack = stat.attack; // 무기 해제이므로 순수 레벨에 해당하는 어택수치로 변경함.

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
                    _attack = stat.attack + WeaponAttackValue + (WeaponSTRValue / 10) + Onupdate_Abillity_attack(); //총 STR의 1/10을 데미지에 기여함 + 무기 어빌리티별 향상데미지
                }
            }
        }

        #endregion

        #region 방어구장착검사

        if (item.equiptype == EquipType.outter_plate)
        {
            if (equipment.player_equip.TryGetValue(EquipType.outter_plate, out Item _chest_def_item)) //장착방어구 검사
            {
                if (_chest_def_item.Equip)
                {
                    ChestDEFvalue -= equipment.player_equip[EquipType.outter_plate].num_1;
                    INTValue -= equipment.player_equip[EquipType.outter_plate].num_2;
                    VITvalue -= equipment.player_equip[EquipType.outter_plate].num_3;
                    AGIvalue -= equipment.player_equip[EquipType.outter_plate].num_4;

                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //총 VIT의 1/10을 방어력에 기여함                                      
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                }
                else if (_chest_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.outter_plate].num_1;                    
                    VITvalue += equipment.player_equip[EquipType.outter_plate].num_3;
                    AGIvalue += equipment.player_equip[EquipType.outter_plate].num_4;

                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //총 DEX의 1/10을 데미지에 기여함                                      
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                }
            }
        }

        if (item.equiptype == EquipType.Chest)
        {
            if (equipment.player_equip.TryGetValue(EquipType.Chest, out Item _chest_def_item)) //장착방어구 검사
            {
                if (_chest_def_item.Equip)
                {
                    ChestDEFvalue -= equipment.player_equip[EquipType.Chest].num_1;                  
                    VITvalue -= equipment.player_equip[EquipType.Chest].num_3;
                    AGIvalue -= equipment.player_equip[EquipType.Chest].num_4;
                   
                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //총 DEX의 1/10을 데미지에 기여함                                       
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;
                }
                else if(_chest_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.Chest].num_1;               
                    VITvalue += equipment.player_equip[EquipType.Chest].num_3;
                    AGIvalue += equipment.player_equip[EquipType.Chest].num_4;

                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //총 DEX의 1/10을 데미지에 기여함                                       
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
                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //총 DEX의 1/10을 데미지에 기여함;                  
                    _vit = stat.VIT + VITvalue;
                    _agi = stat.AGI + AGIvalue;

                }

                else if (_head_def_item.Equip == false)
                {
                    ChestDEFvalue += equipment.player_equip[EquipType.Head].num_1;                        
                    VITvalue += equipment.player_equip[EquipType.Head].num_3;
                    AGIvalue += equipment.player_equip[EquipType.Head].num_4;
                    _defense = stat.defense + ChestDEFvalue + (VITvalue / 10); //총 DEX의 1/10을 데미지에 기여함;                    
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

            Abillity_Script abillity_script = FindObjectOfType<Abillity_Script>();
            for (int i = 0; i < abillity_script.abillity_Slots.Length; i++)
            {
                if (abillity_script.abillity_Slots[i].skill_name.text == "양손검")
                {
                    double abillity_attack_improvement = (double.Parse(abillity_script.abillity_Slots[i].Level.text) * 5); //TODO :여기서 Grade 수치 * 500 도 더해야함 

                    Debug.Log($"향상된 데미지, 무기종류 :{abillity_script.abillity_Slots[i].skill_name.text}, {abillity_attack_improvement}");
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