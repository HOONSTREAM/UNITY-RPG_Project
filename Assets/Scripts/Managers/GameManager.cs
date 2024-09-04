using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private GameObject _player; 
    

    #region NPC ID 참조 
    private const int KNIGHT_HELKEN_NPC_ID = 1003; 
    private const int INN_OWNER_HECSEN_NPC_ID = 1004;
    private const int RUDENCIAN_HOUSE_CHIEF_NPC_ID = 1006;
    private const int RUDENCIAN_ROOKISS_NPC_ID = 1007;

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

    public GameObject selection; //NPC 대화 선택지 
   
    

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

        NPC_Additional_Talk_Button_Open_Method(objdata);

    }

    private void NPC_Additional_Talk_Button_Open_Method(Object_Data objdata)
    {
        switch (objdata.ID) // NPC별 특수기능 검사 
        {
            case KNIGHT_HELKEN_NPC_ID:

                // (대화하기 버튼 진행을 하지 않았고, 첫 대화의 인덱스 최종에 도달한 경우)
                if (Talkindex == 2 && scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == false)
                {

                    Talk_Panel_next_button.gameObject.SetActive(false);
                    selection.gameObject.SetActive(IsTalkAction);
                    Additional_Talk_button.gameObject.SetActive(true);

                    Talkindex = 0; // 톡 인덱스 초기화
                }
                else if (scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == true)
                {

                    selection.gameObject.SetActive(false);

                    if (Talkindex == 4) // 추가 대화가 끝에 도달했는지 검사 
                    {
                        Talk_Panel_next_button.gameObject.SetActive(false);
                        selection.gameObject.SetActive(true);
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

            case RUDENCIAN_HOUSE_CHIEF_NPC_ID:

                // (대화하기 버튼 진행을 하지 않았고, 첫 대화의 인덱스 최종에 도달한 경우)
                if (Talkindex == 1 && scanobject.gameObject.GetComponent<House_Chief_NPC_Script>().is_Additional_Talk_open == false)
                {

                    Talk_Panel_next_button.gameObject.SetActive(false);
                    selection.gameObject.SetActive(IsTalkAction);
                    Additional_Talk_button.gameObject.SetActive(true);

                    Talkindex = 0; // 톡 인덱스 초기화
                }
                else if (scanobject.gameObject.GetComponent<House_Chief_NPC_Script>().is_Additional_Talk_open == true)
                {

                    if(GameObject.Find("Bgm").gameObject.GetComponent<AudioSource>().clip.name != "월터메인퀘스트사운드")
                    {
                        Managers.Sound.Clear();
                        Managers.Sound.Play("월터메인퀘스트사운드", Define.Sound.Bgm);
                    }
                                    
                    selection.gameObject.SetActive(false);

                    if (Talkindex == 8) // 추가 대화가 끝에 도달했는지 검사 
                    {

                        Talk_Panel_next_button.gameObject.SetActive(false);
                        selection.gameObject.SetActive(true);
                        Additional_Talk_button.gameObject.SetActive(false);
                        scanobject.gameObject.GetComponent<House_Chief_NPC_Script>().is_Additional_Talk_open = false;
                        talkmanager.Reset_TalkData(); // Talk Data 리셋
                        Talkindex = 0;

                    }

                }


                break;

            case RUDENCIAN_ROOKISS_NPC_ID:

                if (Talkindex == 1 && scanobject.gameObject.GetComponent<RooKiss_NPC_Script>().is_Additional_Talk_open == false)
                {

                    Talk_Panel_next_button.gameObject.SetActive(false);
                    selection.gameObject.SetActive(IsTalkAction);
                    Additional_Talk_button.gameObject.SetActive(true);

                    Talkindex = 0; // 톡 인덱스 초기화
                }

                else if (scanobject.gameObject.GetComponent<RooKiss_NPC_Script>().is_Additional_Talk_open == true)
                {
                    if (QuestDatabase.instance.QuestDB[5].is_complete == true && !QuestDatabase.instance.QuestDB[6].is_complete)
                    {
                        selection.gameObject.SetActive(false);

                        if (Talkindex == 4) // 장비착용 관련 퀘스트 수령 추가 대화가 끝에 도달했는지 검사 
                        {

                            Talk_Panel_next_button.gameObject.SetActive(false);
                            selection.gameObject.SetActive(true);
                            Additional_Talk_button.gameObject.SetActive(false);
                            scanobject.gameObject.GetComponent<RooKiss_NPC_Script>().is_Additional_Talk_open = false;
                            talkmanager.Reset_TalkData(); // Talk Data 리셋
                            Talkindex = 0;

                        }

                        break;
                    }

                    // 어빌달성 퀘스트를 완료하였고, 루키스와의 3번째 대화가 아직 진행되지 않은상태인지 점검
                    if (QuestDatabase.instance.QuestDB[8].is_complete == true && !QuestDatabase.instance.QuestDB[9].is_complete)
                    {
                        Debug.Log($"퀘스트 1번 완료상태 : {QuestDatabase.instance.QuestDB[0].is_complete}");
                        Debug.Log($"퀘스트 2번 완료상태 : {QuestDatabase.instance.QuestDB[1].is_complete}");
                        Debug.Log($"퀘스트 3번 완료상태 : {QuestDatabase.instance.QuestDB[2].is_complete}");
                        Debug.Log($"퀘스트 4번 완료상태 : {QuestDatabase.instance.QuestDB[3].is_complete}");
                        Debug.Log($"퀘스트 5번 완료상태 : {QuestDatabase.instance.QuestDB[4].is_complete}");
                        Debug.Log($"퀘스트 6번 완료상태 : {QuestDatabase.instance.QuestDB[5].is_complete}");
                        Debug.Log($"퀘스트 7번 완료상태 : {QuestDatabase.instance.QuestDB[6].is_complete}");
                        Debug.Log($"퀘스트 8번 완료상태 : {QuestDatabase.instance.QuestDB[7].is_complete}");
                        Debug.Log($"퀘스트 9번 완료상태 : {QuestDatabase.instance.QuestDB[8].is_complete}");
                        Debug.Log($"퀘스트 10번 완료상태 : {QuestDatabase.instance.QuestDB[9].is_complete}");


                        selection.gameObject.SetActive(false);

                        if (Talkindex == 6) 
                        {

                            Talk_Panel_next_button.gameObject.SetActive(false);
                            selection.gameObject.SetActive(true);
                            Additional_Talk_button.gameObject.SetActive(false);
                            scanobject.gameObject.GetComponent<RooKiss_NPC_Script>().is_Additional_Talk_open = false;
                            talkmanager.Reset_TalkData(); // Talk Data 리셋
                            Talkindex = 0;

                        }

                        break;
                    }

                    selection.gameObject.SetActive(false);

                    if (Talkindex == 4) 
                    {

                        Talk_Panel_next_button.gameObject.SetActive(false);
                        selection.gameObject.SetActive(true);
                        Additional_Talk_button.gameObject.SetActive(false);
                        scanobject.gameObject.GetComponent<RooKiss_NPC_Script>().is_Additional_Talk_open = false;
                        talkmanager.Reset_TalkData(); // Talk Data 리셋
                        Talkindex = 0;

                    }

                }


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

    public void Tracking_NPC_Press_Additional_Talk_Button()
    {
        switch (scanobject.name)
        {
            case "기사 헬켄":
                scanobject.gameObject.GetComponent<Helken_NPC_Script>().Additional_Talk();
                break;
            case "수련관 루키스":
                scanobject.gameObject.GetComponent<RooKiss_NPC_Script>().Additional_Talk();
                break;
            case "촌장 월터":
                scanobject.gameObject.GetComponent<House_Chief_NPC_Script>().Additional_Talk();
                break;
            case "여관주인 헥센":
                break;

        }
    }

    public void Tracking_NPC_Press_End_Button()
    {
        switch (scanobject.name)
        {
            case "기사 헬켄":
                scanobject.gameObject.GetComponent<Helken_NPC_Script>().Helken_Taking_end_button();
                break;
            case "수련관 루키스":
                scanobject.gameObject.GetComponent<RooKiss_NPC_Script>().Taking_end_button();
                break;
            case "촌장 월터":
                scanobject.gameObject.GetComponent<House_Chief_NPC_Script>().Taking_end_button();
                break;
            case "여관주인 헥센":
                
                break;


        }
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

    /// <summary>
    /// 씬을 처음 실행하는 곳에서 플레이어를 스폰시키기 위한 메서드 입니다.
    /// </summary>
    public void Set_Player_and_Save_Data_PreFabs()
    {
        
       GameObject savedata = Managers.Resources.Instantiate("Save_Data"); //Save_Data 오브젝트 새로 생성

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;


        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);

        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);

        return;

    }



}
