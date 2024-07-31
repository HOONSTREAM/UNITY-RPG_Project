using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar :UI_Base
{
    enum GameObjects 
    { 
        HPBar,
        Monster_name,      
    }

    Stat _stat;
   
  
    public override void Init()
    {
       Bind<GameObject>(typeof(GameObjects)); 
     
       _stat = transform.parent.GetComponent<Stat>();

        //TODO 몬스터 별 이름세팅 필요 
       Text text = GetGameobject((int)GameObjects.Monster_name).gameObject.GetComponent<Text>();

        switch (_stat.gameObject.name)
        {
            case "Slime":
                text.text = "레드슬라임";
                break;
            case "Punch_man":
                text.text = "펀치맨";
                break;
            case "Turtle_Slime":
                text.text = "터틀슬라임";
                break;
            
            
        }
       
             
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y); //콜라이더 높이만큼 
        transform.rotation = Camera.main.transform.rotation; //빌보드 

        float ratio = _stat.Hp /(float)_stat.MAXHP;

        SetHPRatio(ratio);

    }

   public void SetHPRatio(float ratio)
   {
       
       Get<GameObject>((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;

   }

}
