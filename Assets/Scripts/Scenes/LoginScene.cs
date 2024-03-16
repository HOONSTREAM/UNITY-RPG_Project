using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    private const int Loading_Scene_number = 0;

    public TMP_InputField ID;
    public TMP_InputField Password;
    public TextMeshProUGUI notify;

   
    private void Start()
    {
        notify.text = "";
    }

    private bool CheckInput(string id, string password)
    {
        if (id == "" || password == "")
        {
            notify.text = "���̵� �Ǵ� �н����带 �Է��ϼ���.";
            ID.text = "";
            Password.text = "";
            return false;
        }

        else
        {
            return true;
        }

    }
    public void SaveUserData()
    {
        if (!CheckInput(ID.text, Password.text))
        {
            return;
        }
        // ������� ���̵�� Key�� �н����带 ��(value)�� �����ؼ� ������.


        if (!PlayerPrefs.HasKey(ID.text))
        {
            Managers.Sound.Play("GUI_Sound/load",Define.Sound.Effect);
            PlayerPrefs.SetString(ID.text, Password.text);
            notify.text = "���������� �Ϸ�Ǿ����ϴ�.";
            ID.text = "";
            Password.text = "";

        }

        else
        {
            Managers.Sound.Play("GUI_Sound/misc_menu", Define.Sound.Effect);
            notify.text = "�̹� �����ϴ� �����Դϴ�.";
            ID.text = "";
            Password.text = "";
        }

    }

    public void CheckUserData()
    {

        if (!CheckInput(ID.text, Password.text))
        {
            return;
        }

        string pass = PlayerPrefs.GetString(ID.text);

        if (Password.text == pass)
        {            
            SceneManager.LoadScene(Loading_Scene_number);
        }

        else
        {
            Managers.Sound.Play("GUI_Sound/misc_menu", Define.Sound.Effect);
            notify.text = "�Է��Ͻ� ���̵�� �н����尡 ����ġ �մϴ�.";
            ID.text = "";
            Password.text = "";
        }
    }

}
