using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Exit_Console : MonoBehaviour
{
    public GameObject ExitConsole;

    public GameObject INVENTORY_CANVAS;
    public GameObject QuestSlot_CANVAS;
    public GameObject Abillity_CANVAS;
    public GameObject Storage_CANVAS;

    public GameObject InvenUI;
    public GameObject EquipmentUI;
    public GameObject QuestSlotUI;
    public GameObject Abillity_slot_UI;
    public GameObject Storage_slot_UI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(InvenUI.activeSelf || EquipmentUI.activeSelf || QuestSlotUI.activeSelf || Abillity_slot_UI.activeSelf || Storage_slot_UI.activeSelf)
            {
                Debug.Log("먼저 열려있는 창이 있다.");

                int inven_sort_order = INVENTORY_CANVAS.gameObject.GetComponent<Canvas>().sortingOrder;
                int quest_sort_order = QuestSlot_CANVAS.gameObject.GetComponent<Canvas>().sortingOrder;
                int abillity_sort_order = Abillity_CANVAS.gameObject.GetComponent<Canvas>().sortingOrder;
                int storage_sort_order = Storage_CANVAS.gameObject.GetComponent<Canvas>().sortingOrder;

                int max_sort_order = Mathf.Max(inven_sort_order, quest_sort_order, abillity_sort_order, storage_sort_order);

                Debug.Log(max_sort_order);
            }

            Managers.Sound.Play("Coin");
            if (ExitConsole.gameObject.activeSelf)
            {
                ExitConsole.gameObject.SetActive(false);
                return;
            }

            ExitConsole.gameObject.SetActive(true);
            
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void NotExitGame()
    {
        Managers.Sound.Play("Coin");
        ExitConsole.gameObject.SetActive(false);
    }
}
