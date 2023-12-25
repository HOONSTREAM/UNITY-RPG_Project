using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    public static QuestDatabase instance;

    public List<Quest> QuestDB = new List<Quest>();


    private void Awake()
    {
        instance = this;
    }

}
