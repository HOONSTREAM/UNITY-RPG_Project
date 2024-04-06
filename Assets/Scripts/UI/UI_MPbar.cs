using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MPbar : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Slider _silder;
    public TextMeshProUGUI mp_text;
    private Stat _stat;

    void Start()
    {
        _player = Managers.Game.GetPlayer();
        _silder = GameObject.Find("MPBar").gameObject.GetComponent<Slider>();

        _stat = _player.GetComponent<Stat>();

    }

    void Update()
    {
        _silder.value = _stat.Mp / (float)_stat.MaxMp;
        mp_text.text = _stat.Mp.ToString();
    }
}
