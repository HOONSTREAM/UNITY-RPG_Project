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
            Debug.LogWarning("����� �����Ͱ� �����ϴ�.");
        }
   
        return;
    }

    
    private void Set_Player_SaveData()
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_scene();
        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());


        DontDestroyOnLoad(Managers.Game.GetPlayer());
        DontDestroyOnLoad(GameObject.Find("Save_Data").gameObject);

        // ������ �ε� �α�
        Debug.Log("����� �����͸� �ε��մϴ�.");

        // �� ������ �ε� �� Ȯ��
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

            Debug.Log("���� �ε�Ǿ����ϴ�.");

            GameObject player = Managers.Game.GetPlayer();

            player.transform.position = position; // ������ ������ġ
            Debug.Log($"�÷��̾� ��ġ ����: {position}");

            PlayerStat stat = player.GetComponent<PlayerStat>(); // ������ ���� �÷��̾� ����
            CopyPlayerStat(stat, Player_Stat);
            Debug.Log("�÷��̾� ���� ���� �Ϸ�.");

            player.GetComponent<Player_Class>().Set_Player_Class(class_type); // ������ ���� �÷��̾��� ����
            Debug.Log($"�÷��̾� ���� ����: {class_type}");

            PlayerInventory.Instance.LoadInventory();
            Debug.Log("�κ��丮 �ε� �Ϸ�.");

            PlayerEquipment.Instance.Load_Equipment();
            Debug.Log("��� �ε� �Ϸ�.");

            PlayerStorage.Instance.Load_Storage();
            Debug.Log("����� �ε� �Ϸ�.");

            PlayerAbility.Instance.Load_Ability_Info();
            Debug.Log("�ɷ� ���� �ε� �Ϸ�.");

            PlayerSkillQuickSlot.Instance.Load_Skill_Quickslot_Info();
            Debug.Log("��ų ������ ���� �ε� �Ϸ�.");

            PlayerQuickSlot.Instance.Load_Item_Quickslot_Info();
            Debug.Log("������ ������ ���� �ε� �Ϸ�.");

            Player_Quest.Instance.Load_Player_Quest_Info();
            Debug.Log("����Ʈ ���� �ε� �Ϸ�.");

            // ���� ���⿡ ���� �ִϸ��̼� ����
            PlayerWeaponController weapon = player.GetComponent<PlayerWeaponController>();
            weapon.Equip_Weapon = weapon_controller;

            switch (weapon.Equip_Weapon.weapontype)
            {
                case WeaponType.One_Hand:
                    player.GetComponent<PlayerAnimController>().Change_oneHand_weapon_animClip();
                    Debug.Log("�ִϸ��̼� ����: One Hand");
                    break;
                case WeaponType.Two_Hand:
                    player.GetComponent<PlayerAnimController>().Change_TwoHand_weapon_animClip();
                    Debug.Log("�ִϸ��̼� ����: Two Hand");
                    break;
                case WeaponType.No_Weapon:
                    player.GetComponent<PlayerAnimController>().Change_No_Weapon_animClip();
                    Debug.Log("�ִϸ��̼� ����: No Weapon");
                    break;
            }

            StartCoroutine(After_1second_Load_Player_Info()); // �ν��Ͻ�ȭ �ð��� �غ��� ����, ���� �ð� �ڿ� ���� ������ �ε�
            Debug.Log("1�� �� �÷��̾� ���� �ε� ����.");

            Debug.Log("�÷��̾� ������ �ε� �Ϸ�.");


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
            case "�絧�þ�":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_scene();
                break;
            case "�絧�þ� ����":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_shop();
                break;
            case "�絧�þ� ����":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_south();
                break;
            case "�絧�þ� ����2":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_South2_Scene();
                break;
            case "�絧�þ� ������":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_jewel_Scene();
                break;
            case "�絧�þ� ����":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_inn_Scene();
                break;
            case "�絧�þ� ����":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_bank_Scene();
                break;
            case "���� ������ ��":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_House_chief_Scene();
                break;

            default:
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Start_Scene();
                break;

        }

        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
    }

}

