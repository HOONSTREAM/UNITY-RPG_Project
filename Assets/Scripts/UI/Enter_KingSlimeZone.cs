using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter_KingSlimeZone : MonoBehaviour
{

    [SerializeField]
    private GameObject King_Slime_Enter_Panel;
    [SerializeField]
    private GameObject King_Slime_CANVAS;
   public void Enter_KingSlime()
    {
        King_Slime_CANVAS = GameObject.Find("Enter_KingSlime_CANVAS").gameObject;
        King_Slime_Enter_Panel.gameObject.SetActive(false);
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Deep_Place_Scene();
        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
        Destroy(King_Slime_CANVAS);
    }

    public void DoNot_Enter_KingSlime()
    {
        King_Slime_Enter_Panel.gameObject.SetActive(false);
    }

}
