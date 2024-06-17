using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Data : MonoBehaviour
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public bool IsNPC;

    private readonly int NPC_LAYER_NUMBER = 12;

    private void Start()
    {
        if(gameObject.layer == NPC_LAYER_NUMBER)
        {
            if (gameObject.GetComponentInChildren<UI_NPC_name>() == null)
            {
                Managers.UI.MakeWorldSpaceUI<UI_NPC_name>(transform);

            }
        }
    }

}
