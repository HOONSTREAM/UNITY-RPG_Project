using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ü �������� �����ϰ����� �����ͺ��̽� 
public class ItemDataBase : MonoBehaviour
{
    //TODO �������� ��ųʸ��� int(���̵�),value = Item ���� Ȯ�� 

   public static ItemDataBase instance;

  
    public List<Item> itemDB = new List<Item>();
    public GameObject fielditemPrefab;
    public Vector3 pos;
    

    private void Awake()
    {
        instance = this;
    }

    public GameObject DropFieldItem() //�ʵ忡 ������ ����
    {      
            GameObject go = Instantiate(fielditemPrefab, pos, Quaternion.identity); //Quaternion.identity�� ȸ�������� ��Ÿ���� ���ʹϾ�
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 4)]);
            

        return go;
           
    }
}
