using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epilenia_Main_Scene : BaseScene
{
    protected override void Init()
    {


        GameObject Init_player = Managers.Game.GetPlayer();

        if (Init_player != null) //새로 게임이 시작 된 것이아니라면 Init함수를 생략함.
        {

            Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(Init_player);
            SceneType = Define.Scene.Epilenia;
            Managers.Sound.Play("에필레니아", Define.Sound.Bgm);
            Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
            gameObject.GetAddComponent<CursorController>();


            return;
        }

        base.Init();

        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(Init_player);
        Managers.Sound.Play("에필레니아", Define.Sound.Bgm);
        Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
        gameObject.GetAddComponent<CursorController>(); //커서컨트롤러 스크립트를 코드상으로 AddComponent하고, Load 함수를 통해 등록
        Managers.Game.Set_Player_and_Save_Data_PreFabs();

        
        SceneType = Define.Scene.Epilenia;


    }


    public override void Clear()
    {

    }
}
