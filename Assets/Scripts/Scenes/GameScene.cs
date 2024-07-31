
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
            Managers.Sound.Play("New_Rudencian_240731", Define.Sound.Bgm);
            Managers.Sound.Play("Nature Ambiance Sound", Define.Sound.Ambiance);
            gameObject.GetAddComponent<CursorController>();
            
      
            return;
        }

        base.Init();

        gameObject.GetAddComponent<CursorController>(); //Ŀ����Ʈ�ѷ� ��ũ��Ʈ�� �ڵ������ AddComponent�ϰ�, Load �Լ��� ���� ���
        Managers.Game.Set_Player_and_Save_Data_PreFabs();

        SceneType = Define.Scene.Rudencian;

       
            
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Weapon_oneHand"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][0]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][1]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["SkillBook"][1]);
        PlayerInventory.Instance.AddItem(ItemDataBase.instance.GetAllItems()["Consumable"][3]);

        PlayerAbility.Instance.AddSkill(SkillDataBase.instance.SkillDB[4]);



        StartCoroutine(WaitAndExecute());
       
    }
    
    private IEnumerator WaitAndExecute()
    {
        Managers.Sound.Play("Quest Start DRUMS BIG", Define.Sound.Effect);
        GameObject go = Managers.Resources.Instantiate("Chapter0_Start");
        Destroy(go, 5.0f);

        yield return new WaitForSeconds(8f);

        Managers.Sound.Play("New_Rudencian_240731", Define.Sound.Bgm);
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
