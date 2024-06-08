using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSkillQuickSlot;


// 참고사항 : 유니티에서 public으로 선언된 필드는 인스펙터에 노출됩니다.
// 만약 public으로 선언된 List가 인스펙터에서 기본값으로 설정되지 않았더라도,
// 유니티는 기본 생성자로 인해 해당 리스트를 자동으로 초기화합니다.
// 즉, public List<Quest> PlayerQuest;와 같이 선언된 리스트는 자동으로 빈 리스트로 초기화됩니다.

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
        if (PlayerQuest.Count > 0) //Null Crash 방지
        {
            PlayerQuest.RemoveAt(index);
            onChangequest.Invoke();

        }

        return;
    }



}
