using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinalObject : MonoBehaviour
{
    public static FinalObject carryOver;

    private void Awake(){

  	DontDestroyOnLoad(gameObject);

    }



}
