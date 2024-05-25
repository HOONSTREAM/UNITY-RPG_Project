using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{


    // 플레이어 데이터 저장
    public void SavePlayerData()
    {
        GameObject player = Managers.Game.GetPlayer();

        ES3.Save("PlayerPosition", player.transform.position);
        ES3.Save("PlayerStat", player.GetComponent<PlayerStat>());
        PlayerInventory.Instance.SaveInventory();
        PlayerEquipment.Instance.Save_Equipment();
      

        ES3.Save("CurrentScene", SceneManager.GetActiveScene().name);

        Debug.Log("Player data saved.");
    }

    // 플레이어 데이터 로드
    public void LoadPlayerData()
    {
        if (ES3.KeyExists("PlayerPosition"))
        {
            // 데이터를 미리 로드합니다.
            Vector3 position = ES3.Load<Vector3>("PlayerPosition");
            PlayerStat Player_Stat = ES3.Load<PlayerStat>("PlayerStat");
           
            string sceneName = ES3.Load<string>("CurrentScene");


            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);


            void OnSceneLoaded(Scene scene, LoadSceneMode mode)
            {
                if (scene.name == sceneName)
                {
                    // 씬이 로드된 후 플레이어 배치
                    GameObject player = Managers.Game.GetPlayer();
                    player.transform.position = position; // 마지막 저장위치

                    PlayerStat stat = player.GetComponent<PlayerStat>();
                    CopyPlayerStat(stat, Player_Stat);
                    PlayerInventory.Instance.LoadInventory();
                    PlayerEquipment.Instance.Load_Equipment();


                    Debug.Log("Player data loaded.");
              
                }
            }


        }
        else
        {
            Debug.LogWarning("No save data found.");
        }
    }

    private void CopyPlayerStat(PlayerStat dest, PlayerStat src)
    {
        dest.LEVEL = src.LEVEL;
        dest.Hp = src.Hp;
        dest.Mp = src.Mp;
        dest.MAXHP = src.MAXHP;
        dest.MaxMp = src.MaxMp;
        dest.MOVESPEED = src.MOVESPEED;
        dest.ATTACK = src.ATTACK;
        dest.DEFENSE = src.DEFENSE;
        dest.EXP = src.EXP;
        dest.Gold = src.Gold;
        dest.STR = src.STR;
        dest.INT = src.INT;
        dest.VIT = src.VIT;
        dest.AGI = src.AGI;
        dest.one_hand_sword_AbilityAttack = src.one_hand_sword_AbilityAttack;
        dest.two_hand_sword_AbilityAttack = src.two_hand_sword_AbilityAttack;
        dest.improvement_Ability_attack = src.improvement_Ability_attack;
        dest.buff_damage = 0;
        dest.buff_DEFENSE = 0;

    }

    private void CopyPlayerInven(PlayerInventory dest, PlayerInventory src)
    {     
        dest._player_Inven_Content = src._player_Inven_Content;
        dest.onChangeItem = src.onChangeItem;
        dest.slot = src.slot;
        dest.player_items = new List<Item>(src.player_items);
    }


}

