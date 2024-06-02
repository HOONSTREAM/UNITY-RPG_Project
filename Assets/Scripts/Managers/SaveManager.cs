using Data;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    private string sceneName;

    public void SavePlayerData()
    {
       
        GameObject player = Managers.Game.GetPlayer();

        ES3.Save("PlayerPosition", player.transform.position);
        ES3.Save("PlayerStat", player.GetComponent<PlayerStat>());
        ES3.Save("Player_Equip_Weapon",player.GetComponent<PlayerWeaponController>().Equip_Weapon);
        ES3.Save("Player_Class", player.GetComponent<Player_Class>().Get_Player_Class());

        PlayerInventory.Instance.SaveInventory();
        PlayerEquipment.Instance.Save_Equipment();
        PlayerStorage.Instance.Save_Storage();
        PlayerAbility.Instance.Save_Ability_Info();
        PlayerSkillQuickSlot.Instance.Save_Skill_Quickslot_Info();
        PlayerQuickSlot.Instance.Save_Item_Quickslot_Info();
        Player_Quest.Instance.Save_Player_Quest_Info();

      

        ES3.Save("CurrentScene", SceneManager.GetActiveScene().name);

        Debug.Log("Player data saved.");
    }

    
    public void LoadPlayerData()
    {
       
        if (ES3.KeyExists("PlayerPosition"))
        {
            Set_Player_SaveData();
        }
        else
        {
            Debug.LogWarning("저장된 데이터가 없습니다.");
        }
   
        return;
    }

    
    private void Set_Player_SaveData()
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_scene();
        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());


        DontDestroyOnLoad(Managers.Game.GetPlayer());
        DontDestroyOnLoad(GameObject.Find("Save_Data").gameObject);

        // 데이터 로드 로그
        Debug.Log("저장된 데이터를 로드합니다.");

        // 각 데이터 로드 후 확인
        Vector3 position = ES3.Load<Vector3>("PlayerPosition");
        Debug.Log($"PlayerPosition: {position}");

        PlayerStat Player_Stat = ES3.Load<PlayerStat>("PlayerStat");
        Debug.Log($"PlayerStat: {Player_Stat}");

        Item weapon_controller = ES3.Load<Item>("Player_Equip_Weapon");
        Debug.Log($"Player_Equip_Weapon: {weapon_controller}");

        Player_Class.ClassType class_type = ES3.Load<Player_Class.ClassType>("Player_Class");
        Debug.Log($"Player_Class: {class_type}");

        sceneName = ES3.Load<string>("CurrentScene");
        Debug.Log($"CurrentScene: {sceneName}");

        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

            Debug.Log("씬이 로드되었습니다.");

            GameObject player = Managers.Game.GetPlayer();

            player.transform.position = position; // 마지막 저장위치
            Debug.Log($"플레이어 위치 설정: {position}");

            PlayerStat stat = player.GetComponent<PlayerStat>(); // 마지막 저장 플레이어 스탯
            CopyPlayerStat(stat, Player_Stat);
            Debug.Log("플레이어 스탯 설정 완료.");

            player.GetComponent<Player_Class>().Set_Player_Class(class_type); // 마지막 저장 플레이어의 직업
            Debug.Log($"플레이어 직업 설정: {class_type}");

            PlayerInventory.Instance.LoadInventory();
            Debug.Log("인벤토리 로드 완료.");

            PlayerEquipment.Instance.Load_Equipment();
            Debug.Log("장비 로드 완료.");

            PlayerStorage.Instance.Load_Storage();
            Debug.Log("저장소 로드 완료.");

            PlayerAbility.Instance.Load_Ability_Info();
            Debug.Log("능력 정보 로드 완료.");

            PlayerSkillQuickSlot.Instance.Load_Skill_Quickslot_Info();
            Debug.Log("스킬 퀵슬롯 정보 로드 완료.");

            PlayerQuickSlot.Instance.Load_Item_Quickslot_Info();
            Debug.Log("아이템 퀵슬롯 정보 로드 완료.");

            Player_Quest.Instance.Load_Player_Quest_Info();
            Debug.Log("퀘스트 정보 로드 완료.");

            // 장착 무기에 따라 애니메이션 조정
            PlayerWeaponController weapon = player.GetComponent<PlayerWeaponController>();
            weapon.Equip_Weapon = weapon_controller;

            switch (weapon.Equip_Weapon.weapontype)
            {
                case WeaponType.One_Hand:
                    player.GetComponent<PlayerAnimController>().Change_oneHand_weapon_animClip();
                    Debug.Log("애니메이션 변경: One Hand");
                    break;
                case WeaponType.Two_Hand:
                    player.GetComponent<PlayerAnimController>().Change_TwoHand_weapon_animClip();
                    Debug.Log("애니메이션 변경: Two Hand");
                    break;
                case WeaponType.No_Weapon:
                    player.GetComponent<PlayerAnimController>().Change_No_Weapon_animClip();
                    Debug.Log("애니메이션 변경: No Weapon");
                    break;
            }

            StartCoroutine(After_1second_Load_Player_Info()); // 인스턴스화 시간차 극복을 위해, 일정 시간 뒤에 스탯 데이터 로드
            Debug.Log("1초 후 플레이어 정보 로드 시작.");

            Debug.Log("플레이어 데이터 로드 완료.");


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

    IEnumerator After_1second_Load_Player_Info()
    {
        yield return new WaitForSeconds(0.3f);
        Managers.Game.GetPlayer().GetComponent<PlayerStat>().onchangestat.Invoke();
        Managers.Game.GetPlayer().GetComponent<PlayerAbility>().onChangeSkill.Invoke();
        Managers.Game.GetPlayer().GetComponent<PlayerSkillQuickSlot>().onChangeskill_quickslot.Invoke();
        Managers.Game.GetPlayer().GetComponent<PlayerQuickSlot>().onChangeItem.Invoke();
        Managers.Game.GetPlayer().GetComponent<Player_Quest>().onChangequest.Invoke();

        Move_To_LastSave_Scene();
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

}

