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

    #region ���� �÷��̽� GUISTATâ ����
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
    public int EXP //������üũ ���� ����  
    { 
        
        get { return _exp; }
        
        set 
        { _exp = value;
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
            
            if(level != Level)
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
        //TODO ���ɻ� �����ʴ�. �̺�Ʈ�� �߻��ϸ� ���� ������Ʈ �Ҽ� �ֵ��� �����ؾ��� ��.
        OnUpdateStatUI();
    }


    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");

    }

    #region ���ݼ��� (�������˻����)

    int WeaponAttackValue = 0; //�������� ���ݽ��� ���庯��
    int ChestDEFvalue = 0; //�������� ���� ���庯��
    public void SetStat(int level)
    {
        
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[level];

        _hp = stat.maxHP;
        _maxHp = stat.maxHP;
        _defense = stat.defense + ChestDEFvalue;
        _movespeed = stat.movespeed;
        _attack = stat.attack + WeaponAttackValue; 

    }

    public void SetEquipmentValue(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict; //Ű�� ���� 
        Data.Stat stat = dict[level];

        #region ���������˻�   
        if (equipment.player_equip.TryGetValue(EquipType.Weapon , out Item _attackitem)) //�������� �˻�
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

        #region �������˻�
        if (equipment.player_equip.TryGetValue(EquipType.Chest, out Item _defitem)) //������ �˻�
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
