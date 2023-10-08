using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 전체 아이템을 저장하고있을 데이터베이스 
public class ItemDataBase : MonoBehaviour
{
    //TODO 아이템을 딕셔너리로 int(아이디),value = Item 으로 확장 

   public static ItemDataBase instance;

  
    public List<Item> itemDB = new List<Item>();
    public GameObject fielditemPrefab;
    public Vector3 pos;
    

    private void Awake()
    {
        instance = this;
    }

    public GameObject DropFieldItem() //필드에 아이템 생성
    {      
            GameObject go = Instantiate(fielditemPrefab, pos, Quaternion.identity); //Quaternion.identity는 회전없음을 나타내는 쿼터니언
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 4)]);
            

        return go;
           
    }
}
