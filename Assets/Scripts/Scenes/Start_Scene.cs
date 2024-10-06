using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Scene : MonoBehaviour
{
    public TextMeshProUGUI textBox;        // UI 텍스트 컴포넌트를 할당받을 변수
    
    public string firstText;    // 첫 번째 전체 텍스트 내용
    public string secondText;   // 두 번째 전체 텍스트 내용
    public float typeSpeed = 0.1f;  // 글자가 표시되는 속도
    public float fadeTime = 1.0f;   // 페이드 인/아웃 시간
    private string currentText = ""; // 현재까지 표시된 텍스트
    public GameObject BackGround_Object;
    public Image backgroundImage;  // 배경 이미지 컴포넌트

    void Start()
    {
        backgroundImage = BackGround_Object.GetComponent<Image>();
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.RooKissRoomScene;
        StartCoroutine(TypeAndSwitchScene());
    }

    IEnumerator TypeAndSwitchScene()
    {
        // 첫 번째 텍스트 타이핑
        yield return StartCoroutine(TypeText(firstText));

        // 배경 이미지 페이드 인
        yield return StartCoroutine(FadeImage(backgroundImage, true, fadeTime));
        Managers.Sound.Play("Dragon_Fire", Define.Sound.Effect);
        // 잠시 대기
        yield return new WaitForSeconds(5.0f);

        // 배경 이미지 페이드 아웃
        yield return StartCoroutine(FadeImage(backgroundImage, false, fadeTime));

        // 배경을 검은색으로 설정
        backgroundImage.color = Color.black;

        // 두 번째 텍스트 타이핑
        yield return StartCoroutine(TypeText(secondText));

        // 5초 후 씬 전환
        yield return new WaitForSeconds(2);
        LoadNextScene();
    }

    IEnumerator TypeText(string text)
    {
        textBox.text = ""; // 텍스트 박스 초기화
        for (int i = 0; i <= text.Length; i++)
        {
            currentText = text.Substring(0, i);
            textBox.text = currentText;
            yield return new WaitForSeconds(typeSpeed);            
        }

        yield return new WaitForSeconds(3);
        textBox.text = "";
    }

    IEnumerator FadeImage(Image image, bool fadeIn, float time)
    {
        float alpha = fadeIn ? 0 : 1;
        float fadeEndValue = fadeIn ? 1 : 0;
        float fadeSpeed = Mathf.Abs(alpha - fadeEndValue) / time;

        while (!Mathf.Approximately(alpha, fadeEndValue))
        {
            alpha = Mathf.MoveTowards(alpha, fadeEndValue, fadeSpeed * Time.deltaTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(Managers.Scene_Number.LoadingScene); // 다음 씬 로드
    }
}
