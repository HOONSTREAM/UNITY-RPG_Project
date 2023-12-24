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

        if (Init_player != null) //���� ������ ���� �� ���̾ƴ϶�� Init�Լ��� ������.
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

        Managers.Resources.Instantiate("Save_Data"); //Save_Data ������Ʈ ���� ����
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        GameObject.Find("NPC Folder_Rudencia_shop").gameObject.SetActive(false);
        Managers.Sound.Play("Calm2 - Childhood Friends", Define.Sound.Bgm);
        Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
       
        gameObject.GetAddComponent<CursorController>(); //Ŀ����Ʈ�ѷ� ��ũ��Ʈ�� �ڵ������ AddComponent�ϰ�, Load �Լ��� ���� ���

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
