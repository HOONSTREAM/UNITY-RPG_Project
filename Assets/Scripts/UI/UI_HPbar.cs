using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPbar : MonoBehaviour
{
    //UI ��ܿ� ���ִ� HPBAR�� 


    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Slider _silder;
    public TextMeshProUGUI hp_text;
    private Stat _stat;

    void Start()
    {
        _player = Managers.Game.GetPlayer();
        _silder = GameObject.Find("HPBar").gameObject.GetComponent<Slider>();

        _stat = _player.GetComponent<Stat>();

    }

    void Update()
    {
        _silder.value = _stat.Hp / (float) _stat.MAXHP;
        hp_text.text = _stat.Hp.ToString();
    }
}
