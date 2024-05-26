using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trampoline: " + collision.transform.name + " has entered the trampoline!");
    }
    
}
