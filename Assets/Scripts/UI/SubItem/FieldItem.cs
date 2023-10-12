using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


// �ʵ������ ���� ��ũ��Ʈ 
public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    
    public void SetItem(Item _item)
    {
        item.ItemID = _item.ItemID; //������ �ڵ� ���̵�
        item.itemname = _item.itemname; //������ �̸�
        item.stat_1 = _item.stat_1; // ATK
        item.stat_2 = _item.stat_2; // DEF
        item.Description = _item.Description; // ������ ����
        item.num_1 = _item.num_1; // ATK ��ġ
        item.num_2 = _item.num_2; // DEF ��ġ
        item.itemImage = _item.itemImage; // ������ ��������Ʈ �̹���
        item.equiptype = _item.equiptype; // �������� �������� 
        item.itemtype = _item.itemtype; // ������ Ÿ�� (���,�Һ�,��Ÿ)
        item.efts = _item.efts; // ������ ��� �� �߻��ϴ� ����Ʈ
        item.Equip = _item.Equip; // ������ �������� ����
        image.sprite = _item.itemImage; //������ ����� �ʵ��̹���
        item.amount = _item.amount;  //������ ���� (�Һ�,��Ÿ�縸)
        
    }

    public Item GetItem()
    {

        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
