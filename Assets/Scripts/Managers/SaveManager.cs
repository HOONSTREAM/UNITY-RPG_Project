using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{


    // �÷��̾� ������ ����
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

    // �÷��̾� ������ �ε�
    public void LoadPlayerData()
    {
        if (ES3.KeyExists("PlayerPosition"))
        {
            // �����͸� �̸� �ε��մϴ�.
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

            // �� �ε� �� �̺�Ʈ �ڵ鷯 ��� 
            /*sceneLoaded �̺�Ʈ
             sceneLoaded �̺�Ʈ�� Unity���� ���ο� ���� �ε�� �� �߻��մϴ�. �� �̺�Ʈ�� SceneManager Ŭ������ ���� �̺�Ʈ��, ���� �ε�� �Ŀ� ����Ǵ� �ݹ� �޼��带 ����ϴ� �� ���˴ϴ�.
             SceneManager.sceneLoaded += OnSceneLoaded; �ڵ�� OnSceneLoaded �޼��带 sceneLoaded �̺�Ʈ�� ����մϴ�. �̷��� �ϸ� ���� �ε�� ������ OnSceneLoaded �޼��尡 ȣ��˴ϴ�.*/

            SceneManager.sceneLoaded += OnSceneLoaded;

            // �� �ε�
            SceneManager.LoadScene(sceneName);

            void OnSceneLoaded(Scene scene, LoadSceneMode mode)
            {
                if (scene.name == sceneName)
                {
                    // ���� �ε�� �� �÷��̾� ��ġ
                    GameObject player = Managers.Game.GetPlayer();
                    player.transform.position = position; // ������ ������ġ

                    // PlayerStat ����
                    PlayerStat stat = player.GetComponent<PlayerStat>();
                    CopyPlayerStat(stat, Player_Stat);

                    //// PlayerInventory ����
                    //PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
                    //CopyInventory(playerInventory, inventory);

                    //// PlayerEquipment ����
                    //PlayerEquipment playerEquip = player.GetComponent<PlayerEquipment>();
                    //CopyPlayerEquipment(playerEquip, equipment);

                    //// PlayerStorage ����
                    //PlayerStorage playerStorage = player.GetComponent<PlayerStorage>();
                    //CopyPlayerStorage(playerStorage, storage);

                    //// PlayerQuickSlot ����
                    //PlayerQuickSlot playerQuickSlot = player.GetComponent<PlayerQuickSlot>();
                    //CopyPlayerQuickSlot(playerQuickSlot, quick_slot);

                    //// PlayerAbility ����
                    //PlayerAbility playerAbility = player.GetComponent<PlayerAbility>();
                    //CopyPlayerAbility(playerAbility, ability);

                    //// Player_Quest ����
                    //Player_Quest playerQuest = player.GetComponent<Player_Quest>();
                    //CopyPlayerQuest(playerQuest, quest);

                    //// PlayerSkillQuickSlot ����
                    //PlayerSkillQuickSlot playerSkillQuickSlot = player.GetComponent<PlayerSkillQuickSlot>();
                    //CopyPlayerSkillQuickSlot(playerSkillQuickSlot, skill_quick_slot);

                    //// Player_Class ����
                    //Player_Class playerClassComponent = player.GetComponent<Player_Class>();
                    //CopyPlayerClass(playerClassComponent, Class);

                    Debug.Log("Player data loaded.");
                    // �̺�Ʈ �ڵ鷯 ��� ����
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
    //    // �ʿ��� �ٸ� �ʵ嵵 �����մϴ�.
    //}

    //private void CopyPlayerStorage(PlayerStorage dest, PlayerStorage src)
    //{
    //    dest.items = new List<Item>(src.items);
    //    // �ʿ��� �ٸ� �ʵ嵵 �����մϴ�.
    //}

    //private void CopyPlayerQuickSlot(PlayerQuickSlot dest, PlayerQuickSlot src)
    //{
    //    dest.slots = new List<QuickSlot>(src.slots);
    //    // �ʿ��� �ٸ� �ʵ嵵 �����մϴ�.
    //}

    //private void CopyPlayerAbility(PlayerAbility dest, PlayerAbility src)
    //{
    //    dest.abilities = new List<Ability>(src.abilities);
    //    // �ʿ��� �ٸ� �ʵ嵵 �����մϴ�.
    //}

    //private void CopyPlayerQuest(Player_Quest dest, Player_Quest src)
    //{
    //    dest.quests = new List<Quest>(src.quests);
    //    // �ʿ��� �ٸ� �ʵ嵵 �����մϴ�.
    //}

    //private void CopyPlayerSkillQuickSlot(PlayerSkillQuickSlot dest, PlayerSkillQuickSlot src)
    //{
    //    dest.skillSlots = new List<SkillQuickSlot>(src.skillSlots);
    //    // �ʿ��� �ٸ� �ʵ嵵 �����մϴ�.
    //}

    //private void CopyPlayerClass(Player_Class dest, Player_Class src)
    //{
    //    dest.classType = src.classType;
    //    dest.level = src.level;
    //    // �ʿ��� �ٸ� �ʵ嵵 �����մϴ�.
    //}
}

