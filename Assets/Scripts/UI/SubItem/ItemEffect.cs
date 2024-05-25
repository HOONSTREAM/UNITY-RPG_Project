using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ItemEffect : ScriptableObject //추상클래스
{
    public abstract bool ExecuteRole(ItemType itemtype);
       
}



