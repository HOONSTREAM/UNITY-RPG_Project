using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(�Ҹ�ǰ)/��ī�罺�Ǳ���")]

public class feathers_of_Icarus : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {

        Print_Info_Text.Instance.PrintUserText("������ ��ȯ�մϴ�.");

        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.RudencianScene;
        SceneManager.LoadScene(Managers.Scene_Number.LoadingScene);

        Managers.Game.GetPlayer().gameObject.transform.position = new Vector3(-8.2f, 0, -13.22f);


        return true;
    }
}
