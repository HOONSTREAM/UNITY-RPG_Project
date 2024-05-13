
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
            SceneType = Define.Scene.Rudencian;
            Managers.Sound.Play("루덴시안", Define.Sound.Bgm);
            Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
            gameObject.GetAddComponent<CursorController>();
            
      
            return;
        }

        base.Init();


        SceneType = Define.Scene.Rudencian;
        
        Managers.Resources.Instantiate("Save_Data"); //Save_Data 오브젝트 새로 생성      
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;      
       
       
        gameObject.GetAddComponent<CursorController>(); //커서컨트롤러 스크립트를 코드상으로 AddComponent하고, Load 함수를 통해 등록

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);


        //TEST CODE
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Weapon_oneHand"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][0]);


        StartCoroutine(WaitAndExecute());
       
    }

    private IEnumerator WaitAndExecute()
    {
        Managers.Sound.Play("Quest Start DRUMS BIG", Define.Sound.Effect);
        GameObject go = Managers.Resources.Instantiate("Chapter0_Start");
        Destroy(go, 5.0f);

        yield return new WaitForSeconds(8f);

        Managers.Sound.Play("루덴시안", Define.Sound.Bgm);
        Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);

        yield return new WaitForSeconds(2f);

        Managers.Sound.Play("Main_Quest_Start", Define.Sound.Effect);
        GameObject main_quest_alarm = Managers.Resources.Instantiate("MainQuest_Start");
        Destroy(go, 5.0f);
        
    }
    public override void Clear()
    {
        
    }

}
