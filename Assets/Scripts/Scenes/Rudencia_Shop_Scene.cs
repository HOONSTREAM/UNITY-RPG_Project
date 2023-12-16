using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudencia_Shop_Scene : BaseScene
{
    protected override void Init()
    {

        base.Init();

        SceneType = Define.Scene.Rudencia_shop;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Managers.Sound.Clear();
        Managers.Sound.Play("Calm3 - Peaceful Days", Define.Sound.Bgm);
        
        GameObject player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        player.transform.position = new Vector3(0, 0, 0);
        
        gameObject.GetAddComponent<CursorController>(); //커서컨트롤러 스크립트를 코드상으로 AddComponent하고, Load 함수를 통해 등록


    }

    public override void Clear()
    {

    }
}
