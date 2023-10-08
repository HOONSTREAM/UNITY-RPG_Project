using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance = new PlayerInventory();
    public List<Item> player_items = new List<Item>();

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    private void Awake()
    {
        Instance = this; 
    }
    public bool AddItem(Item _item)
    {
        if (player_items.Count < 20 )  //������ �߰��Ҷ� ���Ժ��� �������� ������ �߰�
        {           
            player_items.Add(_item);

            if(onChangeItem != null )
            {
                onChangeItem.Invoke();
                return true;
            }
  
        }
        return false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.CompareTag("FieldItem"))
        {
            
            FieldItem fielditems = collision.GetComponent<FieldItem>();
            
            if (AddItem(fielditems.GetItem()))
            {
               
                fielditems.DestroyItem();
                Managers.Sound.Play("Coin");

            }
            
        }
    }

    public void RemoveItem(int index)
    {
        player_items.RemoveAt(index);
        onChangeItem.Invoke();
        //TODO . ������ �����ϵ� ���� ���� üũǥ�� �����ǵ���,
    }

}
