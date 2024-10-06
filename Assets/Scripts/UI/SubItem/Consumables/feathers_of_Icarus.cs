using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(소모품)/이카루스의깃털")]

public class feathers_of_Icarus : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {

        Print_Info_Text.Instance.PrintUserText("마을로 귀환합니다.");

        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.RudencianScene;
        SceneManager.LoadScene(Managers.Scene_Number.LoadingScene);

        Managers.Game.GetPlayer().gameObject.transform.position = new Vector3(-8.2f, 0, -13.22f);


        return true;
    }
}
