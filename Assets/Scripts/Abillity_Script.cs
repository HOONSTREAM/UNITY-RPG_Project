using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static SerializableDictionary;

public class Abillity_Script : MonoBehaviour
{
    public GameObject Abillity_Panel;
    public GameObject Abillity_canvas;
    public GameObject Abillity_Explaination_Panel;
   
    private PlayerAbillity abillity; 
    private PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)

    public delegate void OnUpdateAbillity();
    public OnUpdateAbillity onupdate_abillity;

    public Abillity_Slot[] abillity_Slots;
    public Transform abillity_slotHolder;
    

    private void Start() //Player Controller에 붙이면, 프리펩이므로 Start 전의 프리펩이 붙어서 slot이 업데이트가 안됨. 
    {
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        abillity = PlayerAbillity.Instance;
        abillity_Slots = abillity_slotHolder.GetComponentsInChildren<Abillity_Slot>();       
        abillity.onChangeSkill += RedrawSlotUI;
        UI_Base.BindEvent(Abillity_Panel, (PointerEventData data) => {Abillity_Panel.transform.position = data.position; }, Define.UIEvent.Drag);
        Managers.UI.SetCanvas(Abillity_canvas, true);

        onupdate_abillity += Accumulate_abillity_Func; //delegate


        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[0]);
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[1]);

        
    }
    public void Exit()
    {
        if (Abillity_Panel.activeSelf)
        {
            Abillity_Panel.SetActive(false);
            GameObject go = GameObject.Find("GUI_User_Interface").gameObject;
            go.GetComponent<Abillity_Button_Script>().active_abillity_panel = false;
            Managers.Sound.Play("Inven_Open");
        }

        return;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < abillity_Slots.Length; i++)
        {
            abillity_Slots[i].slotnum = i;
        }

        for (int i = 0; i < abillity_Slots.Length; i++) //싹 밀어버리고
        {
           abillity_Slots[i].RemoveSlot();
        }

        for (int i = 0; i < abillity.PlayerSkill.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
        {
            abillity_Slots[i].skill = abillity.PlayerSkill[i];
            abillity_Slots[i].UpdateSlotUI();

        }
    }

    public void Accumulate_abillity_Func()

        //TODO : 어빌 1.00 당 해당 무기 고정데미지 향상 +5 , 게임저장 방법(유니티교과서) , 그레이드
    {
       
        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value) && value.weapontype == WeaponType.One_Hand) // 무기를 장착중이고, 한손검인경우
        {
                                 
            for (int i = 0; i < abillity_Slots.Length; i++)
            {
                if (abillity_Slots[i].skill_name.text == "한손검")
                {                                                 

                    if (double.Parse(abillity_Slots[i].Level.text) == 100.00)
                    {
                        Debug.Log("어빌이 100에 달성하였습니다.");
                        return;
                    }

                    abillity_Slots[i].Level.text = $"{abillity_Slots[i].skill.abillity += 10.00}"; //TEST TODO

                }               
            }

       
           
        }

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.Two_Hand) // 무기를 장착중이고, 두손검인경우
        {
            Debug.Log("두손검 어빌을 올립니다.");

            for (int i = 0; i < abillity_Slots.Length; i++)
            {
                if (abillity_Slots[i].skill_name.text == "양손검")
                {
                    Debug.Log("두손검 어빌 발견");
                    Debug.Log($"{abillity_Slots[i].slotnum}번 스킬슬롯 입니다.");
                    Debug.Log("어빌을 올립니다.");
                    abillity_Slots[i].Level.text = $"LEVEL                  {abillity_Slots[i].skill.abillity += 0.01}";
                }

            }

        }

        else
        {
            Debug.Log("맨손 입니다.");

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
}
