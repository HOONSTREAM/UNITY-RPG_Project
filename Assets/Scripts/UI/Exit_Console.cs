using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Exit_Console : MonoBehaviour
{
    public GameObject ExitConsole;

    public List<GameObject> UI_panels;

    public GameObject InvenUI;
    public GameObject EquipmentUI;
    public GameObject QuestSlotUI;
    public GameObject Ability_slot_UI;
    public GameObject Storage_slot_UI;

  
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(InvenUI.activeSelf || EquipmentUI.activeSelf || QuestSlotUI.activeSelf || Ability_slot_UI.activeSelf || Storage_slot_UI.activeSelf)
            {
              

                int highestSortOrder = int.MinValue;

                GameObject PanelToClose = null;

                foreach(GameObject panel in UI_panels)
                {
                    
                    Canvas canvas = panel.GetComponent<Canvas>();
                    if (canvas != null && canvas.sortingOrder > highestSortOrder)
                    {
                        highestSortOrder = canvas.sortingOrder;
                        Debug.Log(highestSortOrder);
                        PanelToClose = panel;
                        
                    }

                }

                if (PanelToClose != null)
                {
                    Debug.Log(PanelToClose.name);

                    switch (PanelToClose.name)
                    {
                        case "INVENTORY CANVAS":

                            InvenUI.gameObject.SetActive(false);
                            EquipmentUI.gameObject.SetActive(false);

                            PanelToClose.gameObject.GetComponent<NewInvenUI>().activeInventory = false;
                            PanelToClose.gameObject.GetComponent<Equipment_UI>().active_equipment_panel = false;

                            Managers.Sound.Play("Inven_Open");
                            PanelToClose.gameObject.GetComponent<Canvas>().sortingOrder = 0;
                            
                            break;                     
                        case "QuestSlot CANVAS":
                            QuestSlotUI.gameObject.SetActive(false);
                            PanelToClose.gameObject.GetComponent<Quest_Script>().activequestpanel = false;
                            Managers.Sound.Play("Inven_Open");
                            PanelToClose.gameObject.GetComponent<Canvas>().sortingOrder = 0;
                            Player_Quest.Instance.onChangequest.Invoke();
                            break;
                        case "Ability_Slot_CANVAS":
                            Ability_slot_UI.gameObject.SetActive(false);
                            PanelToClose.gameObject.GetComponent<Ability_Script>().active_Ability_panel = false;
                            Managers.Sound.Play("Inven_Open");
                            PanelToClose.gameObject.GetComponent<Canvas>().sortingOrder = 0;
                            break;
                        case "Storage CANVAS":
                            Storage_slot_UI.gameObject.SetActive(false);
                            PanelToClose.gameObject.GetComponent<Storage_Script>().activestorage = false;
                            for(int i = 0; i < PanelToClose.gameObject.GetComponent<Storage_Script>().Player_slots.Length; i++)
                            {
                                PanelToClose.gameObject.GetComponent<Storage_Script>().Player_slots[i].isStorageMode = false;
                            }                            
                            Managers.Sound.Play("Inven_Open");
                            PanelToClose.gameObject.GetComponent<Canvas>().sortingOrder = 0;
                            break;

                    }
                }

                return;
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
