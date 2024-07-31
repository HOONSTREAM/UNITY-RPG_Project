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

        //TODO ���� �� �̸����� �ʿ� 
       Text text = GetGameobject((int)GameObjects.Monster_name).gameObject.GetComponent<Text>();

        switch (_stat.gameObject.name)
        {
            case "Slime":
                text.text = "���彽����";
                break;
            case "Punch_man":
                text.text = "��ġ��";
                break;
            case "Turtle_Slime":
                text.text = "��Ʋ������";
                break;
            
            
        }
       
             
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y); //�ݶ��̴� ���̸�ŭ 
        transform.rotation = Camera.main.transform.rotation; //������ 

        float ratio = _stat.Hp /(float)_stat.MAXHP;

        SetHPRatio(ratio);

    }

   public void SetHPRatio(float ratio)
   {
       
       Get<GameObject>((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;

   }

}
