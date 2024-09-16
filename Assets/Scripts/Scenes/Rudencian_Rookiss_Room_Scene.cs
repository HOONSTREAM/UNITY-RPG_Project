using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rudencian_Rookiss_Room_Scene : BaseScene
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Terrain _rookiss_terrain;
    private readonly int TERRAIN_LAYER_GROUND = 9;
    
    protected override void Init()
    {

        base.Init();

        Managers.Game.Set_Player_and_Save_Data_PreFabs();
        SceneType = Define.Scene.Rudencian_Rookiss_Room;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;


        Managers.Sound.Clear();
        

        GameObject player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        player.transform.position = new Vector3(4.344155f, 0, 7.816045f);
        player.transform.rotation = new Quaternion(0, 180f, 0, 0);
        
        PlayerController pc = player.gameObject.GetComponent<PlayerController>(); 
        pc.State = Define.State.Idle; // Scene ��ȯ �ǰ��� ��� �����̴� ���� �����մϴ�.


        gameObject.GetAddComponent<CursorController>();


        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Weapon_oneHand"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Weapon_TwoHand"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Head"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][1]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][1]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Consumable"][3]);


        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Etcs"][0]);



        StartCoroutine(First_Start_Game_Guide_Quest());

        gameManager.selection.gameObject.SetActive(true);
        Managers.Game.GetPlayer().gameObject.name = "UnityChan";
    }

    /// <summary>
    /// �絧�þȿ� ó�� �������� ��, ����Ʈ �ȳ��� ���ִ� �޼��� �Դϴ�.
    /// </summary>
    /// <returns></returns>
    private IEnumerator First_Start_Game_Guide_Quest()
    {
        Managers.Sound.Play("Quest Start DRUMS BIG", Define.Sound.Effect);
        GameObject go = Managers.Resources.Instantiate("Chapter0_Start");
        Destroy(go, 5.0f);

        yield return new WaitForSeconds(8f);

        Managers.Sound.Play("������", Define.Sound.Bgm);
        

        yield return new WaitForSeconds(2f);

        Managers.Sound.Play("Main_Quest_Start", Define.Sound.Effect);
        GameObject main_quest_alarm = Managers.Resources.Instantiate("MainQuest_Start");

        yield return new WaitForSeconds(1.5f);

        Player_First_monologue();

       
        yield return new WaitForSeconds(3.0f);

        Managers.Sound.Play("Main_Quest_Start", Define.Sound.Effect);
        GameObject player_start_alarm = Managers.Resources.Instantiate("Player_Start_Alarm");
        Destroy(player_start_alarm, 5.0f);
        _rookiss_terrain.gameObject.layer = TERRAIN_LAYER_GROUND; // �÷��̾ �̵� �� ���ֵ��� ������ ���� �մϴ�.


    }

    private void Player_First_monologue()
    {
        Managers.Game.GetPlayer().gameObject.name = "��ũ";
        gameManager.SelectedNPC = Managers.Game.GetPlayer().gameObject;
        gameManager.TalkAction();
        gameManager.selection.gameObject.SetActive(false);
         
        // Test(); => ���ʷ��Ͼ� �̵� �޼���
    }

    public override void Clear()
    {

    }



    public void Test()
    {
        GameObject player = Managers.Game.GetPlayer();
        player.transform.position = new Vector3(359.3f, 9.0f, 424.1f);
        player.transform.rotation = new Quaternion(0, 180f, 0, 0);

        SceneManager.LoadScene(14);

    }
}
