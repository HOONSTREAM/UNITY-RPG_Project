using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Scene 전환 되고나서 계속 움직이는 현상 방지
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        pc.State = Define.State.Idle;


        gameObject.GetAddComponent<CursorController>();


        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Weapon_oneHand"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][1]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][1]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Consumable"][3]);

        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[4]);

        StartCoroutine(First_Start_Game_Guide_Quest());

    }

    public override void Clear()
    {

    }

    /// <summary>
    /// 루덴시안에 처음 입장했을 때, 퀘스트 안내를 해주는 메서드 입니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator First_Start_Game_Guide_Quest()
    {
        Managers.Sound.Play("Quest Start DRUMS BIG", Define.Sound.Effect);
        GameObject go = Managers.Resources.Instantiate("Chapter0_Start");
        Destroy(go, 5.0f);

        yield return new WaitForSeconds(8f);

        Managers.Sound.Play("오르골", Define.Sound.Bgm);
        

        yield return new WaitForSeconds(2f);

        Managers.Sound.Play("Main_Quest_Start", Define.Sound.Effect);
        GameObject main_quest_alarm = Managers.Resources.Instantiate("MainQuest_Start");

        yield return new WaitForSeconds(1.5f);

        gameManager.SelectedNPC = Managers.Game.GetPlayer().gameObject;
        gameManager.TalkAction();
        _rookiss_terrain.gameObject.layer = TERRAIN_LAYER_GROUND;

        Destroy(go, 5.0f);

    }
}
