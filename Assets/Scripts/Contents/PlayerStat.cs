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
   
    PlayerEquipment equipment;

    #region 게임 플레이시 GUISTAT창 세팅
    private void OnUpdateStatUI()
    {
        GameObject atktxt = GameObject.Find("ATKnum").gameObject;
        GameObject deftxt = GameObject.Find("DEFnum").gameObject;
        GameObject goldtxt = GameObject.Find("Goldnum").gameObject;
        atktxt.GetComponent<TextMeshProUGUI>().text = Attack.ToString();
        deftxt.GetComponent<TextMeshProUGUI>().text = Defense.ToString();
        goldtxt.GetComponent<TextMeshProUGUI>().text = Gold.ToString();

    }
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
    }
    #endregion

    /*https://jeongkyun-it.tistory.com/23 */
    public int EXP //레벨업체크 까지 관리  
    { 
        
        get { return _exp; }
        
        set 
        { _exp = value;
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
            
            if(level != Level)
            {
                Level = level;
                SetStat(Level);              
               GameObject go = GameObject.Find("Level_Text").gameObject;
               go.GetComponent<TextMeshProUGUI>().text = $"Lv . {Level}";
               Managers.Sound.Play("univ0007");
                PrintUserText("레벨이 올랐습니다.");
               Invoke("TextClear", 2.0f);
                //23.09.26 수정 (exp 초기화)
                _exp -= (int)Managers.Data.StatDict[level].totalexp;
            }
            
        } 
    
    
    
    }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;   
        _gold = 0;
        _exp = 0;
        SetStat(_level);
        equipment = GetComponent<PlayerEquipment>();
    }
    private void Update()

    {
        //TODO 성능상 좋지않다. 이벤트가 발생하면 스텟 업데이트 할수 있도록 수정해야할 듯.
        OnUpdateStatUI();
    }


    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");

    }

    #region 스텟세팅 (장착장비검사까지)

    int WeaponAttackValue = 0; //장착무기 공격스텟 저장변수
    int ChestDEFvalue = 0; //장착갑옷 방어스텟 저장변수
    public void SetStat(int level)
    {
        
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //키가 레벨 
        Data.Stat stat = dict[level];

        _hp = stat.maxHP;
        _maxHp = stat.maxHP;
        _defense = stat.defense + ChestDEFvalue;
        _movespeed = stat.movespeed;
        _attack = stat.attack + WeaponAttackValue; 

    }

    public void SetEquipmentValue(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //키가 레벨 
        Data.Stat stat = dict[level];

        #region 무기장착검사   
        if (equipment.player_equip.TryGetValue(EquipType.Weapon , out Item _attackitem)) //장착무기 검사
        { 
            WeaponAttackValue = equipment.player_equip[EquipType.Weapon].num_1;
            _attack = stat.attack + WeaponAttackValue;
        }
        else
        {
            WeaponAttackValue = 0;
            _attack = stat.attack + WeaponAttackValue;
        }
        #endregion

        #region 방어구장착검사
        if (equipment.player_equip.TryGetValue(EquipType.Chest, out Item _defitem)) //장착방어구 검사
        {
            ChestDEFvalue = equipment.player_equip[EquipType.Chest].num_1;
            _defense = stat.defense + ChestDEFvalue;
        }

        else
        {
            ChestDEFvalue = 0;
            _defense = stat.defense + ChestDEFvalue;
        }
        #endregion

    }


    #endregion

}
