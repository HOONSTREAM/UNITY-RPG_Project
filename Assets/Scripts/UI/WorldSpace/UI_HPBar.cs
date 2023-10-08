using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar :UI_Base
{
    enum GameObjects 
    { 
        HPBar,
        Monster_name

    }

    Stat _stat;

    public override void Init()
    {
       Bind<GameObject>(typeof(GameObjects));
     
       _stat = transform.parent.GetComponent<Stat>();

       Text text = GetGameobject((int)GameObjects.Monster_name).gameObject.GetComponent<Text>();
        text.text = "Lv 1. 슬라임";
        

    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y); //콜라이더 높이만큼 
        transform.rotation = Camera.main.transform.rotation; //빌보드 

        float ratio = _stat.Hp /(float)_stat.MaxHp;

        SetHPRatio(ratio);
    }

   public void SetHPRatio(float ratio)
   {
       
       Get<GameObject>((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;

   }
}
