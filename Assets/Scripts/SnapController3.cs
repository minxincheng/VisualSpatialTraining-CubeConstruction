using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class SnapController3 : MonoBehaviour
{
    Drag dragFunction;
    OptionList optionList;
    
    public GameObject currentOption, finalObject, centerCube, matrix;
    private GameObject tempObject;
    
    public List<int> a = new List<int>();
    public List<int> b = new List<int>();
    public List<int> c = new List<int>();
    /*
    public List<float> x = new List<float>();
    public List<float> y = new List<float>();
    public List<float> z = new List<float>();
    */
    //public List<Vector3> pos = new List<Vector3>();
    
    public List<string> names = new List<string>();
    public List<GameObject> neighbours = new List<GameObject>();
    public List<float> distances = new List<float>();

    public int takedone = 100;
    public int cubenumber;
    public string takenCube;
    
    public Slider sliderRotation;
    public Slider sliderPosition;
    private float previousRotationValue;
    private float previousPositionValue;
    public int changing = 0;
    
    void Awake(){
        
        this.sliderRotation.onValueChanged.AddListener(this.OnSliderRotationChanged);
        this.previousRotationValue = this.sliderRotation.value;

        this.sliderPosition.onValueChanged.AddListener(this.OnSliderPositionChanged);
        this.previousPositionValue = this.sliderPosition.value;

    }
    
    public void Start(){
        
        dragFunction = gameObject.GetComponent<Drag>();
        optionList = gameObject.GetComponent<OptionList>();
        
        a.Add(5);
        b.Add(5);
        c.Add(5);
        
        cubenumber = 1;
        takedone = 100;

        names.Add("4-5-5");
        names.Add("6-5-5");
        names.Add("5-4-5");
        names.Add("5-6-5");
        names.Add("5-5-4");
        names.Add("5-5-6");

        neighbours.Add(GameObject.Find(names[0]));
        neighbours.Add(GameObject.Find(names[1]));
        neighbours.Add(GameObject.Find(names[2]));
        neighbours.Add(GameObject.Find(names[3]));
        neighbours.Add(GameObject.Find(names[4]));
        neighbours.Add(GameObject.Find(names[5]));
        
        distances.Add(100);
        distances.Add(100);
        distances.Add(100);
        distances.Add(100);
        distances.Add(100);
        distances.Add(100);

    }
    
    public void Update(){

        currentOption = GameObject.FindWithTag("Current");

        if(currentOption != null && !currentOption.name.Contains("-")){

                for (int i = 0; i < neighbours.Count; i++){
                
                distances[i] = Vector3.Distance(currentOption.transform.position, neighbours[i].transform.position);
                
            }

            GetTakenCube(distances);
            
        }

        if(currentOption != null)
        {
            Hint(distances);
        }
        else
        {
            for (int i = 0; i < neighbours.Count; i++)
            {

                neighbours[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
 
        }
        
        
        if(currentOption != null && currentOption.name.Contains("-")){
            
            RemoveNeighbours();
            
        }
        
        if(takedone != 100 && currentOption != null){
            
            Take(takedone);
            
        }

        //SelfCheck();

    }

    public void Hint(List<float> distances)
    {
        float mindistance = distances.Min();
        int idx = distances.IndexOf(mindistance);

        if (currentOption != null)
        {
            if (mindistance <= 0.06f)
            {
                neighbours[idx].gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                neighbours[idx].gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            neighbours[idx].gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    
    public int GetTakenCube(List<float> distances){
        
        takenCube = null;
        
        for(int i = 0; i < neighbours.Count; i ++){
            
            float value = float.PositiveInfinity;
            if(distances[i] < value){

                value = distances[i];

                if(value <= 0.05f){

                    neighbours[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
                    takenCube = names[i].ToString();
                    takedone = i;
                    Take(takedone);
                    UpdateListAdding();
                    }
                    
                }
                else{
                    
                    takenCube = null;
                    takedone = 100;
                }
                
            }
            
            return takedone;
            
    }
    
    public void Take(int takedone){

        if(currentOption != null){

            currentOption.transform.SetParent(finalObject.transform);
            currentOption.transform.position = neighbours[takedone].transform.position;
            currentOption.transform.rotation = centerCube.transform.rotation;
            currentOption.layer = LayerMask.NameToLayer("Final");

            tempObject = currentOption;
            currentOption.transform.tag = "Untagged";

        }

        Invoke("Reset", 0.5f);

        takedone = 100;
        
    }

    public void UpdateListAdding(){

        if(a.Count == cubenumber){
            
            a.Add(Convert.ToInt32(takenCube.Substring(0, 1)));
            b.Add(Convert.ToInt32(takenCube.Substring(2, 1)));
            c.Add(Convert.ToInt32(takenCube.Substring(4, 1)));
            
        }

        string temp1 = (a[cubenumber] - 1).ToString() + "-" + b[cubenumber].ToString() + "-" + c[cubenumber].ToString();
        string temp2 = (a[cubenumber] + 1).ToString() + "-" + b[cubenumber].ToString() + "-" + c[cubenumber].ToString();
        string temp3 = a[cubenumber].ToString() + "-" + b[cubenumber].ToString() + "-" + (c[cubenumber] - 1).ToString();
        string temp4 = a[cubenumber].ToString() + "-" + b[cubenumber].ToString() + "-" + (c[cubenumber] + 1).ToString();
        string temp5 = a[cubenumber].ToString() + "-" + (b[cubenumber] - 1).ToString() + "-" + c[cubenumber].ToString();
        string temp6 = a[cubenumber].ToString() + "-" + (b[cubenumber] + 1).ToString() + "-" + c[cubenumber].ToString();
        
        names.Remove(names[takedone]);
        if(!names.Contains(temp1) && a[cubenumber] > 0) names.Add(temp1);
        if(!names.Contains(temp2) && a[cubenumber] < 10) names.Add(temp2);
        if(!names.Contains(temp3) && c[cubenumber] > 0) names.Add(temp3);
        if(!names.Contains(temp4) && c[cubenumber] < 10) names.Add(temp4);
        if(!names.Contains(temp5) && b[cubenumber] > 0) names.Add(temp5);
        if(!names.Contains(temp6) && b[cubenumber] < 10) names.Add(temp6);
        if(names.Contains("5-5-5")) names.Remove("5-5-5");
        
        neighbours.Remove(neighbours[takedone]);
        if(!neighbours.Contains(GameObject.Find(temp1)) && a[cubenumber] > 0) neighbours.Add(GameObject.Find(temp1));
        if(!neighbours.Contains(GameObject.Find(temp2)) && a[cubenumber] < 10) neighbours.Add(GameObject.Find(temp2));
        if(!neighbours.Contains(GameObject.Find(temp3)) && c[cubenumber] > 0) neighbours.Add(GameObject.Find(temp3));
        if(!neighbours.Contains(GameObject.Find(temp4)) && c[cubenumber] < 10) neighbours.Add(GameObject.Find(temp4));
        if(!neighbours.Contains(GameObject.Find(temp5)) && b[cubenumber] > 0) neighbours.Add(GameObject.Find(temp5));
        if(!neighbours.Contains(GameObject.Find(temp6)) && b[cubenumber] < 10) neighbours.Add(GameObject.Find(temp6));
        if(neighbours.Contains(GameObject.Find("5-5-5"))) neighbours.Remove(GameObject.Find("5-5-5"));
        
        for(int i = distances.Count; i < names.Count; i++){
            
            distances.Add(100);
            
        }
        
        for(int i = 0; i < names.Count; i++){
            
            distances[i] = 100;
            
        }
        
        takedone = 100;
        cubenumber = finalObject.transform.childCount + 1;
        
    }

    public void RemoveNeighbours(){
        
        if(currentOption != null && currentOption.name.Contains("-")){
            
            for(int i = 0; i < a.Count; i++){
                
                string currenttaken = a[i].ToString() + "-" + b[i].ToString() + "-" + c[i].ToString();
                if(currentOption.name.Substring(5, 5) == currenttaken){
                    
                    string toremove1 = (a[i] - 1).ToString() + "-" + b[i].ToString() + "-" + c[i].ToString();
                    string toremove2 = (a[i] + 1).ToString() + "-" + b[i].ToString() + "-" + c[i].ToString();
                    string toremove3 = a[i].ToString() + "-" + b[i].ToString() + "-" + (c[i] - 1).ToString();
                    string toremove4 = a[i].ToString() + "-" + b[i].ToString() + "-" + (c[i] + 1).ToString();
                    string toremove5 = a[i].ToString() + "-" + (b[i] - 1).ToString() + "-" + c[i].ToString();
                    string toremove6 = a[i].ToString() + "-" + (b[i] + 1).ToString() + "-" + c[i].ToString();
                    
                    if(names.Contains(toremove1)) names.Remove(toremove1);
                    if(names.Contains(toremove2)) names.Remove(toremove2);
                    if(names.Contains(toremove3)) names.Remove(toremove3);
                    if(names.Contains(toremove4)) names.Remove(toremove4);
                    if(names.Contains(toremove5)) names.Remove(toremove5);
                    if(names.Contains(toremove6)) names.Remove(toremove6);
                    names.Add(currentOption.name.Substring(5, 5));
                    
                    if(neighbours.Contains(GameObject.Find(toremove1))) neighbours.Remove(GameObject.Find(toremove1));
                    if(neighbours.Contains(GameObject.Find(toremove2))) neighbours.Remove(GameObject.Find(toremove2));
                    if(neighbours.Contains(GameObject.Find(toremove3))) neighbours.Remove(GameObject.Find(toremove3));
                    if(neighbours.Contains(GameObject.Find(toremove4))) neighbours.Remove(GameObject.Find(toremove4));
                    if(neighbours.Contains(GameObject.Find(toremove5))) neighbours.Remove(GameObject.Find(toremove5));
                    if(neighbours.Contains(GameObject.Find(toremove6))) neighbours.Remove(GameObject.Find(toremove6));
                    neighbours.Add(GameObject.Find(currentOption.name.Substring(5, 5)));
                    
                    a.Remove(a[i]);
                    b.Remove(b[i]);
                    c.Remove(c[i]);

                }
                
            }
            
            for(int i = names.Count; i < distances.Count; i++){
                
                distances.Remove(distances[i]);
                
            }
            
            currentOption.transform.SetParent(null);
            cubenumber = finalObject.transform.childCount + 1;
            
        }
    }
    
    public void Reset(){
        
        if(tempObject != null){
            
            tempObject.tag = "Options";
            tempObject.name = tempObject.name + "-" + takenCube;

            takenCube = null;
            tempObject = null;
            
        }

    }
    
    public void SendBack(GameObject sendback){
        
        sendback = currentOption;
        
        sendback.transform.SetParent(null);
        sendback.transform.tag = "Options";
        sendback.gameObject.layer = LayerMask.NameToLayer("Default");

        optionList.MoveBack(sendback.name);
        sendback.transform.position = new Vector3(optionList.col, optionList.row, 0f);
        sendback.transform.rotation = Quaternion.Euler(0, 45, 0);

        sendback.name = sendback.name.Substring(0, 4);
        sendback = null;
        
    }
    /*
    public void SelfCheck()
    {
        for(int i = 1; i < taken.Count; i++)
        {
            string myname = taken[i];
            //GameObject myself = null;
            foreach(Transform child in finalObject.transform)
            {
                if (child.name.Contains(myname))
                {
                    myself = child.gameObject;
                }
            }
            //int mya = Convert.ToInt32(myname.Substring(5, 1));
            //int myb = Convert.ToInt32(myname.Substring(7, 1));
            //int myc = Convert.ToInt32(myname.Substring(9, 1));

            mya = Convert.ToInt32(myname.Substring(0, 1));
            myb = Convert.ToInt32(myname.Substring(2, 1));
            myc = Convert.ToInt32(myname.Substring(4, 1));

            // check if myself is still in the neighbours list
            if (names.Contains(myname)) names.Remove(myname);
            if (neighbours.Contains(GameObject.Find(myname))) neighbours.Remove(GameObject.Find(myname));

            // get all my neighbours information
            //List<string> myneighboursname = new List<string>();
            myneighboursname.Add((mya - 1).ToString() + "-" + myb.ToString() + "-" + myc.ToString());
            myneighboursname.Add((mya + 1).ToString() + "-" + myb.ToString() + "-" + myc.ToString());
            myneighboursname.Add(mya.ToString() + "-" + (myb - 1).ToString() + "-" + myc.ToString());
            myneighboursname.Add(mya.ToString() + "-" + (myb + 1).ToString() + "-" + myc.ToString());
            myneighboursname.Add(mya.ToString() + "-" + myb.ToString() + "-" + (myc - 1).ToString());
            myneighboursname.Add(mya.ToString() + "-" + myb.ToString() + "-" + (myc + 1).ToString());

            // check how many of my neighbours are taken
            //int takeneighbours = 0;

            foreach (string mnn in myneighboursname)
            {
                takeneighbours = taken.Contains(mnn) ? takeneighbours++ : takeneighbours;

            }
            // if more than two of my neighbours are taken, I shouldn't be able to move

            if(myname != "5-5-5")
            {
               myself.transform.GetComponent<BoxCollider>().enabled = takeneighbours >= 2 ? false : true;
            }

        }

    }
    */
    
    void OnSliderRotationChanged (float value)
    {
        changing = 1;

        // How much we've changed
        float delta = value - this.previousRotationValue;
        this.finalObject.transform.Rotate(Vector3.down * delta * 360);
        this.matrix.transform.Rotate(Vector3.down * delta * 360);
        
        // Set our previous value for the next change
        this.previousRotationValue = value;
        
    }

    void OnSliderPositionChanged(float value)
    {
        changing = 1;

        // How much we've changed
        float delta = value - this.previousPositionValue;
        this.finalObject.transform.position += new Vector3(0f, 0f, delta * 0.09f);
        this.matrix.transform.position += new Vector3(0f, 0f, delta * 0.09f);

        // Set our previous value for the next change
        this.previousPositionValue = value;

    }

}
