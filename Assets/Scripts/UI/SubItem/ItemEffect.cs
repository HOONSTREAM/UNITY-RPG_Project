using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject //�߻�Ŭ����
{
    public abstract bool ExecuteRole(ItemType itemtype);
    public abstract int GetAtk();

 
    
}
