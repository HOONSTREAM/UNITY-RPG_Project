using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter_Fade_Inout : MonoBehaviour
{
    private Scene scene;
    public GameObject particle;
    public TextMeshProUGUI chpater_name;
    float time = 0f;
    float Fade_time = 5f;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        Fade();
    }
    public void Fade()
    {

        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        chpater_name.gameObject.SetActive(true);
        particle.gameObject.SetActive(true);
        time = 0f;
        Color alpha = chpater_name.color;
       
        while (alpha.a < 1f)
        {

            time += Time.deltaTime / Fade_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            chpater_name.color = alpha;
            yield return null;
        }

        time = 0f;
        yield return new WaitForSeconds(1f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / Fade_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            chpater_name.color = alpha;
            yield return null;
        }

        chpater_name.text = "";
        chpater_name.gameObject.SetActive(false);
        particle.gameObject.SetActive(false);
        yield return null;

    }
}
