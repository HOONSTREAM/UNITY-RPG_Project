using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSkillQuickSlot;


// ������� : ����Ƽ���� public���� ����� �ʵ�� �ν����Ϳ� ����˴ϴ�.
// ���� public���� ����� List�� �ν����Ϳ��� �⺻������ �������� �ʾҴ���,
// ����Ƽ�� �⺻ �����ڷ� ���� �ش� ����Ʈ�� �ڵ����� �ʱ�ȭ�մϴ�.
// ��, public List<Quest> PlayerQuest;�� ���� ����� ����Ʈ�� �ڵ����� �� ����Ʈ�� �ʱ�ȭ�˴ϴ�.

public class Player_Quest : MonoBehaviour
{
    public static Player_Quest Instance;

    
    public List<Quest> PlayerQuest;


    public delegate void OnChangeQuest();
    public OnChangeQuest onChangequest;


    private void Awake()
    {
        Instance = this;
        PlayerQuest = new List<Quest>();
    }

    public bool AddQuest(Quest _quest) 
    {
        PlayerQuest.Add(_quest);

        if (onChangequest != null)
        {
            onChangequest.Invoke();

        }

        return true;
    }

    public void RemoveQuest(int index)
    {
        if (PlayerQuest.Count > 0) //Null Crash ����
        {
            PlayerQuest.RemoveAt(index);
            onChangequest.Invoke();

        }

        return;
    }



}
