using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable] //데이터포맷
    public class Stat
    {
        public int level; //Json 파일과 이름이 다르면 찾지를 못하므로 이름을 꼭 맞춘다. 
        public int maxHP;
        public int attack;
        public float totalexp;
        public int defense;
        public float movespeed;

    }
    [Serializable]
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

