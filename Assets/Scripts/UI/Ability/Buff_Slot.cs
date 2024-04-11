using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buff_Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;

    public Skill skill;
    public Image skillicon;
    public TextMeshProUGUI skill_time_text;
    public Ability_Slot[] Ability_slots;
    public Transform Abilityslot_holder;

    void Awake()
    {

        skillicon.gameObject.SetActive(false); //초기화 (아이콘 표시 안함)
        skill_time_text.text = "";
        Ability_slots = Abilityslot_holder.GetComponentsInChildren<Ability_Slot>();
    }


    public void UpdateSlotUI()
    {
        skillicon.sprite = skill.skill_image;
        skillicon.gameObject.SetActive(true);

    }

    public void RemoveSlot()
    {
        skill = null;
        skillicon.gameObject.SetActive(false);
        skill_time_text.text = "";
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
