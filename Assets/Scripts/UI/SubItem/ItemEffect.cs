using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ItemEffect : ScriptableObject //�߻�Ŭ����
{
    public abstract bool ExecuteRole(ItemType itemtype);
       
}



