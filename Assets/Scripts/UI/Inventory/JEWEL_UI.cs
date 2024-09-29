using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class JEWEL_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _jewel_UI_Panel;
    [SerializeField]
    private GameObject _jewel_canvas;
    private PlayerJewelInven _jewel_inven; //�÷��̾� �κ��丮 ����

    public JEWEL_Slot[] slots;
    public Transform slotHolder;

    public bool active_jewel_Inventory = false;

    private void Start()
    {

        _jewel_inven = PlayerJewelInven.Instance;
        slots = slotHolder.GetComponentsInChildren<JEWEL_Slot>();
        _jewel_inven.onChangejewel += RedrawSlotUI;
        Managers.UI.SetCanvas(_jewel_canvas, true);
        //�κ��丮 �巡�� �����ϵ��� �ϴ� �̺�Ʈ
        UI_Base.BindEvent(_jewel_UI_Panel, (PointerEventData data) => { _jewel_UI_Panel.transform.position = data.position; }, Define.UIEvent.Drag);


        TraverseChildrenRecursively(_jewel_UI_Panel.transform);


        _jewel_UI_Panel.SetActive(active_jewel_Inventory);


    }

    private void TraverseChildrenRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {

            TraverseChildrenRecursively(child);    // ��������� ���� �ڽ��� �ڽĵ鵵 ��ȸ

            if (child.name == "Content")
            {
                PlayerJewelInven.Instance._player_Jewel_Inven_Content = child.gameObject;
            }
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            Jewel_Inventory_Button_Open();
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

        for (int i = 0; i < _jewel_inven.player_jewel_items.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            slots[i].item = _jewel_inven.player_jewel_items[i];
            slots[i].UpdateSlotUI();

        }

    }


    public void Jewel_Inventory_Button_Open()
    {
        active_jewel_Inventory = !active_jewel_Inventory;
        _jewel_UI_Panel.SetActive(active_jewel_Inventory);

        Managers.Sound.Play("Inven_Open");
        StartCoroutine(After_1second_Update_Inven_Slot());
        Managers.UI.SetCanvas(_jewel_canvas, true);
        RedrawSlotUI();
        
    }
    public void Xbutton_Exit()
    {
        if (_jewel_UI_Panel != null && _jewel_UI_Panel.activeSelf)
        {
            active_jewel_Inventory = !active_jewel_Inventory;
            _jewel_UI_Panel.SetActive(active_jewel_Inventory);
            Managers.Sound.Play("Inven_Open");
        }

        return;
    }

    /// <summary>
    /// �ν��Ͻ�ȭ �ð����� ���Ͽ� ������ ������ �������� ���� ������� �ʴ������� �ذ��ϱ� ���� 1�� ��� �� ��������Ʈ �Լ����� ȣ���մϴ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator After_1second_Update_Inven_Slot()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerJewelInven.Instance.onChangejewel.Invoke();
    }
}
