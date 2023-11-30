using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 전체 아이템을 저장하고있을 데이터베이스 
public class ItemDataBase : MonoBehaviour
{
    //TODO 아이템을 딕셔너리로 int(아이디),value = Item 으로 확장, 검색하기 편하도록...

   public static ItemDataBase instance; 

   public List<Item> itemDB = new List<Item>();

   public List<Item> itemBackupDatabase = new List<Item> ();
    
    private void Awake()
    {
        instance = this;
        itemBackupDatabase = itemDB;
    }
 
}
