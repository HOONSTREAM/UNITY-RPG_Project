using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject //추상클래스
{
    public abstract bool ExecuteRole(ItemType itemtype);
    public abstract int GetAtk();

 
    
}
