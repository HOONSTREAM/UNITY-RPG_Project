using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Storage_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject StoragePanel;
    [SerializeField]
    private GameObject Deposit_amount_panel; //�ñ�ų�, ã���� ��� �Է�
    [SerializeField]
    private GameObject WithDraw_amount_panel;
    [SerializeField]
    private GameObject Storage_Canvas;

    PlayerStorage storage; //�÷��̾� �κ��丮 ����
    PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)



    public Storage_Slots[] storage_slots;
    public Transform storage_slotHolder;
    public Slot[] Player_slots;
    public Transform player_slotHolder;

    bool activestorage = false;



    void Start()
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        storage = PlayerStorage.Instance;
        storage_slots = storage_slotHolder.GetComponentsInChildren<Storage_Slots>();
        Player_slots = player_slotHolder.GetComponentsInChildren<Slot>() ;

        StoragePanel.SetActive(activestorage);
        storage.onChangeItem += RedrawSlotUI;  // Invoke �Լ� ��� �̺�Ʈ �߻����� �Լ� ȣ��
        Managers.UI.SetCanvas(Storage_Canvas, true);
        //�κ��丮 �巡�� �����ϵ��� �ϴ� �̺�Ʈ
        UI_Base.BindEvent(StoragePanel, (PointerEventData data) => { StoragePanel.transform.position = data.position; }, Define.UIEvent.Drag);

        #region ���Թ��׹��� ���Ի����� ����
        storage.AddItem(ItemDataBase.instance.itemDB[0]);  
        storage.RemoveItem(0);  
        RedrawSlotUI(); 
        #endregion

    }


    public void Enter() 
    {
        activestorage = !activestorage;
        StoragePanel.SetActive(activestorage);
        Managers.Sound.Play("Inven_Open");

        
        for (int i = 0; i < Player_slots.Length; i++)
        {
                Player_slots[i].isStorageMode = activestorage;
        }
        

        return;
    }
    public void PanelExit()
    {
        if (StoragePanel.activeSelf)
        {
            activestorage = !activestorage;
            StoragePanel.SetActive(activestorage);
            Managers.Sound.Play("Inven_Open");

        }

        for (int i = 0; i < Player_slots.Length; i++)
        {
            Player_slots[i].isStorageMode = false;
        }

        return;
    }


    void RedrawSlotUI()
    {
        for (int i = 0; i < storage_slots.Length; i++)
        {
            storage_slots[i].slotnum = i;
        }

        for (int i = 0; i < storage_slots.Length; i++) //�� �о������
        {
            storage_slots[i].RemoveSlot();
        }

        for (int i = 0; i < storage.storage_item.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            storage_slots[i].item = storage.storage_item[i];
            storage_slots[i].UpdateSlotUI();

        }
    }


    public void Deposit()
    {
        Managers.Sound.Play("Coin");

        if(Deposit_amount_panel.activeSelf == false)
        {
            Deposit_amount_panel.SetActive(true);
        }
        
    }

    public void WithDraw()
    {
        Managers.Sound.Play("Coin");

        if (WithDraw_amount_panel.activeSelf == false)
        {
            WithDraw_amount_panel.SetActive(true);
        }

    }



}
