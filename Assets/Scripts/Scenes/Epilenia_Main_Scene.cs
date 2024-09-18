using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epilenia_Main_Scene : BaseScene
{
    protected override void Init()
    {


        GameObject Init_player = Managers.Game.GetPlayer();

        if (Init_player != null) //���� ������ ���� �� ���̾ƴ϶�� Init�Լ��� ������.
        {

            Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(Init_player);
            SceneType = Define.Scene.Epilenia;
            Managers.Sound.Play("���ʷ��Ͼ�", Define.Sound.Bgm);
            Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
            gameObject.GetAddComponent<CursorController>();


            return;
        }

        base.Init();

        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(Init_player);
        Managers.Sound.Play("���ʷ��Ͼ�", Define.Sound.Bgm);
        Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
        gameObject.GetAddComponent<CursorController>(); //Ŀ����Ʈ�ѷ� ��ũ��Ʈ�� �ڵ������ AddComponent�ϰ�, Load �Լ��� ���� ���
        Managers.Game.Set_Player_and_Save_Data_PreFabs();

        
        SceneType = Define.Scene.Epilenia;


    }


    public override void Clear()
    {

    }
}
