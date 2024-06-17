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

        //TODO ���� �� �̸����� �ʿ� 
        Text text = GetGameobject((int)GameObjects.npc_name).gameObject.GetComponent<Text>();

        npc_name_setting(text);

    }
    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y); //�ݶ��̴� ���̸�ŭ 
        transform.rotation = Camera.main.transform.rotation; //������ 
    }

    private void npc_name_setting(Text text)
    {
        switch (_obj_data.gameObject.name)
        {
            case "��� ����":
                text.text = "��� ����";
                break;
            case "���ð� ��Ű��":
                text.text = "���ð� ��Ű��";
                break;
            case "������ ����":
                text.text = "������ ����";
                break;
            case "��ξ�":
                text.text = "��ξ�";
                break;
            case "�ɳ�":
                text.text = "�ɳ�";
                break;
            case "�������� ��":
                text.text = "�������� ��";
                break;
            case "����� ���":
                text.text = "����� ���";
                break;
            case "���� ����":
                text.text = "���� ����";
                break;

        }
    }
}
