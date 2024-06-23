using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_Effecter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private Color originalColor;
    private bool isFlickering = false;

    public float flickerDuration = 0.5f; // 효과 지속 시간

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (!isFlickering)
        {
            StartCoroutine(FlickerEffect());
        }

        Managers.Sound.Play("Title_Button_OnPointerEnter_Sound", Define.Sound.Effect);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(FlickerEffect());
        buttonImage.color = originalColor;
        isFlickering = false;
    }

    private IEnumerator FlickerEffect()
    {
        isFlickering = true;
        float elapsedTime = 0f;
        while (isFlickering)
        {
            elapsedTime = 0f;
            while (elapsedTime < flickerDuration)
            {
                buttonImage.color = Color.Lerp(originalColor, Color.red, elapsedTime / flickerDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < flickerDuration)
            {
                buttonImage.color = Color.Lerp(Color.red, originalColor, elapsedTime / flickerDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}


