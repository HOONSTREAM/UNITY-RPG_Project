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
    public TextMeshProUGUI Name_grade;
    [SerializeField]
    private Slider _slider;

    private void Start()
    {
       
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

        if(skill_name.text == "�Ѽհ�")
        {
            Managers.Sound.Play("spell", Define.Sound.Effect);

           GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Lightning aura");
            effect.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
            effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position;
            
            Destroy(effect, 5.0f);
        }

    }

}
