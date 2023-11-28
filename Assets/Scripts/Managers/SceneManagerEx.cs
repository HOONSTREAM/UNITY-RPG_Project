using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx 
{

    public BaseScene CurrentScene
    { get { return GameObject.FindObjectOfType<BaseScene>(); } } //BaseScene 컴포넌트를 들고있는 object를 찾기. }

  public void LoadScene(Define.Scene type)
    {

        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));


    }

    string GetSceneName(Define.Scene type)
    {
       string name = System.Enum.GetName(typeof(Define.Scene), type);

        return name;
    }


    public void Clear()
    {
        CurrentScene.Clear(); //현재 맵을 클리어하고 다음 맵(씬)으로 넘어가게 하기위함.
    }


}
