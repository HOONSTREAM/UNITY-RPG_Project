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
       // skill_icon.gameObject.SetActive(false); //�ʱ�ȭ (������ ǥ�� ����)
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
        skill_icon.gameObject.SetActive(false); //�ʱ�ȭ (������ ǥ�� ����)
        skill_name.text = "";

        return;
    }


    public void OnPointerUp(PointerEventData eventData)
    {

        //TODO : ��ư ������ �߻��ϴ� �� ����
        Debug.Log("��ư�� �������ϴ�.");

    }

}
