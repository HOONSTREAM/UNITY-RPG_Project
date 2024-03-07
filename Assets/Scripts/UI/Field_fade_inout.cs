using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Field_fade_inout : MonoBehaviour
{
    private Scene scene;   
    public GameObject particle;
    public TextMeshProUGUI Field_name;
    float time = 0f;
    float Fade_time = 2f;

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
        Field_name.gameObject.SetActive(true);
        particle.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Field_name.color;
        Field_name.text = scene.name;
        while (alpha.a < 1f)
        {

            time += Time.deltaTime / Fade_time;
            alpha.a = Mathf.Lerp(0, 1, time);           
            Field_name.color = alpha;           
            yield return null;
        }
      
        time = 0f;
        yield return new WaitForSeconds(1f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / Fade_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Field_name.color = alpha;          
            yield return null;
        }

        Field_name.text = "";
        Field_name.gameObject.SetActive(false);
        particle.gameObject.SetActive(false);
        yield return null;

    }
}
