using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderData : MonoBehaviour
{
    
    public readonly string hand;
    public readonly string targetobject;
    
    public HeaderData(string hand, string targetobject){
        
        this.hand = hand;
        this.targetobject = targetobject;
        
    }
}
