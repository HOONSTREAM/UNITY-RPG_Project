using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class King_Slime_HP : UI_Scene
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

        _stat = GameObject.Find("King_Slime").gameObject.GetComponent<Stat>();

        //TODO ���� �� �̸����� �ʿ� 
        Text text = GetGameobject((int)GameObjects.Monster_name).gameObject.GetComponent<Text>();

        switch (_stat.gameObject.name)
        {
            
            case "King_Slime":
                text.text = "[����] ŷ������";
                break;

        }


    }

    private void Update()
    {
        float ratio = _stat.Hp / (float)_stat.MAXHP;

        SetHPRatio(ratio);


        if (ratio <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetHPRatio(float ratio)
    {

        Get<GameObject>((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;

    }

}
