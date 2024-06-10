using Data;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(100)]
public class SaveManager : MonoBehaviour
{

    private const float SAVE_COMPLETE_ALARM_DURATION = 5.0f;
    private const string SAVE_COMPLETE_RESOURCE = "Save_Complete";
    private const string LOAD_COMPLETE_RESOURCE = "Load_Complete";
    private const string SAVE_COMPLETE_SOUND = "GUI_Sound/misc_sound";
    private const string LOADING_BACKGROUND = "LOADING_CANVAS";
    private const float WAIT_FOR_SET_STAT_AND_GOLD = 1.0f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private string sceneName;

    public void SavePlayerData()
    {
       
        GameObject player = Managers.Game.GetPlayer();

        if (player == null)
            return;

        SavePlayerDetails(player);
        SavePlayerProgress();

        ES3.Save("CurrentScene", SceneManager.GetActiveScene().name);

        ShowSaveCompleteAlarm();
    }


    private void SavePlayerDetails(GameObject player)
    {
        ES3.Save("PlayerPosition", player.transform.position);
        ES3.Save("PlayerStat", player.GetComponent<PlayerStat>());
        ES3.Save("Player_Equip_Weapon", player.GetComponent<PlayerWeaponController>().Equip_Weapon);
        ES3.Save("Player_Class", player.GetComponent<Player_Class>().Get_Player_Class());
    }

    private void SavePlayerProgress()
    {
        PlayerInventory.Instance.SaveInventory();
        PlayerEquipment.Instance.Save_Equipment();
        PlayerStorage.Instance.Save_Storage();
        PlayerAbility.Instance.Save_Ability_Info();
        PlayerSkillQuickSlot.Instance.Save_Skill_Quickslot_Info();
        PlayerQuickSlot.Instance.Save_Item_Quickslot_Info();
        QuestDatabase.instance.Save_QuestDB_Info();
    }

    private void ShowSaveCompleteAlarm()
    {
        GameObject go = Managers.Resources.Instantiate(SAVE_COMPLETE_RESOURCE).gameObject;
        Managers.Sound.Play(SAVE_COMPLETE_SOUND);
        Destroy(go, SAVE_COMPLETE_ALARM_DURATION);
    }

    public void ShowLoadCompleteAlarm()
    {
        GameObject go = Managers.Resources.Instantiate(LOAD_COMPLETE_RESOURCE).gameObject;
        Managers.Sound.Play(SAVE_COMPLETE_SOUND);
        Destroy(go, SAVE_COMPLETE_ALARM_DURATION);
    }

    public void LoadPlayerData()
    {
        if (ES3.KeyExists("PlayerPosition"))
        {            
            Set_Player_SaveData();
        }
    }

    private void Set_Player_SaveData()
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_scene();

        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());

        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;

        DontDestroyOnLoad(player);
        GameObject saveData = GameObject.Find("Save_Data");
        if (saveData != null)
        {
            DontDestroyOnLoad(saveData);
        }

        Vector3 position = ES3.Load<Vector3>("PlayerPosition");
        PlayerStat Player_Stat = ES3.Load<PlayerStat>("PlayerStat");
        Item weapon_controller = ES3.Load<Item>("Player_Equip_Weapon");
        Player_Class.ClassType class_type = ES3.Load<Player_Class.ClassType>("Player_Class");
        sceneName = ES3.Load<string>("CurrentScene");

        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {


            GameObject player = Managers.Game.GetPlayer();

            player.transform.position = position; // 마지막 저장위치           
            PlayerStat stat = player.GetComponent<PlayerStat>(); // 마지막 저장 플레이어 스탯
            CopyPlayerStat(stat, Player_Stat);
        
            player.GetComponent<Player_Class>().Set_Player_Class(class_type); // 마지막 저장 플레이어의 직업
          
            PlayerInventory.Instance.LoadInventory();          
            PlayerEquipment.Instance.Load_Equipment();      
            PlayerStorage.Instance.Load_Storage();            
            PlayerAbility.Instance.Load_Ability_Info();       
            PlayerSkillQuickSlot.Instance.Load_Skill_Quickslot_Info();
            PlayerQuickSlot.Instance.Load_Item_Quickslot_Info();
            QuestDatabase.instance.Load_QuestDB_Info();


            PlayerWeaponController weapon = player.GetComponent<PlayerWeaponController>();
            weapon.Equip_Weapon = weapon_controller;

            switch (weapon.Equip_Weapon.weapontype)
            {
                case WeaponType.One_Hand:
                    player.GetComponent<PlayerAnimController>().Change_oneHand_weapon_animClip();
                  
                    break;
                case WeaponType.Two_Hand:
                    player.GetComponent<PlayerAnimController>().Change_TwoHand_weapon_animClip();
                   
                    break;
                case WeaponType.No_Weapon:
                    player.GetComponent<PlayerAnimController>().Change_No_Weapon_animClip();
             
                    break;
            }


            Move_To_LastSave_Scene();
            Coroutine_Runner.Instance.StartCoroutine(Set_PlayerStat_and_Gold());

        }
    }

    private void Move_To_LastSave_Scene()
    {
        switch (sceneName)
        {
            case "루덴시안":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_scene();
                break;
            case "루덴시안 상점":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_shop();
                break;
            case "루덴시안 남쪽":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_south();
                break;
            case "루덴시안 남쪽2":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_South2_Scene();
                break;
            case "루덴시안 보석상":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_jewel_Scene();
                break;
            case "루덴시안 여관":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_inn_Scene();
                break;
            case "루덴시안 은행":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_bank_Scene();
                break;
            case "촌장 월터의 집":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_House_chief_Scene();
                break;

            default:
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Start_Scene();
                break;

        }

        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
        
    }

    IEnumerator Set_PlayerStat_and_Gold()
    {
        yield return new WaitForSeconds(WAIT_FOR_SET_STAT_AND_GOLD);
        Managers.Game.GetPlayer().GetComponent<PlayerStat>().onchangestat.Invoke();
        Managers.Game.GetPlayer().GetComponent<PlayerAbility>().onChangeSkill.Invoke();
        Managers.Game.GetPlayer().GetComponent<PlayerSkillQuickSlot>().onChangeskill_quickslot.Invoke();
        Managers.Game.GetPlayer().GetComponent<PlayerQuickSlot>().onChangeItem.Invoke();
        Managers.Game.GetPlayer().GetComponent<Player_Quest>().onChangequest.Invoke();
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

    
}

