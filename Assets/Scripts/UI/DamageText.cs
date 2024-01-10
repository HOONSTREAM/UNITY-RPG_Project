using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float DestroyTime = 4f;
    public Vector3 offset = new Vector3(0, 0, 0);
    public Vector3 RandomizeIntensity = new Vector3(0, 0, 0);
    private Transform mainCam;

    private void Start() 
    {
        mainCam = Camera.main.transform;

        Destroy(gameObject, DestroyTime);
        transform.localPosition += offset;

        transform.localPosition += new Vector3(0, 0, 0);
            
            //new Vector3 (Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
           // Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
           // Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));

    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCam.rotation * Vector3.forward,
            mainCam.rotation * Vector3.up); //ºôº¸µå
    }


}
