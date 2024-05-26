using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewInvenUI : MonoBehaviour
{
    
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject Inventory_canvas;
    private PlayerInventory inven; //�÷��̾� �κ��丮 ����
    private PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)
    public TextMeshProUGUI inven_amount_text; // �κ��丮 ĭ �ؽ�Ʈ
    public int inven_amount; //�κ��丮 ĭ ����


    public GameObject Equip_Drop_Selection; // ���ǰ Ŭ���� �ߴ� �ܼ� ���� (���â ����� ���� �ʱ�ȭ ����)
    public GameObject Consumable_use_Drop_Selection; // �Ҹ�ǰ Ŭ���� �ߴ� �ܼ� ���� (���â ����� ���� �ʱ�ȭ ����)


    public Slot[] slots;
    public Transform slotHolder;

    public bool activeInventory = false;

    private void Start()
    {

        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        inven = PlayerInventory.Instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onChangeItem += RedrawSlotUI;
        Managers.UI.SetCanvas(Inventory_canvas, true);
        //�κ��丮 �巡�� �����ϵ��� �ϴ� �̺�Ʈ
        UI_Base.BindEvent(inventoryPanel, (PointerEventData data) => { inventoryPanel.transform.position = data.position; }, Define.UIEvent.Drag);


        TraverseChildrenRecursively(inventoryPanel.transform);


        inventoryPanel.SetActive(activeInventory);
        
        
    }

   private void TraverseChildrenRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            
            TraverseChildrenRecursively(child);    // ��������� ���� �ڽ��� �ڽĵ鵵 ��ȸ
          
            if(child.name == "Content")
            {
                PlayerInventory.Instance._player_Inven_Content = child.gameObject;
            }
        }
    }
        private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.I))
        {

            Inventory_Button_Open();

            if (inventoryPanel.activeSelf == false)
            {
                Equip_Drop_Selection.gameObject.SetActive(false);
                Consumable_use_Drop_Selection.gameObject.SetActive(false);
                
            }
            
        }
       
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

        for (int i = 0; i < inven.player_items.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            slots[i].item = inven.player_items[i];
            slots[i].UpdateSlotUI();
            
        }

        inven_amount = inven.player_items.Count;
        inven_amount_text.text = $"�κ��丮 :  {inven_amount.ToString()}/30"; //�κ��丮 ���� ������Ʈ

        
    }
    public void Inventory_Button_Open()
    {
        activeInventory = !activeInventory;
        inventoryPanel.SetActive(activeInventory);
        PlayerInventory.Instance.onChangeItem.Invoke();
        Managers.UI.SetCanvas(Inventory_canvas, true);
        RedrawSlotUI();
        Managers.Sound.Play("Inven_Open");
    }
    public void Xbutton_Exit()
    {
        if (inventoryPanel != null && inventoryPanel.activeSelf)
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
            Managers.Sound.Play("Inven_Open");           
            Equip_Drop_Selection.gameObject.SetActive(false);
            Consumable_use_Drop_Selection.gameObject.SetActive(false);

        }

        return;
    }

    
}


