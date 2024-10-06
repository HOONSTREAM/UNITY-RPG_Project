using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(�Ҹ�ǰ)/���ʷ��Ͼ� ��ȯ��")]
public class Epilenia_Port_Paper : ItemEffect
{

    public override bool ExecuteRole(ItemType itemtype)
    {

        Print_Info_Text.Instance.PrintUserText("������ ��ȯ�մϴ�.");

        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.EpileniaMainScene;
        SceneManager.LoadScene(Managers.Scene_Number.LoadingScene);

        Managers.Game.GetPlayer().gameObject.transform.position = new Vector3(359.5609f, 9.1f, 422f);


        return true;
    }

}
