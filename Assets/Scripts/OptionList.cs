using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionList : MonoBehaviour
{
    public GameObject cube, greycube;
    public GameObject finalObject, matrix, sliderRotation, sliderPosition;
    // the original that should be moved back
    public float row, col;
    
    // float values for the options position and the relative shape position
    private float row1, row2;
    public float col1, col2, col3, col4, col5;

    public void Initialize(){      
        
        if(GlobalControl.Instance.handOption == 0)
        {

            // assign values to the options position
            col1 = -0.09f;
            col2 = 0.015f;
            col3 = 0.125f;
            col4 = 0.235f;
            col5 = 0.345f;

            row1 = 0.08f;
            row2 = -0.075f;

            finalObject.transform.position = new Vector3(-0.22f, 0.003f, -0.017f);
            matrix.transform.position = new Vector3(-0.22f, 0.003f, -0.017f);
            //sliderRotation.transform.localPosition = new Vector3(-514, -500, 0);
            //sliderPosition.transform.localPosition = new Vector3(-900, -260, 0);

            sliderRotation.transform.localPosition = new Vector3(-780, -480, 0);
            sliderPosition.transform.localPosition = new Vector3(-1040, -220, 0);
        }
        else
        {
            // assign values to the options position
            col1 = 0.09f;
            col2 = -0.015f;
            col3 = -0.125f;
            col4 = -0.235f;
            col5 = -0.345f;

            row1 = 0.08f;
            row2 = -0.075f;

            finalObject.transform.position = new Vector3(0.22f, 0.003f, -0.017f);
            matrix.transform.position = new Vector3(0.22f, 0.003f, -0.017f);
            sliderRotation.transform.localPosition = new Vector3(780, -480, 0);
            sliderPosition.transform.localPosition = new Vector3(1040, -220, 0);

        }

        // column 1
        GameObject r1c1 = Instantiate(cube, new Vector3(col1, row1, 0), Quaternion.Euler(0, 45, 0));
        r1c1.name = "r1c1";
        
        GameObject r2c1 = Instantiate(greycube, new Vector3(col1, row2, 0), Quaternion.Euler(0, 45, 0));
        r2c1.name = "r2c1";
        
        // column 2
        GameObject r1c2 = Instantiate(greycube, new Vector3(col2, row1, 0), Quaternion.Euler(0, 45, 0));
        r1c2.name = "r1c2";
        
        GameObject r2c2 = Instantiate(cube, new Vector3(col2, row2, 0), Quaternion.Euler(0, 45, 0));
        r2c2.name = "r2c2";
        
        // column 3
        GameObject r1c3 = Instantiate(cube, new Vector3(col3, row1, 0), Quaternion.Euler(0, 45, 0));
        r1c3.name = "r1c3";
        
        GameObject r2c3 = Instantiate(greycube, new Vector3(col3, row2, 0), Quaternion.Euler(0, 45, 0));
        r2c3.name = "r2c3";
        
        // column 4
        GameObject r1c4 = Instantiate(greycube, new Vector3(col4, row1, 0), Quaternion.Euler(0, 45, 0));
        r1c4.name = "r1c4";
        
        GameObject r2c4 = Instantiate(cube, new Vector3(col4, row2, 0), Quaternion.Euler(0, 45, 0));
        r2c4.name = "r2c4";
        
        // column 5
        GameObject r1c5 = Instantiate(cube, new Vector3(col5, row1, 0), Quaternion.Euler(0, 45, 0));
        r1c5.name = "r1c5";
        
        GameObject r2c5 = Instantiate(greycube, new Vector3(col5, row2, 0), Quaternion.Euler(0, 45, 0));
        r2c5.name = "r2c5";

        print("Initialization finished");
        
    }
    
    public void MoveBack(string name){

        if (GlobalControl.Instance.handOption == 0)
        {

            // assign values to the options position
            col1 = -0.09f;
            col2 = 0.015f;
            col3 = 0.125f;
            col4 = 0.235f;
            col5 = 0.345f;

            row1 = 0.08f;
            row2 = -0.075f;

        }
        else
        {
            // assign values to the options position
            col1 = 0.09f;
            col2 = -0.015f;
            col3 = -0.125f;
            col4 = -0.235f;
            col5 = -0.345f;

            row1 = 0.08f;
            row2 = -0.075f;

        }

        switch (name){
            
            case string n when name.Contains("c1"):
                col = col1;
                break;
            case string n when name.Contains("c2"):
                col = col2;
                break;
            case string n when name.Contains("c3"):
                col = col3;
                break;
            case string n when name.Contains("c4"):
                col = col4;
                break;
            case string n when name.Contains("c5"):
                col = col5;
                break;
        }
        
        switch(name){
            
            case string n when name.Contains("r1"):
                row = row1;
                break;
            case string n when name.Contains("r2"):
                row = row2;
                break;
            
        }
        
        

        
    }

}
