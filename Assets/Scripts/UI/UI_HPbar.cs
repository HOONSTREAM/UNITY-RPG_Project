using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPbar : MonoBehaviour
{
    //UI 상단에 떠있는 HPBAR임 


    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Slider _silder;

    private Stat _stat;

    void Start()
    {
        _player = Managers.Game.GetPlayer();
        _silder = GameObject.Find("HPBar").gameObject.GetComponent<Slider>();

        _stat = _player.GetComponent<Stat>();

    }

    void Update()
    {
        _silder.value = _stat.Hp / (float) _stat.MaxHp;
    }
}
