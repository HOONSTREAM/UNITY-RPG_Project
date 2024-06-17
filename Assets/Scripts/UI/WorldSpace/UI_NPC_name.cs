using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class UI_NPC_name : UI_Base
{
    enum GameObjects
    {
        npc_name,
    }

    private Object_Data _obj_data;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        _obj_data = transform.parent.GetComponent<Object_Data>();

        //TODO 몬스터 별 이름세팅 필요 
        Text text = GetGameobject((int)GameObjects.npc_name).gameObject.GetComponent<Text>();

        npc_name_setting(text);

    }
    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y); //콜라이더 높이만큼 
        transform.rotation = Camera.main.transform.rotation; //빌보드 
    }

    private void npc_name_setting(Text text)
    {
        switch (_obj_data.gameObject.name)
        {
            case "기사 헬켄":
                text.text = "기사 헬켄";
                break;
            case "수련관 루키스":
                text.text = "수련관 루키스";
                break;
            case "보석상 케일":
                text.text = "보석상 케일";
                break;
            case "브로아":
                text.text = "브로아";
                break;
            case "케넨":
                text.text = "케넨";
                break;
            case "여관주인 헥센":
                text.text = "여관주인 헥센";
                break;
            case "은행원 살라":
                text.text = "은행원 살라";
                break;
            case "촌장 월터":
                text.text = "촌장 월터";
                break;

        }
    }
}
