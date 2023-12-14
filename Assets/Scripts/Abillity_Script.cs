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
    private PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)

    public delegate void OnUpdateAbillity();
    public OnUpdateAbillity onupdate_abillity;

    public Abillity_Slot[] abillity_Slots;
    public Transform abillity_slotHolder;
    

    private void Start() //Player Controller�� ���̸�, �������̹Ƿ� Start ���� �������� �پ slot�� ������Ʈ�� �ȵ�. 
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
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

        for (int i = 0; i < abillity_Slots.Length; i++) //�� �о������
        {
           abillity_Slots[i].RemoveSlot();
        }

        for (int i = 0; i < abillity.PlayerSkill.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            abillity_Slots[i].skill = abillity.PlayerSkill[i];
            abillity_Slots[i].UpdateSlotUI();

        }
    }

    public void Accumulate_abillity_Func()

        //TODO : ��� 1.00 �� �ش� ���� ���������� ��� +5 , �������� ���(����Ƽ������) , �׷��̵�
    {
       
        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value) && value.weapontype == WeaponType.One_Hand) // ���⸦ �������̰�, �Ѽհ��ΰ��
        {
                                 
            for (int i = 0; i < abillity_Slots.Length; i++)
            {
                if (abillity_Slots[i].skill_name.text == "�Ѽհ�")
                {                                                 

                    if (double.Parse(abillity_Slots[i].Level.text) == 100.00)
                    {
                        Debug.Log("����� 100�� �޼��Ͽ����ϴ�.");
                        return;
                    }

                    abillity_Slots[i].Level.text = $"{abillity_Slots[i].skill.abillity += 10.00}"; //TEST TODO

                }               
            }

       
           
        }

        else if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.Two_Hand) // ���⸦ �������̰�, �μհ��ΰ��
        {
            Debug.Log("�μհ� ����� �ø��ϴ�.");

            for (int i = 0; i < abillity_Slots.Length; i++)
            {
                if (abillity_Slots[i].skill_name.text == "��հ�")
                {
                    Debug.Log("�μհ� ��� �߰�");
                    Debug.Log($"{abillity_Slots[i].slotnum}�� ��ų���� �Դϴ�.");
                    Debug.Log("����� �ø��ϴ�.");
                    abillity_Slots[i].Level.text = $"LEVEL                  {abillity_Slots[i].skill.abillity += 0.01}";
                }

            }

        }

        else
        {
            Debug.Log("�Ǽ� �Դϴ�.");

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
