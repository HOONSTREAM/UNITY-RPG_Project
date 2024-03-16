using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScene : MonoBehaviour
{
    public const int next_sceneNumber = 1;
    public Slider loadingbar;
    public Text loadingText;


    private void Start()
    {
        StartCoroutine(TransitionNextScene(next_sceneNumber));
    }

    IEnumerator TransitionNextScene(int number)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(number);

        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            loadingbar.value = ao.progress;
            loadingText.text = (ao.progress * 100f).ToString() + "%";

            if (ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            yield return null; // 다음 프레임이 될 때 까지 기다린다.
        }

    }
}
