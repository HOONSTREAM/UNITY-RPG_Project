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

        SceneType = Define.Scene.Game;
                                  
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Managers.Sound.Play("Magic Town", Define.Sound.Bgm);
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
