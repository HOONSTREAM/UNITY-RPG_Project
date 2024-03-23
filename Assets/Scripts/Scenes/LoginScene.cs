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

    public GameObject ID_enter_rejection;
    public GameObject ID_register_complete;
    public GameObject Not_enterd_ID_or_password;
    public GameObject ID_already_exist;


    private void Start()
    {
        GameObject Init_player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(Init_player);
        gameObject.GetAddComponent<CursorController>();

        ID_enter_rejection.gameObject.SetActive(false);
        ID_register_complete.gameObject.SetActive(false);
        Not_enterd_ID_or_password.gameObject.SetActive(false);
        ID_already_exist.gameObject.SetActive(false);

    }

    private bool CheckInput(string id, string password)
    {
        if (id == "" || password == "")
        {
            Not_enterd_ID_or_password.gameObject.SetActive(true);
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
            Managers.Sound.Play("GUI_Sound/load",Define.Sound.Effect);
            PlayerPrefs.SetString(ID.text, Password.text);
            ID_register_complete.gameObject.SetActive(true);
            ID.text = "";
            Password.text = "";

        }

        else
        {
            Managers.Sound.Play("GUI_Sound/misc_menu", Define.Sound.Effect);
            ID_already_exist.gameObject.SetActive(true);
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
            ID_enter_rejection.gameObject.SetActive(true);
            ID.text = "";
            Password.text = "";
        }
    }

    public void Verification_complete_button()
    {
        if (ID_enter_rejection.activeSelf)
        {
            ID_enter_rejection.gameObject.SetActive(false);

        }

        if (ID_register_complete.activeSelf)
        {
            ID_register_complete.gameObject.SetActive(false);

        }

        if (Not_enterd_ID_or_password.activeSelf)
        {
            Not_enterd_ID_or_password.gameObject.SetActive(false);

        }

        if (ID_already_exist.activeSelf)
        {
            ID_already_exist.gameObject.SetActive(false);

        }

        return;
    }

}
