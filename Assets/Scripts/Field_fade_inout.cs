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
        Managers.Sound.Play("GUI_Sound/misc_sound");
        Field_name_panel.gameObject.SetActive(true);       
        time = 0f;
        Color alpha = Field_name_panel.color;
        while(alpha.a < 1f)
        {
            time += Time.deltaTime / Fade_time;
            alpha.a = Mathf.Lerp(0, 1, time);            
            Field_name_panel.color = alpha;           
            yield return null;
        }
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
        yield return null;

    }
}
