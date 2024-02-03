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
    private const int HelKen_NPC_ID = 1003;
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


    public GameObject Helken_selection; //헬켄 NPC 대화 선택지 
   

    public void TalkAction() //실제 대화진행 
    {
        // TODO: 추가적인 대화 종료 후, 대화창 종료 방식 


        Debug.Log($"토크인덱스 : {Talkindex}");
        scanobject = SelectedNPC;     
        Object_Data objdata = scanobject.GetComponent<Object_Data>();
        Talk(objdata.ID, objdata.IsNPC);
        TalkPanel.SetActive(IsTalkAction);

        switch (objdata.ID) // NPC별 특수기능 검사 
        {
            case HelKen_NPC_ID:

                // (대화하기 버튼 진행을 하지 않았고, 첫 대화의 인덱스 최종에 도달한 경우)
                if(Talkindex == 2 && scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == false)
                {
                    Debug.Log("선택지");
                    Helken_selection.gameObject.SetActive(IsTalkAction);
                    Talkindex = 0; // 톡 인덱스 초기화
                }
                else if (scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == true)
                {
                    Debug.Log("추가대화 진행");
                    Helken_selection.gameObject.SetActive(false);

                    if(Talkindex == 2) // 추가 대화가 끝에 도달했는지 검사 
                    {
                        scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open = false;
                        talkmanager.Reset_TalkData(); // Talk Data 리셋
                        Talkindex = 0;
                        
                    }                   

                }

                break;

            default:

                break;

        }


    }

    void Talk(int id, bool isNPC) //토크판넬에 텍스트를 등록하는 함수 
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
        Talkindex++; //그다음 문장을 뽑아내기 위함
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
