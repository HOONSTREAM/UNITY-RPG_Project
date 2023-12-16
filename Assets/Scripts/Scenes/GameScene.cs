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
       
        gameObject.GetAddComponent<CursorController>(); //Ŀ����Ʈ�ѷ� ��ũ��Ʈ�� �ڵ������ AddComponent�ϰ�, Load �Լ��� ���� ���


       GameObject Init_player = Managers.Game.GetPlayer();

       if(Init_player == null) //���� ������ ���� �� ���̶�� �÷��̾ ���� ������.
        {
            GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
            Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);

        }
        else // ������ �̹� ����ǰ� �ְ�, ����ȯ�� �̷���� ���̸� ĳ���͸� ���� �������� ����.
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
