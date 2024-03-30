using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Rudencian_Shop_Scene : BaseScene
{
    protected override void Init()
    {

        base.Init();

        SceneType = Define.Scene.Rudencian_shop;

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        Managers.Sound.Clear();
        Managers.Sound.Play("���� �����", Define.Sound.Bgm);
        
        GameObject player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(player);
        player.transform.position = new Vector3(-6.2817f, 0, 3.5255f);
        // Scene ��ȯ �ǰ��� ��� �����̴� ���� ����
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();
        pc.State = Define.State.Idle;


        gameObject.GetAddComponent<CursorController>(); 


    }

    public override void Clear()
    {

    }
}
