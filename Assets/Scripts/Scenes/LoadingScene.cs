using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScene : MonoBehaviour
{
    public static int NEXT_SCENE_NUMBER = 0;

    [SerializeField]
    private Slider loadingbar;
    [SerializeField]
    private Text loadingText;

    

    private void Start()
    {
        if (loadingbar == null || loadingText == null)
        {
            Debug.LogError("LoadingBar or LoadingText is not set in the inspector.");
            return;
        }

        StartCoroutine(TransitionNextScene(NEXT_SCENE_NUMBER));
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
