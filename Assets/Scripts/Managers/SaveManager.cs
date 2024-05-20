using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{


    // 플레이어 데이터 저장
    public void SavePlayerData()
    {
        GameObject player = Managers.Game.GetPlayer();

        ES3.Save("PlayerPosition", player.transform.position);
        ES3.Save("PlayerStat", player.GetComponent <PlayerStat>());
        ES3.Save("PlayerInventory", player.GetComponent<PlayerInventory>());
        ES3.Save("PlayerEquipment", player.GetComponent<PlayerEquipment>());
        ES3.Save("PlayerStorage", player.GetComponent<PlayerStorage>());
        ES3.Save("PlayerQuickSlot", player.GetComponent<PlayerQuickSlot>());
        ES3.Save("PlayerAbility", player.GetComponent<PlayerAbility>());
        ES3.Save("PlayerQuest", player.GetComponent<Player_Quest>());
        ES3.Save("PlayerSkillQuickSlot", player.GetComponent<PlayerSkillQuickSlot>());
        ES3.Save("PlayerClass", player.GetComponent<Player_Class>()); 
        
        ES3.Save("CurrentScene", SceneManager.GetActiveScene().name);

        Debug.Log("Player data saved.");
    }

    // 플레이어 데이터 로드
    public void LoadPlayerData()
    {
        if (ES3.KeyExists("PlayerPosition"))
        {
            Vector3 position = ES3.Load<Vector3>("PlayerPosition");
            PlayerStat Player_Stat = ES3.Load<PlayerStat>("PlayerStat");
            PlayerInventory inventory = ES3.Load<PlayerInventory>("PlayerInventory");
            PlayerEquipment equipment = ES3.Load<PlayerEquipment>("PlayerEquipment");
            PlayerStorage storage = ES3.Load<PlayerStorage>("PlayerStorage");
            PlayerQuickSlot quick_slot = ES3.Load<PlayerQuickSlot>("PlayerQuickSlot");
            PlayerAbility ability = ES3.Load<PlayerAbility>("PlayerAbility");
            Player_Quest  quest = ES3.Load<Player_Quest>("PlayerQuest");
            PlayerSkillQuickSlot skill_quick_slot = ES3.Load<PlayerSkillQuickSlot>("PlayerSkillQuickSlot");
            Player_Class Class = ES3.Load<Player_Class>("PlayerClass");


            string sceneName = ES3.Load<string>("CurrentScene");

            SceneManager.LoadScene(sceneName);

            // 씬 로드 후 플레이어 배치
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (scene.name == sceneName)
                {
                    //GameObject Spawn_player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
                    //Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);

                    GameObject player = Managers.Game.GetPlayer();
                    player.transform.position = position; // 마지막 저장위치

                    PlayerStat stat = player.GetComponent<PlayerStat>();
                    stat = Player_Stat;

                    PlayerInventory Player_Inven = player.GetComponent<PlayerInventory>();
                    Player_Inven = inventory;

                    PlayerEquipment player_Equip = player.GetComponent<PlayerEquipment>();
                    player_Equip = equipment;

                    PlayerStorage player_storage = player.GetComponent<PlayerStorage>();
                    player_storage = storage;

                    PlayerQuickSlot player_quickslot = player.GetComponent<PlayerQuickSlot>();
                    player_quickslot = quick_slot;

                    PlayerAbility player_ability = player.GetComponent<PlayerAbility>();
                    player_ability = ability;

                    Player_Quest player_Quest = player.GetComponent<Player_Quest>();
                    player_Quest = quest;

                    PlayerSkillQuickSlot playerSkillQuickSlot = player.GetComponent<PlayerSkillQuickSlot>();
                    playerSkillQuickSlot = skill_quick_slot;

                    Player_Class player_Class = player.GetComponent<Player_Class>();
                    player_Class = Class;


                    Debug.Log("Player data loaded.");
                }
            };
        }
        else
        {
            Debug.LogWarning("No save data found.");

            return;
        }
    }
}
