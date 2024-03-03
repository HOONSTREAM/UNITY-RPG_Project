using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudencian_South_Scene : BaseScene
{

    public GameObject Rudencia_NPC_Folder;



    protected override void Init()
    {
        base.Init();
      
        SceneType = Define.Scene.Rudencian_South;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Managers.Sound.Clear();
        Managers.Sound.Play("�絧�þȳ����ʵ�", Define.Sound.Bgm);

        GameObject player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        player.transform.position = new Vector3(-13.1455f, 0, -39.69f);
 
        // Scene ��ȯ �ǰ��� ��� �����̴� ���� ����
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        pc.State = Define.State.Idle;


        gameObject.GetAddComponent<CursorController>();


        //���� ����
        GameObject go = new GameObject { name = "Spawning Pool" };
        SpawningPool pool = go.GetAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(8);



    }



    public override void Clear()
    {
       
    }

   
}
