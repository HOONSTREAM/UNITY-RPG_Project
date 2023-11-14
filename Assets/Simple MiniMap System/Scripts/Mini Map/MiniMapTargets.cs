using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTargets : MonoBehaviour
{
    public static MiniMapTargets Instance;

    public Transform[] targets;
    
    void Awake()
    {
        Instance = this;
    }

    public virtual Transform GetTarget(int id)
    {
        if (id >= targets.Length)
        {
            Debug.Log("Id longer than array length");
            return null;
        }
        
        return targets[id];
    }
}
