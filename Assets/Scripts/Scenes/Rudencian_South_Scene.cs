using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudencian_South_Scene : BaseScene
{

    protected override void Init()
    {
        base.Init();
      
        SceneType = Define.Scene.Rudencian_South;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Managers.Sound.Clear();
        Managers.Sound.Play("Rudencian_monster_zone", Define.Sound.Bgm);

        GameObject player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        
 
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
