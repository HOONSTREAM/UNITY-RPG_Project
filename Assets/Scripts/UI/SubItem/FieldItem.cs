using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;


// �ʵ������ ���� ��ũ��Ʈ 
public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    public GameObject fielditemPrefab;
    public Vector3 pos;



    public void SetItem(Item _item)
    {
        item.ItemID = _item.ItemID; //������ �ڵ� ���̵�
        item.itemrank = _item.itemrank; // ������ ���
        item.itemname = _item.itemname; //������ �̸�
        item.stat_1 = _item.stat_1; // ���� : ATK , �� : DEF
        item.stat_2 = _item.stat_2; // STR/DEX
        item.stat_3 = _item.stat_3;
        item.stat_4 = _item.stat_4;
        item.Description = _item.Description; // ������ ����
        item.num_1 = _item.num_1; // ���� : ATK , �� DEF
        item.num_2 = _item.num_2; // STR/DEX
        item.num_3 = _item.num_3;
        item.num_4 = _item.num_4;
        item.itemImage = _item.itemImage; // ������ ��������Ʈ �̹���
        item.equiptype = _item.equiptype; // �������� �������� 
        item.weapontype = _item.weapontype; //������ Ÿ��
        item.itemtype = _item.itemtype; // ������ Ÿ�� (���,�Һ�,��Ÿ)
        item.efts = _item.efts; // ������ ��� �� �߻��ϴ� ����Ʈ
        item.Equip = _item.Equip; // ������ �������� ����
        image.sprite = _item.itemImage; //������ ����� �ʵ��̹���
        item.amount = _item.amount;  //������ ���� (�Һ�,��Ÿ�縸)
        item.buyprice = _item.buyprice; //���Ű���
        item.sellprice = _item.sellprice; //�ǸŰ���
        
        
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
        
    }

    public GameObject SlimeDropFieldItem() //�ʵ忡 ������ ����
    {
        GameObject go = Instantiate(fielditemPrefab, pos, Quaternion.identity); //Quaternion.identity�� ȸ�������� ��Ÿ���� ���ʹϾ�    
        go.GetComponent<FieldItem>().SetItem(ItemDataBase.instance.GetAllItems()["Etcs"][Random.Range(0,0)]);
       
        return go;

    }

    public GameObject PunchmanDropFieldItem() //�ʵ忡 ������ ����
    {
        GameObject go = Instantiate(fielditemPrefab, pos, Quaternion.identity); //Quaternion.identity�� ȸ�������� ��Ÿ���� ���ʹϾ�    
        go.GetComponent<FieldItem>().SetItem(ItemDataBase.instance.GetAllItems()["Etcs"][1]);

        return go;

    }


}
