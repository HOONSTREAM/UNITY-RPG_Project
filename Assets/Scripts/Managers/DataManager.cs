using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager 
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>(); // 데이터 추가 
    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict(); //데이터 추가
  
    }

    Loader LoadJson<Loader,Key,Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textasset = Managers.Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textasset.text); //Json 파일 Pasing 하는방법 
    }
}
