using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable] //데이터포맷
    public class Stat //Json 파일과 이름이 다르면 찾지를 못하므로 이름을 꼭 맞춘다. 
    {
        public int level; 
        public int maxHP;
        public int attack;
        public float totalexp;
        public int defense;
        public float movespeed;
        public int STR;
        public int DEX;
        public int VIT;
        public int AGI;

    }
    [Serializable] //클래스 또는 구조체를 인스펙터상으로 노출시킬 때 사용하는 키워드.
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
            {
                dict.Add(stat.level, stat); //key(레벨),value 
            }

            return dict;
        }
    }
    #endregion

}

