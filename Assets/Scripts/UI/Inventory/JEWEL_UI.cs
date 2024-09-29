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
    private PlayerJewelInven _jewel_inven; //플레이어 인벤토리 참조

    public JEWEL_Slot[] slots;
    public Transform slotHolder;

    public bool active_jewel_Inventory = false;

    private void Start()
    {

        _jewel_inven = PlayerJewelInven.Instance;
        slots = slotHolder.GetComponentsInChildren<JEWEL_Slot>();
        _jewel_inven.onChangejewel += RedrawSlotUI;
        Managers.UI.SetCanvas(_jewel_canvas, true);
        //인벤토리 드래그 가능하도록 하는 이벤트
        UI_Base.BindEvent(_jewel_UI_Panel, (PointerEventData data) => { _jewel_UI_Panel.transform.position = data.position; }, Define.UIEvent.Drag);


        TraverseChildrenRecursively(_jewel_UI_Panel.transform);


        _jewel_UI_Panel.SetActive(active_jewel_Inventory);


    }

    private void TraverseChildrenRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {

            TraverseChildrenRecursively(child);    // 재귀적으로 현재 자식의 자식들도 순회

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

        for (int i = 0; i < slots.Length; i++) //싹 밀어버리고
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i < _jewel_inven.player_jewel_items.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
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
    /// 인스턴스화 시간차에 의하여 아이템 갯수나 장착정보 등이 적용되지 않는현상을 해결하기 위해 1초 대기 후 델리게이트 함수들을 호출합니다.
    /// </summary>
    /// <returns></returns>
    IEnumerator After_1second_Update_Inven_Slot()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerJewelInven.Instance.onChangejewel.Invoke();
    }
}
