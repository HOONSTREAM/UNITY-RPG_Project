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
    /*���߿� ������ �����Ǹ� ��ųʸ��� int ID�� ����*/
    //Dictionary<int,GameObject> _players = new Dictionary<int,GameObject>();

    #region NPC��ȭ
    /*================================NPC��ȭ���� ��ũ��Ʈ===============================*/
    public TalkManager talkmanager;
    public TextMeshProUGUI talkText;
    public GameObject scanobject;
    [SerializeField]
    GameObject TalkPanel;
    public bool IsTalkAction;
    public int Talkindex;
    public GameObject SelectedNPC; //NPC ���ӿ�����Ʈ�� �Ѱܹ޾� TalkAction �Լ��� scanObject�� �����ϱ����� -> ��ư OnClick ���


    public void TalkAction()
    {
        Debug.Log($"��ũ�ε��� : {Talkindex}");
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
        Talkindex++; //�״��� ������ �̾Ƴ��� ���� 
    }
    /*==================================================================================*/
    #endregion

    HashSet<GameObject> _monsters = new HashSet<GameObject>(); //��ųʸ��� ���������� key�� ����

    public Action<int> OnSpawnEvent; //�׼ǿ� ��Ʈ���� ->���� ��ü ���ڸ� �ǹ�
   
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
                            OnSpawnEvent.Invoke(-1); //1������ �پ��ٴ� �ǹ�
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
