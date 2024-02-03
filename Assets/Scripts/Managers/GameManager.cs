using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private GameObject _player; //���߿� ������ �����Ǹ� ��ųʸ��� int ID�� ����


    #region NPC ID ����
    private const int HelKen_NPC_ID = 1003;
    #endregion

    #region NPC��ȭ
    /*================================NPC��ȭ���� ��ũ��Ʈ===============================*/
    public TalkManager talkmanager;
    public TextMeshProUGUI talkText; // ������ TalkPanel�� ��ϵǴ� �ؽ�Ʈ ���� ( ��ȭ���� ) 
    public GameObject scanobject;
    public GameObject TalkPanel;
    public bool IsTalkAction;
    public int Talkindex;
    public GameObject SelectedNPC; //NPC ���ӿ�����Ʈ�� �Ѱܹ޾� TalkAction �Լ��� scanObject�� �����ϱ����� -> ��ư OnClick ���


    public GameObject Helken_selection; //���� NPC ��ȭ ������ 
   

    public void TalkAction() //���� ��ȭ���� 
    {
        // TODO: �߰����� ��ȭ ���� ��, ��ȭâ ���� ��� 


        Debug.Log($"��ũ�ε��� : {Talkindex}");
        scanobject = SelectedNPC;     
        Object_Data objdata = scanobject.GetComponent<Object_Data>();
        Talk(objdata.ID, objdata.IsNPC);
        TalkPanel.SetActive(IsTalkAction);

        switch (objdata.ID) // NPC�� Ư����� �˻� 
        {
            case HelKen_NPC_ID:

                // (��ȭ�ϱ� ��ư ������ ���� �ʾҰ�, ù ��ȭ�� �ε��� ������ ������ ���)
                if(Talkindex == 2 && scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == false)
                {
                    Debug.Log("������");
                    Helken_selection.gameObject.SetActive(IsTalkAction);
                    Talkindex = 0; // �� �ε��� �ʱ�ȭ
                }
                else if (scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == true)
                {
                    Debug.Log("�߰���ȭ ����");
                    Helken_selection.gameObject.SetActive(false);

                    if(Talkindex == 2) // �߰� ��ȭ�� ���� �����ߴ��� �˻� 
                    {
                        scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open = false;
                        talkmanager.Reset_TalkData(); // Talk Data ����
                        Talkindex = 0;
                        
                    }                   

                }

                break;

            default:

                break;

        }


    }

    void Talk(int id, bool isNPC) //��ũ�ǳڿ� �ؽ�Ʈ�� ����ϴ� �Լ� 
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
