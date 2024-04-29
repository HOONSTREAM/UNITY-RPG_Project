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
    

    #region NPC ID ���� 
    private const int KNIGHT_HELKEN_NPC_ID = 1003; 
    private const int INN_OWNER_HECSEN_NPC_ID = 1004;
    private const int RUDENCIAN_HOUSE_CHIEF_NPC_ID = 1006;

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

    public TextMeshProUGUI NPC_name;

    public GameObject Talk_Panel_next_button;
    public GameObject Additional_Talk_button;

    public GameObject selection; //NPC ��ȭ ������ 
   
    

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

        switch (objdata.ID) // NPC�� Ư����� �˻� 
        {
            case KNIGHT_HELKEN_NPC_ID:

                // (��ȭ�ϱ� ��ư ������ ���� �ʾҰ�, ù ��ȭ�� �ε��� ������ ������ ���)
                if(Talkindex == 2 && scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == false)
                {
                   
                    Talk_Panel_next_button.gameObject.SetActive(false);
                    selection.gameObject.SetActive(IsTalkAction);
                    Additional_Talk_button.gameObject.SetActive(true);

                    Talkindex = 0; // �� �ε��� �ʱ�ȭ
                }
                else if (scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open == true)
                {

                    selection.gameObject.SetActive(false);

                    if(Talkindex == 4) // �߰� ��ȭ�� ���� �����ߴ��� �˻� 
                    {
                        Talk_Panel_next_button.gameObject.SetActive(false);
                        selection.gameObject.SetActive(true);
                        Additional_Talk_button.gameObject.SetActive(false);
                        scanobject.gameObject.GetComponent<Helken_NPC_Script>().is_Additional_Talk_open = false;
                        talkmanager.Reset_TalkData(); // Talk Data ����
                        Talkindex = 0;
                        
                    }                   

                }

                break;

            case INN_OWNER_HECSEN_NPC_ID:
                // TODO 
                break;

            case RUDENCIAN_HOUSE_CHIEF_NPC_ID:

                // (��ȭ�ϱ� ��ư ������ ���� �ʾҰ�, ù ��ȭ�� �ε��� ������ ������ ���)
                if (Talkindex == 1 && scanobject.gameObject.GetComponent<House_Chief_NPC_Script>().is_Additional_Talk_open == false)
                {

                    Talk_Panel_next_button.gameObject.SetActive(false);
                    selection.gameObject.SetActive(IsTalkAction);
                    Additional_Talk_button.gameObject.SetActive(true);

                    Talkindex = 0; // �� �ε��� �ʱ�ȭ
                }
                else if (scanobject.gameObject.GetComponent<House_Chief_NPC_Script>().is_Additional_Talk_open == true)
                {

                    selection.gameObject.SetActive(false);

                    if (Talkindex == 4) // �߰� ��ȭ�� ���� �����ߴ��� �˻� 
                    {
                        Debug.Log($"{Talkindex}�Դϴ�. �׸��� �߰���ȭ ���� if�������Ѱ� true");
                        Talk_Panel_next_button.gameObject.SetActive(false);
                        selection.gameObject.SetActive(true);
                        Additional_Talk_button.gameObject.SetActive(false);
                        scanobject.gameObject.GetComponent<House_Chief_NPC_Script>().is_Additional_Talk_open = false;
                        talkmanager.Reset_TalkData(); // Talk Data ����
                        Talkindex = 0;

                    }

                }


                break;


            default:

                break;

        }


    }

    /// <summary>
    /// Talk_Panel�� Text�� ������ �Ҵ��ϴ� �޼��� �Դϴ�.
    /// ���̸�ŭ �Ҵ��Ű��, TalkIndex�� ������ŵ�ϴ�.
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
