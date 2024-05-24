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
            // 데이터를 미리 로드합니다.
            Vector3 position = ES3.Load<Vector3>("PlayerPosition");
            PlayerStat Player_Stat = ES3.Load<PlayerStat>("PlayerStat");
            PlayerInventory inventory = ES3.Load<PlayerInventory>("PlayerInventory");
            PlayerEquipment equipment = ES3.Load<PlayerEquipment>("PlayerEquipment");
            PlayerStorage storage = ES3.Load<PlayerStorage>("PlayerStorage");
            PlayerQuickSlot quick_slot = ES3.Load<PlayerQuickSlot>("PlayerQuickSlot");
            PlayerAbility ability = ES3.Load<PlayerAbility>("PlayerAbility");
            Player_Quest quest = ES3.Load<Player_Quest>("PlayerQuest");
            PlayerSkillQuickSlot skill_quick_slot = ES3.Load<PlayerSkillQuickSlot>("PlayerSkillQuickSlot");
            Player_Class Class = ES3.Load<Player_Class>("PlayerClass");

            string sceneName = ES3.Load<string>("CurrentScene");

            // 씬 로드 후 이벤트 핸들러 등록 
            /*sceneLoaded 이벤트
             sceneLoaded 이벤트는 Unity에서 새로운 씬이 로드될 때 발생합니다. 이 이벤트는 SceneManager 클래스의 정적 이벤트로, 씬이 로드된 후에 실행되는 콜백 메서드를 등록하는 데 사용됩니다.
             SceneManager.sceneLoaded += OnSceneLoaded; 코드는 OnSceneLoaded 메서드를 sceneLoaded 이벤트에 등록합니다. 이렇게 하면 씬이 로드될 때마다 OnSceneLoaded 메서드가 호출됩니다.*/

            SceneManager.sceneLoaded += OnSceneLoaded;

            // 씬 로드
            SceneManager.LoadScene(sceneName);

            void OnSceneLoaded(Scene scene, LoadSceneMode mode)
            {
                if (scene.name == sceneName)
                {
                    // 씬이 로드된 후 플레이어 배치
                    GameObject player = Managers.Game.GetPlayer();
                    player.transform.position = position; // 마지막 저장위치

                    // PlayerStat 설정
                    PlayerStat stat = player.GetComponent<PlayerStat>();
                    CopyPlayerStat(stat, Player_Stat);

                    //// PlayerInventory 설정
                    //PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
                    //CopyInventory(playerInventory, inventory);

                    //// PlayerEquipment 설정
                    //PlayerEquipment playerEquip = player.GetComponent<PlayerEquipment>();
                    //CopyPlayerEquipment(playerEquip, equipment);

                    //// PlayerStorage 설정
                    //PlayerStorage playerStorage = player.GetComponent<PlayerStorage>();
                    //CopyPlayerStorage(playerStorage, storage);

                    //// PlayerQuickSlot 설정
                    //PlayerQuickSlot playerQuickSlot = player.GetComponent<PlayerQuickSlot>();
                    //CopyPlayerQuickSlot(playerQuickSlot, quick_slot);

                    //// PlayerAbility 설정
                    //PlayerAbility playerAbility = player.GetComponent<PlayerAbility>();
                    //CopyPlayerAbility(playerAbility, ability);

                    //// Player_Quest 설정
                    //Player_Quest playerQuest = player.GetComponent<Player_Quest>();
                    //CopyPlayerQuest(playerQuest, quest);

                    //// PlayerSkillQuickSlot 설정
                    //PlayerSkillQuickSlot playerSkillQuickSlot = player.GetComponent<PlayerSkillQuickSlot>();
                    //CopyPlayerSkillQuickSlot(playerSkillQuickSlot, skill_quick_slot);

                    //// Player_Class 설정
                    //Player_Class playerClassComponent = player.GetComponent<Player_Class>();
                    //CopyPlayerClass(playerClassComponent, Class);

                    Debug.Log("Player data loaded.");
                    // 이벤트 핸들러 등록 해제
                    SceneManager.sceneLoaded -= OnSceneLoaded;
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

    //private void CopyPlayerEquipment(PlayerEquipment dest, PlayerEquipment src)
    //{
    //    dest.weapon = src.weapon;
    //    dest.armor = src.armor;
    //    // 필요한 다른 필드도 복사합니다.
    //}

    //private void CopyPlayerStorage(PlayerStorage dest, PlayerStorage src)
    //{
    //    dest.items = new List<Item>(src.items);
    //    // 필요한 다른 필드도 복사합니다.
    //}

    //private void CopyPlayerQuickSlot(PlayerQuickSlot dest, PlayerQuickSlot src)
    //{
    //    dest.slots = new List<QuickSlot>(src.slots);
    //    // 필요한 다른 필드도 복사합니다.
    //}

    //private void CopyPlayerAbility(PlayerAbility dest, PlayerAbility src)
    //{
    //    dest.abilities = new List<Ability>(src.abilities);
    //    // 필요한 다른 필드도 복사합니다.
    //}

    //private void CopyPlayerQuest(Player_Quest dest, Player_Quest src)
    //{
    //    dest.quests = new List<Quest>(src.quests);
    //    // 필요한 다른 필드도 복사합니다.
    //}

    //private void CopyPlayerSkillQuickSlot(PlayerSkillQuickSlot dest, PlayerSkillQuickSlot src)
    //{
    //    dest.skillSlots = new List<SkillQuickSlot>(src.skillSlots);
    //    // 필요한 다른 필드도 복사합니다.
    //}

    //private void CopyPlayerClass(Player_Class dest, Player_Class src)
    //{
    //    dest.classType = src.classType;
    //    dest.level = src.level;
    //    // 필요한 다른 필드도 복사합니다.
    //}
}

