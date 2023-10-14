using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject ShopPanel; //RectTransform¿∏∑Œ «ÿµµ µ .

   
    void Start()
    {
        
    }

    void Update()
    {

    }


    public void Enter(GameObject player)
    {
       ShopPanel.SetActive(true);
    }
    
}
