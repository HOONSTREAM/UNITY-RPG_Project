using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class GameScene : BaseScene
{  
    
    protected override void Init()
    {
        GameObject Init_player = Managers.Game.GetPlayer();

        if (Init_player != null) //새로 게임이 시작 된 것이아니라면 Init함수를 생략함.
        {
           
            Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(Init_player);
            SceneType = Define.Scene.Rudencia;
            Managers.Sound.Play("Calm2 - Childhood Friends", Define.Sound.Bgm);
            Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
            gameObject.GetAddComponent<CursorController>();
            GameObject gos = new GameObject { name = "Spawning Pool" };
            SpawningPool pools = gos.GetAddComponent<SpawningPool>();
            pools.SetKeepMonsterCount(2);
            return;
        }

        base.Init();


        SceneType = Define.Scene.Rudencia;

        Managers.Resources.Instantiate("Save_Data"); //Save_Data 오브젝트 새로 생성
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        GameObject.Find("NPC Folder_Rudencia_shop").gameObject.SetActive(false);
        Managers.Sound.Play("Calm2 - Childhood Friends", Define.Sound.Bgm);
        Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
       
        gameObject.GetAddComponent<CursorController>(); //커서컨트롤러 스크립트를 코드상으로 AddComponent하고, Load 함수를 통해 등록

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);

        //Managers.Game.Spawn(Define.WorldObject.Monster, "Slime");
        GameObject go = new GameObject { name = "Spawning Pool" };
        SpawningPool pool = go.GetAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(2);
    
    }

    public override void Clear()
    {
        
    }

}
