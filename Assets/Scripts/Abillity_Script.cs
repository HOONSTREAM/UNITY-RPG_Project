using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static SerializableDictionary;

public class Abillity_Script : MonoBehaviour
{
    public GameObject Abillity_Panel;
    public GameObject Abillity_canvas;


    private PlayerAbillity abillity; //플레이어 인벤토리 참조
    private PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)



    public Abillity_Slot[] abillity_Slots;
    public Transform abillity_slotHolder;
   

    private void Start()
    {
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        abillity = PlayerAbillity.Instance;
        abillity_Slots = abillity_slotHolder.GetComponentsInChildren<Abillity_Slot>();

        abillity.onChangeSkill += RedrawSlotUI;

        UI_Base.BindEvent(Abillity_Panel, (PointerEventData data) => { Abillity_Panel.transform.position = data.position; }, Define.UIEvent.Drag);
        Managers.UI.SetCanvas(Abillity_canvas, true);

        #region 슬롯버그방지 슬롯생성후 삭제
        abillity.AddSkill(SkillDataBase.instance.SkillDB[0]);
        abillity.RemoveSkill(0);      
        RedrawSlotUI();
        #endregion

        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[0]);
        PlayerAbillity.Instance.AddSkill(SkillDataBase.instance.SkillDB[1]);
    }
    public void Exit()
    {
        if (Abillity_Panel.activeSelf)
        {
            Abillity_Panel.SetActive(false);
            GameObject go = GameObject.Find("GUI").gameObject;
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
    {
        if(PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value) && value.weapontype == WeaponType.One_Hand) // 무기를 장착중이고, 한손검인경우
        {
            Debug.Log("한손검 어빌을 올립니다.");

            return;
        }

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.Two_Hand) // 무기를 장착중이고, 두손검인경우
        {
            Debug.Log("두손검 어빌을 올립니다.");

            return;

        }

        else
        {
            Debug.Log("맨손 입니다.");

            return;
        }
    }
}
