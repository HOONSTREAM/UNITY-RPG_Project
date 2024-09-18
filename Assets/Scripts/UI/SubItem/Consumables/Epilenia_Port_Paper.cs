using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(소모품)/에필레니아 귀환서")]
public class Epilenia_Port_Paper : ItemEffect
{

    public override bool ExecuteRole(ItemType itemtype)
    {

        Print_Info_Text.Instance.PrintUserText("마을로 귀환합니다.");

        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Epilenia_Main_Scene();
        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());

        Managers.Game.GetPlayer().gameObject.transform.position = new Vector3(359.5609f, 9.1f, 422f);


        return true;
    }

}
