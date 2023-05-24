using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLeap : MonoBehaviour
{
    public bool test;
    
    void OnTriggerEnter(Collider other){
        
        test = true;
        other.transform.position = gameObject.transform.position;
        
    }
    
    void OnTriggerExit(){
        test = false;
    }
}
