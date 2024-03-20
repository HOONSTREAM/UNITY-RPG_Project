
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
            SceneType = Define.Scene.Rudencian;
            Managers.Sound.Play("�絧�þ�", Define.Sound.Bgm);
            Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
            gameObject.GetAddComponent<CursorController>();
            
      
            return;
        }

        base.Init();


        SceneType = Define.Scene.Rudencian;
        
        Managers.Resources.Instantiate("Save_Data"); //Save_Data ������Ʈ ���� ����      
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;      
        Managers.Sound.Play("�絧�þ�", Define.Sound.Bgm);
        Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
       
        gameObject.GetAddComponent<CursorController>(); //Ŀ����Ʈ�ѷ� ��ũ��Ʈ�� �ڵ������ AddComponent�ϰ�, Load �Լ��� ���� ���

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);


    }

    public override void Clear()
    {
        
    }

}
