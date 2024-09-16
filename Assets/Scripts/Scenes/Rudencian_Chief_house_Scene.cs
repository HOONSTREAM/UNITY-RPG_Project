using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudencian_Chief_house_Scene : BaseScene
{
    protected override void Init()
    {
        
        base.Init();

        SceneType = Define.Scene.Rudencian_chief_house;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;


        Managers.Sound.Clear();
        Managers.Sound.Play("Rudencian", Define.Sound.Bgm);

        GameObject player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        player.transform.position = new Vector3(2.3f, 0, 3.8f);
        player.transform.rotation = new Quaternion(0, 270f, 0, 0);
        // Scene 전환 되고나서 계속 움직이는 현상 방지
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        pc.State = Define.State.Idle;


        gameObject.GetAddComponent<CursorController>();
       

    }

    public override void Clear()
    {

    }

   
}
