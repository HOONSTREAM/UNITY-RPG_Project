using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff_Slot : MonoBehaviour
{
    public static PlayerBuff_Slot Instance;

    PlayerStat stat;
    public List<Skill> buff_slot;
    public Buff_Slot slot;

    public delegate void OnChangeBuff();
    public OnChangeBuff onChangeBuff;


    private void Awake()
    {
        Instance = this;
        buff_slot = new List<Skill>();
        stat = GetComponent<PlayerStat>();
        slot = GetComponent<Buff_Slot>();

    }


    public bool Buff_slot_AddBuffSkill(Skill _skill, int index = 0)
    {

        if (buff_slot.Count == 4)
        {
            // ��ų�� �� ���� �� ���� ���
            return false;
        }
        buff_slot.Add(_skill); //clone �Լ� �����ʰ� ���� �������� �����ؾ��Ѵ�. (Clone�Լ� �����������)
        onChangeBuff.Invoke();

        return true;

    }

    public void Buff_Slot_RemoveBuffSkill(int index)
    {

        if (buff_slot[index] == null)
        {
            return;
        }

        buff_slot.RemoveAt(index);
        onChangeBuff.Invoke();


        return;

    }






 }

