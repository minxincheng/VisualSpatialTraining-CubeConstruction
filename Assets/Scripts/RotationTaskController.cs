using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class RotationTaskController : MonoBehaviour
{
    public static RotationTaskController Instance { get; private set; }
    
    DataHandler dataHandler;
    GlobalControl globalControl;
    OptionList optionList;
    MatrixList matrixList;
    SnapController3 snapControl;
    ButtonController buttonControl;

    private GameObject targetObject;
    private GameObject display1, display2, display3;

    public int submitting, checking, cubenumber; 
    public string dragging, placing;

    private void Awake(){

        Instance = this;
        targetObject = GameObject.Find("FinalObject");
        targetObject.transform.position = new Vector3(-100, 0, 0);

        optionList = GetComponent<OptionList>();
        matrixList = GetComponent<MatrixList>();

        optionList.Initialize();
        matrixList.Initialize();

        //DontDestroyOnLoad(GlobalControl.Instance.finalObject);

    }
    

    public void Start(){
        
        globalControl = GlobalControl.Instance;
        snapControl = GetComponent<SnapController3>();
        dataHandler = GetComponent<DataHandler>();
        buttonControl = GetComponent<ButtonController>();

        Initialize();

        if (GlobalControl.Instance.handOption == 0)
        {
            display1 = Instantiate(targetObject, new Vector3(-0.34f, 0.15f, 0f), Quaternion.Euler(0, -45, 0));
            display2 = Instantiate(targetObject, new Vector3(-0.34f, 0.025f, 0f), Quaternion.Euler(0, 45, 0));
            display3 = Instantiate(targetObject, new Vector3(-0.34f, -0.1f, 0f), Quaternion.Euler(-45, 0, 0));
        }
        else
        {
            display1 = Instantiate(targetObject, new Vector3(0.34f, 0.15f, 0f), Quaternion.Euler(0, -45, 0));
            display2 = Instantiate(targetObject, new Vector3(0.34f, 0.025f, 0f), Quaternion.Euler(0, 45, 0));
            display3 = Instantiate(targetObject, new Vector3(0.34f, -0.1f, 0f), Quaternion.Euler(-45, 0, 0));

        }
   
        display1.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        for(int i = 0; i < display1.transform.childCount; i++){
            
            display1.transform.GetChild(i).tag = "Untagged";
            display1.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
            
        }
 
        display2.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        for (int i = 0; i < display2.transform.childCount; i++){
        
            display2.transform.GetChild(i).tag = "Untagged";
            display2.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
        
        }

        display3.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        for (int i = 0; i < display3.transform.childCount; i++){
            
            display3.transform.GetChild(i).tag = "Untagged";
            display3.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
            
        }

    }

    public void Initialize(){

        dataHandler.dataWritten = false;

	if(globalControl.recordingData){
		
		StartRecording();

	}

	Debug.Log("Initialized");

    }

    public void Update(){

        GatherContinuousData();
        
        checking = buttonControl.checking;
        submitting = buttonControl.submitting;
        cubenumber = snapControl.cubenumber;
        dragging = GameObject.FindWithTag("Current") != null ? GameObject.FindWithTag("Current").name : "";
        placing = snapControl.takenCube != null ? snapControl.takenCube : "";

    }
    
    public void StartRecording(){
        
        List<string> cubes = new List<string>();
        for(int i = 0; i < targetObject.transform.childCount; i++){
            
            cubes.Add(targetObject.transform.GetChild(i).name);
            
        }
        
        string targetobjects = String.Join(",", cubes);
        
        dataHandler.recordHeaderInfo(GlobalControl.Instance.hand.ToString(), targetobjects);
        
    }

    public void GatherContinuousData(){

	if(globalControl.recordingData){

        dataHandler.recordContinuous(Time.time, submitting, checking, cubenumber, dragging, placing);

	}

    }

    private void OnApplicationQuit(){

        QuitTask();

    }

    public void QuitTask(){

        dataHandler.WriteDataToFiles();
        SceneManager.LoadScene("Empty");

    }


}
