
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class RudencianMainScene : BaseScene
{

    protected override void Init()
    {
      
        
        GameObject Init_player = Managers.Game.GetPlayer();

        if (Init_player != null) //새로 게임이 시작 된 것이아니라면 Init함수를 생략함.
        {

            Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(Init_player);
            SceneType = Define.Scene.Rudencian;
            Managers.Sound.Play("Rudencian", Define.Sound.Bgm);
            Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
            gameObject.GetAddComponent<CursorController>();
            
      
            return;
        }

        base.Init();

        gameObject.GetAddComponent<CursorController>(); //커서컨트롤러 스크립트를 코드상으로 AddComponent하고, Load 함수를 통해 등록
        Managers.Game.Set_Player_and_Save_Data_PreFabs();

        SceneType = Define.Scene.Rudencian;
      
       
    }
    
   
    public override void Clear()
    {
        
    }

}
