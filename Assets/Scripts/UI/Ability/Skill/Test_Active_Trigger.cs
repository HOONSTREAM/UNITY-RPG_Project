using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Active_Trigger : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        // Output to the console when the collider detects another object
        Debug.Log("Collision detected with: " + other.name);
    }



}
