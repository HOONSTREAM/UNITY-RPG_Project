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
        Managers.Sound.Play("Calm3 - Peaceful Days", Define.Sound.Bgm);

        GameObject player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        player.transform.position = new Vector3(353.35f, 1.81f, 308.36f);
 
        // Scene 전환 되고나서 계속 움직이는 현상 방지
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        pc.State = Define.State.Idle;


        gameObject.GetAddComponent<CursorController>();

    }



    public override void Clear()
    {
       
    }

   
}
