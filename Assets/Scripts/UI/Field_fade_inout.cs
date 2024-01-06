using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Field_fade_inout : MonoBehaviour
{
    private Scene scene;
    public Image Field_name_panel;
    public GameObject particle;
    public TextMeshProUGUI Field_name;
    float time = 0f;
    float Fade_time = 1f;

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
        
        Field_name_panel.gameObject.SetActive(true);
        particle.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Field_name_panel.color;
        while(alpha.a < 1f)
        {

            time += Time.deltaTime / Fade_time;
            alpha.a = Mathf.Lerp(0, 1, time);            
            Field_name_panel.color = alpha;           
            yield return null;
        }
        Managers.Sound.Play("change_scene");
        Field_name.text = scene.name;
        

        time = 0f;
        yield return new WaitForSeconds(1f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / Fade_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Field_name.text = "";
            Field_name_panel.color = alpha;
            yield return null;
        }

        Field_name_panel.gameObject.SetActive(false);
        particle.gameObject.SetActive(false);
        yield return null;

    }
}
