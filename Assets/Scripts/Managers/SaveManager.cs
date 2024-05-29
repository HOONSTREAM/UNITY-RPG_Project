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

    // 플레이어 데이터 저장
    // TODO : 암호화, 타이틀창에서 저장 슬롯창 띄워 선택저장 (복사 여러개 가능하도록)
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

    // 플레이어 데이터 로드
    public void LoadPlayerData()
    {
        if (ES3.KeyExists("PlayerPosition"))
        {
            // 데이터를 미리 로드합니다.
            Vector3 position = ES3.Load<Vector3>("PlayerPosition");
            PlayerStat Player_Stat = ES3.Load<PlayerStat>("PlayerStat");
            Item weapon_controller = ES3.Load<Item>("Player_Equip_Weapon");
            Player_Class.ClassType class_type = ES3.Load<Player_Class.ClassType>("Player_Class");

            string sceneName = ES3.Load<string>("CurrentScene");


            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);


            void OnSceneLoaded(Scene scene, LoadSceneMode mode)
            {
                if (scene.name == sceneName)
                {
                    // 씬이 로드된 후 플레이어 배치
                    GameObject player = Managers.Game.GetPlayer();
                    
                    player.transform.position = position; // 마지막 저장위치

                    PlayerStat stat = player.GetComponent<PlayerStat>(); //마지막 저장 플레이어 스텟
                    CopyPlayerStat(stat, Player_Stat);

                    player.GetComponent<Player_Class>().Set_Player_Class(class_type); //마지막 저장 플레이어의 직업
                    

                    PlayerInventory.Instance.LoadInventory();
                    PlayerEquipment.Instance.Load_Equipment();
                    PlayerStorage.Instance.Load_Storage();
                    PlayerAbility.Instance.Load_Ability_Info();
                    PlayerSkillQuickSlot.Instance.Load_Skill_Quickslot_Info();
                    PlayerQuickSlot.Instance.Load_Item_Quickslot_Info();
                    Player_Quest.Instance.Load_Player_Quest_Info();


                    // 장착 무기에 따라 애니메이션을 조정합니다.

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


                    StartCoroutine(After_1second_Load_Player_Info()); // 인스턴스화 시간차 극복을 위해, 일정시간 뒤에 스텟데이터 로드

                    Debug.Log("Player data loaded.");

                }

               
            }


        }
        else
        {
            Debug.LogWarning("No save data found.");
        }


        return;
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
    }

}

