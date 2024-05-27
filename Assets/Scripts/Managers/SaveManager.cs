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
    // TODO : 스텟 즉시반영 안됨, 인벤토리 즉시 반영안됨
    public void SavePlayerData()
    {
        GameObject player = Managers.Game.GetPlayer();

        ES3.Save("PlayerPosition", player.transform.position);
        ES3.Save("PlayerStat", player.GetComponent<PlayerStat>());
        ES3.Save("Player_Equip_Weapon",player.GetComponent<PlayerWeaponController>().Equip_Weapon);

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
            Item weapon_controller = ES3.Load<Item>("Player_Equip_Weapon");


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


                    StartCoroutine(After_1second_Load_Inven_and_Stat()); // 인스턴스화 시간차 극복을 위해, 1.5초 뒤에 스텟과 인벤 데이터 로드

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

    private void CopyPlayerInven(PlayerInventory dest, PlayerInventory src)
    {     
        dest._player_Inven_Content = src._player_Inven_Content;
        dest.onChangeItem = src.onChangeItem;
        dest.slot = src.slot;
        dest.player_items = new List<Item>(src.player_items);
    }

    IEnumerator After_1second_Load_Inven_and_Stat()
    {
        yield return new WaitForSeconds(1.0f);
        Managers.Game.GetPlayer().GetComponent<PlayerStat>().onchangestat.Invoke();

        PlayerInventory.Instance.onChangeItem.Invoke(); //TODO : 인벤토리가 열려야 업데이트 되는 문제 해결 필요

    }

}

