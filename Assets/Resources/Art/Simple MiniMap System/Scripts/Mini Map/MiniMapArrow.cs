using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MiniMapArrow : MonoBehaviour
{
    public static MiniMapArrow Instance;

    public bool showArrow = true;
    
    public Transform center;
    public Transform currentTarget;
    public GameObject arrow;
    
    void Awake()
    {
        Instance = this;
        
        arrow.GetComponent<SpriteRenderer>().enabled = showArrow;
    }
    
    void Update()
    {
        ProcessArrow();
    }

    public virtual void ProcessArrow()
    {
        if (currentTarget == null)
        {
            SetActive(false);
            return;
        }
        else if(currentTarget != null && !arrow.activeSelf)
        {
            SetActive(true);
        }
        
        Vector3 difference  = currentTarget.position - center.position;
        difference.y = 0.0f;     // Flatten the vector, assuming you're not concerned with indicating height difference
 
        transform.rotation = Quaternion.LookRotation(difference.normalized);
    }

    public virtual int GetDistanceToTarget()
    {
        return Mathf.RoundToInt( Vector3.Distance(currentTarget.position, center.position));
    }
    

    public virtual void SetTarget(Transform target)
    {
        this.currentTarget = target;
    }
    
    public virtual void SetTarget(int id)
    {
        if (MiniMapTargets.Instance == null)
        {
            Debug.Log("Please add a Minimap Targets Script to scene");
            return;
        }
        
        this.currentTarget =  MiniMapTargets.Instance.targets[id];
    }
    

    public virtual void SetActive(bool active)
    {
        arrow.SetActive(active);
    }
}
