using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private GameObject _player; //나중에 서버랑 연동되면 딕셔너리로 int ID로 관리
    

    #region NPC ID 참조
    private const int KNIGHT_HELKEN_NPC_ID = 1003; // 기사 헬켄
    private const int INN_OWNER_HECSEN_NPC_ID = 1004;

    #endregion

    #region NPC대화
    /*================================NPC대화관련 스크립트===============================*/
    public TalkManager talkmanager;
    public TextMeshProUGUI talkText; // 실제로 TalkPanel에 등록되는 텍스트 내용 ( 대화내용 ) 
    public GameObject scanobject;
    public GameObject TalkPanel;
    public bool IsTalkAction;
    public int Talkindex;
    public GameObject SelectedNPC; //NPC 게임오브젝트를 넘겨받아 TalkAction 함수에 scanObject에 대입하기위함 -> 버튼 OnClick 사용

    public TextMeshProUGUI NPC_name;

    public GameObject Talk_Panel_next_button;
    public GameObject Additional_Talk_button;

    public GameObject Helken_selection; //헬켄 NPC 대화 선택지 
   


    public void TalkAction() 
    {
        
        scanobject = SelectedNPC;

        if (scanobject == null)
            return;

        Debug.Log(scanobject.name);
        Object_Data objdata = scanobject.GetComponent<Object_Data>();
        Talk(objdata.ID, objdata.IsNPC);


        TalkPanel.gameObject.SetActive(IsTalkAction);

        Talk_Panel_next_button.gameObject.SetActive(true);

        NPC_name.text = scanobject.name;

        switch (objdata.ID) // NPC별 특수기능 검사 
        {
            case KNIGHT_HELKEN_NPC_ID:

                // (대화하기 버튼 진행을 하지 않았고, 첫 대화의 인덱스 최종에 도달한 경우)
                if(Talkindex == 2 && scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == false)
                {
                   
                    Talk_Panel_next_button.gameObject.SetActive(false);
                    Helken_selection.gameObject.SetActive(IsTalkAction);
                    Additional_Talk_button.gameObject.SetActive(true);

                    Talkindex = 0; // 톡 인덱스 초기화
                }
                else if (scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == true)
                {
                    
                    Helken_selection.gameObject.SetActive(false);

                    if(Talkindex == 2) // 추가 대화가 끝에 도달했는지 검사 
                    {
                        Talk_Panel_next_button.gameObject.SetActive(false);
                        Helken_selection.gameObject.SetActive(true);
                        Additional_Talk_button.gameObject.SetActive(false);
                        scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open = false;
                        talkmanager.Reset_TalkData(); // Talk Data 리셋
                        Talkindex = 0;
                        
                    }                   

                }

                break;

            case INN_OWNER_HECSEN_NPC_ID:
                // TODO 
                break;

            default:

                break;

        }


    }

    /// <summary>
    /// Talk_Panel에 Text를 실제로 할당하는 메서드 입니다.
    /// 길이만큼 할당시키고, TalkIndex를 증가시킵니다.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isNPC"></param>
    void Talk(int id, bool isNPC)
    {
       
        string talkData = talkmanager.GetTalk(id, Talkindex);

        if (talkData == null)
        {
            IsTalkAction = false;           
            Talkindex = 0;
            
            return;
        }
        
        if (isNPC)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        IsTalkAction = true;
        Talkindex++;
        
    }
    /*==================================================================================*/
    #endregion

    HashSet<GameObject> _monsters = new HashSet<GameObject>(); //딕셔너리랑 유사하지만 key가 없음

    public Action<int> OnSpawnEvent; //액션에 인트전달 ->몬스터 개체 숫자를 의미
   
    public GameObject GetPlayer() { return _player; }

    public GameObject Spawn(Define.WorldObject type , string path, Transform parent = null)
    {

       GameObject go =  Managers.Resources.Instantiate(path,parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;

        }

        return go;

    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();

        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;

    }

    public void DeSpawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                        {
                            OnSpawnEvent.Invoke(-1); //1마리가 줄었다는 의미
                        }
                      
                    }
                      
                }
                
                break;
            case Define.WorldObject.Player:
                {

                    if (_player == go)
                        _player = null;
                }
                break;

        }

        Managers.Resources.Destroy(go);
    }

   

}
