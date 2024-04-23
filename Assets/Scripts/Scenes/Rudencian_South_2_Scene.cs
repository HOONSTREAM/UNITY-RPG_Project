using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudencian_South_2_Scene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Rudencian_South;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Managers.Sound.Clear();
        Managers.Sound.Play("루덴시안남쪽필드", Define.Sound.Bgm);

        GameObject player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        player.transform.position = new Vector3(-1.1499f, 0, -33.6f);

        // Scene 전환 되고나서 계속 움직이는 현상 방지
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        pc.State = Define.State.Idle;


        gameObject.GetAddComponent<CursorController>();


        //몬스터 생성
        GameObject go = new GameObject { name = "Spawning Pool_South2" };
        Spawning_Pool_South2 pool = go.GetAddComponent<Spawning_Pool_South2>();
        pool.SetKeepMonsterCount(8);



    }



    public override void Clear()
    {

    }
}
