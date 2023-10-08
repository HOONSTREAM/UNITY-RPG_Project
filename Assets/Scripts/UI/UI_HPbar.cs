using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPbar : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    [SerializeField]
    Slider _silder;

    Stat _stat;

    void Start()
    {
        _player = GameObject.Find("UnityChan").gameObject;
        _silder = GameObject.Find("HPBar").gameObject.GetComponent<Slider>();

        _stat = _player.GetComponent<Stat>();

    }

    void Update()
    {
        _silder.value = _stat.Hp / (float) _stat.MaxHp;
    }
}
