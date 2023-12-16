using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class GameScene : BaseScene
{  
    protected override void Init()
    {
       
        base.Init();

        SceneType = Define.Scene.Rudencia;
                                  
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Managers.Sound.Play("Calm2 - Childhood Friends", Define.Sound.Bgm);
        Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
       
        gameObject.GetAddComponent<CursorController>(); //커서컨트롤러 스크립트를 코드상으로 AddComponent하고, Load 함수를 통해 등록


       GameObject Init_player = Managers.Game.GetPlayer();

       if(Init_player == null) //새로 게임이 시작 된 것이라면 플레이어를 새로 생성함.
        {
            GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
            Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);

        }
        else // 게임은 이미 실행되고 있고, 씬전환이 이루어진 것이면 캐릭터를 새로 생성하지 않음.
        {
            GameObject player = Managers.Game.GetPlayer();
            Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        }

        //Managers.Game.Spawn(Define.WorldObject.Monster, "Slime");
        GameObject go = new GameObject { name = "Spawning Pool" };
        SpawningPool pool = go.GetAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(2);
    
    }

    public override void Clear()
    {
        
    }

}
