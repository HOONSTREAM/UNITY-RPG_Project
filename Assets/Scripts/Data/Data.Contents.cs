using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable] //����������
    public class Stat //Json ���ϰ� �̸��� �ٸ��� ã���� ���ϹǷ� �̸��� �� �����. 
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
    [Serializable] //Ŭ���� �Ǵ� ����ü�� �ν����ͻ����� �����ų �� ����ϴ� Ű����.
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
            {
                dict.Add(stat.level, stat); //key(����),value 
            }

            return dict;
        }
    }
    #endregion

}

