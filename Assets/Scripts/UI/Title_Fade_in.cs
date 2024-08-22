using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_Fade_in : MonoBehaviour
{

    public GameObject Title;
    public TextMeshProUGUI Title_name;
    public TextMeshProUGUI Title_hangul_name;
    float time = 0f;
    float Fade_time = 2f;

    private void Start()
    {
        Title.gameObject.SetActive(false);
        Title_name.gameObject.SetActive(false);
        Fade();
    }
    public void Fade()
    {

        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        Title.gameObject.SetActive(true);
        Title_name.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Title_name.color;

        
        while (alpha.a < 1f)
        {

            time += Time.deltaTime / Fade_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Title.GetComponent<Image>().color = alpha;
            Title_name.color = alpha;
            Title_hangul_name.color = alpha;
            yield return null;
        }
        Managers.Sound.Play("로딩배경 효과음", Define.Sound.Effect);
        time = 0f;

        yield return new WaitForSeconds(1f);

       

    }
}
