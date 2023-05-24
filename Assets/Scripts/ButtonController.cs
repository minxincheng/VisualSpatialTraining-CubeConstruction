using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    DataHandler dataHandler;
    
    public GameObject finalObject, desktopRig;
    public GameObject gotIt, check, submit, yes, no, sliderRotation, sliderPosition;
    public int checking = 0;
    public int submitting = 0;
    
    public void Start(){
        
        dataHandler = GetComponent<DataHandler>();

        desktopRig.transform.localPosition = new Vector3(-100, 0, 0);

        gotIt.SetActive(false);
        check.SetActive(true);
        yes.SetActive(false);
        no.SetActive(false);
        sliderRotation.SetActive(true);
        sliderPosition.SetActive(true);
    }
    
    public void Check(){
        
        gotIt.SetActive(true);
        check.SetActive(false);
        sliderRotation.SetActive(false);
        sliderPosition.SetActive(false);
        if (GlobalControl.Instance.handOption == 0)
        {
            desktopRig.transform.localPosition = new Vector3(-0.2174f, -0.246f, -0.1225f);
        }
        else
        {
            desktopRig.transform.localPosition = new Vector3(0.2174f, -0.246f, -0.1225f);

        }

        checking = 1;
        
    }
    
    public void GotIt(){

        if (GlobalControl.Instance.handOption == 0)
        {
            finalObject.transform.position = new Vector3(-0.22f, 0.003f, -0.017f);
        }
        else
        {
            finalObject.transform.position = new Vector3(0.22f, 0.003f, -0.017f);

        }

        finalObject.transform.rotation = Quaternion.Euler(0, 45, 0);
        
        gotIt.SetActive(false);
        check.SetActive(true);
        sliderRotation.SetActive(true);
        sliderPosition.SetActive(true);
        desktopRig.transform.localPosition = new Vector3(-100, 0, 0);

        checking = 0;
        
    }
    
    public void Submit(){
        
        submit.SetActive(false);
        yes.SetActive(true);
        no.SetActive(true);
        submitting = 1;
        
    }
    
    public void Yes(){
        
        dataHandler.WriteDataToFiles();
        SceneManager.LoadScene("Empty");
        Application.Quit();

    }
    
    public void No(){
        
        submit.SetActive(true);
        yes.SetActive(false);
        no.SetActive(false);
        submitting = 0;
        
    }
}
