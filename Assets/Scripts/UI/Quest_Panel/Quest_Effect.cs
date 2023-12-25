using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public abstract class Quest_Effect : ScriptableObject //추상클래스
    {
        public abstract bool ExecuteRole(QuestType questtype);

    }

