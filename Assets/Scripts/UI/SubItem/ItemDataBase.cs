using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ü �������� �����ϰ����� �����ͺ��̽� 
public class ItemDataBase : MonoBehaviour
{
    //TODO �������� ��ųʸ��� int(���̵�),value = Item ���� Ȯ��, �˻��ϱ� ���ϵ���...

   public static ItemDataBase instance; 

   public List<Item> itemDB = new List<Item>();

   public List<Item> itemBackupDatabase = new List<Item> ();
    
    private void Awake()
    {
        instance = this;
        itemBackupDatabase = itemDB;
    }
 
}
