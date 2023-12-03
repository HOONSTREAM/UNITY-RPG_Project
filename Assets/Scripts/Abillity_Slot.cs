using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Abillity_Slot : MonoBehaviour , IPointerUpHandler
{
    public int slotnum;


    public Skill skill;
    public Image skill_icon;
    public TextMeshProUGUI skill_name;
    public TextMeshProUGUI grade_amount;
    public TextMeshProUGUI Level;
    [SerializeField]
    private Slider _slider;

    private void Start()
    {
       // skill_icon.gameObject.SetActive(false); //초기화 (아이콘 표시 안함)
        _slider = GameObject.Find("Abillity_Slider").gameObject.GetComponent<Slider>();
    }

    public void UpdateSlotUI()
    {
        skill_icon.sprite = skill.skill_image;
        skill_name.text = skill.skill_name;
        skill_icon.gameObject.SetActive(true);

        return;
    }
    public void RemoveSlot()
    {
        skill = null;
        skill_icon.gameObject.SetActive(false); //초기화 (아이콘 표시 안함)
        skill_name.text = "";

        return;
    }


    public void OnPointerUp(PointerEventData eventData)
    {

        //TODO : 버튼 누르면 발생하는 것 구현
        Debug.Log("버튼을 눌렀습니다.");

    }

}
