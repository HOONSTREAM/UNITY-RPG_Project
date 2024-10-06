using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Scene : MonoBehaviour
{
    public TextMeshProUGUI textBox;        // UI �ؽ�Ʈ ������Ʈ�� �Ҵ���� ����
    
    public string firstText;    // ù ��° ��ü �ؽ�Ʈ ����
    public string secondText;   // �� ��° ��ü �ؽ�Ʈ ����
    public float typeSpeed = 0.1f;  // ���ڰ� ǥ�õǴ� �ӵ�
    public float fadeTime = 1.0f;   // ���̵� ��/�ƿ� �ð�
    private string currentText = ""; // ������� ǥ�õ� �ؽ�Ʈ
    public GameObject BackGround_Object;
    public Image backgroundImage;  // ��� �̹��� ������Ʈ

    void Start()
    {
        backgroundImage = BackGround_Object.GetComponent<Image>();
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.RooKissRoomScene;
        StartCoroutine(TypeAndSwitchScene());
    }

    IEnumerator TypeAndSwitchScene()
    {
        // ù ��° �ؽ�Ʈ Ÿ����
        yield return StartCoroutine(TypeText(firstText));

        // ��� �̹��� ���̵� ��
        yield return StartCoroutine(FadeImage(backgroundImage, true, fadeTime));
        Managers.Sound.Play("Dragon_Fire", Define.Sound.Effect);
        // ��� ���
        yield return new WaitForSeconds(5.0f);

        // ��� �̹��� ���̵� �ƿ�
        yield return StartCoroutine(FadeImage(backgroundImage, false, fadeTime));

        // ����� ���������� ����
        backgroundImage.color = Color.black;

        // �� ��° �ؽ�Ʈ Ÿ����
        yield return StartCoroutine(TypeText(secondText));

        // 5�� �� �� ��ȯ
        yield return new WaitForSeconds(2);
        LoadNextScene();
    }

    IEnumerator TypeText(string text)
    {
        textBox.text = ""; // �ؽ�Ʈ �ڽ� �ʱ�ȭ
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
        SceneManager.LoadScene(Managers.Scene_Number.LoadingScene); // ���� �� �ε�
    }
}
