using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    public InputField ID;
    public InputField Password;

    public Text notify;

    private void Start()
    {
        notify.text = "";
    }

    private bool CheckInput(string id, string password)
    {
        if (id == "" || password == "")
        {
            notify.text = "아이디 또는 패스워드를 입력하세요.";
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
        // 사용자의 아이디는 Key로 패스워드를 값(value)로 설정해서 저장함.


        if (!PlayerPrefs.HasKey(ID.text))
        {
            PlayerPrefs.SetString(ID.text, Password.text);
            notify.text = "계정생성이 완료되었습니다.";
            ID.text = "";
            Password.text = "";

        }

        else
        {
            notify.text = "이미 존재하는 계정입니다.";
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
            SceneManager.LoadScene(1);
        }

        else
        {
            notify.text = "입력하신 아이디와 패스워드가 불일치 합니다.";
            ID.text = "";
            Password.text = "";
        }
    }

}
