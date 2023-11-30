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
    public List<Item> itemDB;

    private void Start()
    {
        itemDB = ItemDataBase.instance.itemDB;
    }
    public void SetItem(Item _item)
    {
        item.ItemID = _item.ItemID; //������ �ڵ� ���̵�
        item.itemname = _item.itemname; //������ �̸�
        item.stat_1 = _item.stat_1; // ���� : ATK , �� : DEF
        item.stat_2 = _item.stat_2; // ��ġ����
        item.Description = _item.Description; // ������ ����
        item.num_1 = _item.num_1; // ���� : ATK , �� DEF
        item.num_2 = _item.num_2; // ��ġ����
        item.itemImage = _item.itemImage; // ������ ��������Ʈ �̹���
        item.equiptype = _item.equiptype; // �������� �������� 
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
        go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 10)]);

        return go;

    }

}
