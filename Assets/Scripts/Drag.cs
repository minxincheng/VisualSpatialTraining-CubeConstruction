using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    SnapController3 snapControl;
    
    //the camera that emits radiation
    private Camera cam;
    //objects colliding with rays
    public GameObject currentOption;
    public GameObject[] otherOptions;
    
    private Vector3 screenSpace;
    public bool isDrage = false;
    
    void Start()
    {
        cam = Camera.main;
        
        //currentOption = null;
        
        snapControl = gameObject.GetComponent<SnapController3>();
        
    }

    void Update ()
    {
      if(Input.GetMouseButton(0))
      {
          
          MouseDown();
          
     }
      
      if(Input.GetMouseButtonUp(0)){
          
          MouseUp();

      }
      
     currentOption = GameObject.FindWithTag("Current") != null ? GameObject.FindWithTag("Current") : null;
     otherOptions = GameObject.FindGameObjectsWithTag("Options");

     if(currentOption != null)
     {
            foreach(GameObject option in otherOptions)
            {
                option.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else
        {
            foreach (GameObject option in otherOptions)
            {
                option.GetComponent<BoxCollider>().enabled = true;
            }

        }
      

    }
    
    public void MouseDown(){
        
        //overall initial position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //ray from camera to click coordinate
        RaycastHit hitInfo;
        
        if (isDrage == false)
        {
            
          if(Physics.Raycast (ray, out hitInfo))
          {
              
            //The scribed rays can only be seen in the scene view
            Debug.DrawLine(ray.origin, hitInfo.point);
            
            hitInfo.collider.gameObject.tag = hitInfo.collider.gameObject.tag == "Options"? "Current" : hitInfo.collider.gameObject.tag;
                
            if(currentOption != null){
                
                screenSpace = cam.WorldToScreenPoint(currentOption.transform.position);
                
            }
            
          }
          
          Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
          Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace);
          
          if (currentOption != null){
              
              currentOption.transform.position = new Vector3(currentPosition.x, currentPosition.y, -0.01f);

          }
          
          isDrage = true;
          
        }
        else{

            isDrage = false;
            
        }
    }
    
    public void MouseUp(){
        
          isDrage = false;
          
          if(currentOption != null){
              
              if(currentOption.name != snapControl.takenCube){
                  
                  snapControl.SendBack(currentOption);
                  
              }
              
          }

          
          //plateControl.dragging = false;
          
          //plateControl.Check();
          /*
          foreach(GameObject otherOption in otherOptions){
              
              otherOption.GetComponent<BoxCollider>().enabled = true;
              
          }
          */
          
    }

}
