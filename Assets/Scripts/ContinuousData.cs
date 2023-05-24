using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousData : MonoBehaviour
{
    
    public readonly float time;
    public readonly int submitting;
    public readonly int checking;
    public readonly int cubenumber;
    public readonly string dragging;
    public readonly string placing;

    
    public ContinuousData(float time, int submitting, int checking, int cubenumber, string dragging, string placing){
        
        this.time = time;
        this.submitting = submitting;
        this.checking = checking;
        this.cubenumber = cubenumber;
        this.dragging = dragging;
        this.placing = placing;
        
    }
    
}
