using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject _player;
    /*나중에 서버랑 연동되면 딕셔너리로 int ID로 관리*/
    //Dictionary<int,GameObject> _players = new Dictionary<int,GameObject>();

    #region NPC대화
    /*================================NPC대화관련 스크립트===============================*/
    public TalkManager talkmanager;
    public TextMeshProUGUI talkText;
    public GameObject scanobject;
    [SerializeField]
    GameObject TalkPanel;
    public bool IsTalkAction;
    public int Talkindex;
    public GameObject SelectedNPC; //NPC 게임오브젝트를 넘겨받아 TalkAction 함수에 scanObject에 대입하기위함 -> 버튼 OnClick 사용


    public void TalkAction()
    {
        Debug.Log($"토크인덱스 : {Talkindex}");
        scanobject = SelectedNPC;     
        Object_Data objdata = scanobject.GetComponent<Object_Data>();
        Talk(objdata.ID, objdata.IsNPC);
        TalkPanel.SetActive(IsTalkAction);

    }

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
