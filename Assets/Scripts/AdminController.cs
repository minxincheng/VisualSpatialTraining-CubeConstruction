using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminController : MonoBehaviour
{
    OptionList optionList;
    MatrixList matrixList;

    public GameObject finalObject;

    public void Awake(){
        
        optionList = GetComponent<OptionList>();
        matrixList = GetComponent<MatrixList>();

        optionList.Initialize();
        matrixList.Initialize();

    }
    
    public void Update(){
        
        //GlobalControl.Instance.finalObject = GameObject.Find("FinalObject");
        
        for(int i = 0; i < finalObject.transform.childCount; i++){
            
            GlobalControl.Instance.shape.Add(finalObject.transform.GetChild(i).name);
            
        }
        
    }

    public void StartGame(){
        
        SceneManager.LoadScene("RotationTask");
        
    }

}
