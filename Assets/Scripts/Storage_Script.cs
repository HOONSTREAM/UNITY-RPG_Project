using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Storage_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject StoragePanel;

    PlayerStorage storage; //�÷��̾� �κ��丮 ����
    PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)

    public Slot[] slots;
    public Transform slotHolder;

    bool activestorage = false;



    void Start()
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        storage = PlayerStorage.Instance; 
        slots = slotHolder.GetComponentsInChildren<Slot>();

        StoragePanel.SetActive(activestorage);
        storage.onChangeItem += RedrawSlotUI;  

        //�κ��丮 �巡�� �����ϵ��� �ϴ� �̺�Ʈ
        UI_Base.BindEvent(StoragePanel, (PointerEventData data) => { StoragePanel.transform.position = data.position; }, Define.UIEvent.Drag);

        #region ���Թ��׹��� ���Ի����� ����
        storage.AddItem(ItemDataBase.instance.itemDB[0]);  // PlayerStorage ��ũ��Ʈ ��������
        storage.RemoveItem(0);  // PlayerStorage ��ũ��Ʈ ��������
        RedrawSlotUI(); 
        #endregion

    }


    void Update()
    {
        //TEST

        if (Input.GetKeyDown(KeyCode.S))
        {
            activestorage = !activestorage;
            StoragePanel.SetActive(activestorage);
            Managers.Sound.Play("Inven_Open");
        }
    }

   
    public void PanelExit()
    {
        if (StoragePanel.activeSelf)
        {
            StoragePanel.SetActive(false);
            Managers.Sound.Play("Inven_Open");
        }

        return;
    }


    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
        }

        for (int i = 0; i < slots.Length; i++) //�� �о������
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i < storage.storage_item.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            slots[i].item = storage.storage_item[i];
            slots[i].UpdateSlotUI();

        }
    }



}
