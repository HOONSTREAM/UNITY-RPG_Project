using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudencian_Deep_Place_Scene : BaseScene
{

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Rudencian_South2;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Managers.Sound.Clear();
        Managers.Sound.Play("���ʱ�����_BOSS", Define.Sound.Bgm);

        GameObject player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        player.transform.position = new Vector3(-1.524565f, 0, -35.0375f);

        // Scene ��ȯ �ǰ��� ��� �����̴� ���� ����
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        pc.State = Define.State.Idle;


        gameObject.GetAddComponent<CursorController>();


        //���� ����
        GameObject go = new GameObject { name = "Spawning Pool_South2" };
        Spawning_Pool_South2 pool = go.GetAddComponent<Spawning_Pool_South2>();
        pool.SetKeepMonsterCount(8);



    }



    public override void Clear()
    {

    }
}
