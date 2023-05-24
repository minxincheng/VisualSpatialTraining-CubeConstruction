using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadWriteCSV;
using System.IO;
using UnityEngine.Networking;

public class DataHandler : MonoBehaviour
{
    
    List<ContinuousData> continuousDatas = new List<ContinuousData>();
    HeaderData headerData;
    
    private string pid;
    private string hand;
    
    public bool dataWritten = false;
    
    void OnDisable(){
        
        WriteContinuousFile();
        
    }
    
    public void WriteDataToFiles(){
        
        if(dataWritten){
            
            Debug.Log("Data already saved");
            return;
            
        }
        
        dataWritten = true;
        
        System.DateTime now = System.DateTime.Now;
        
        if(GlobalControl.Instance.handOption == 0){
            
            hand = "Right";
            
        }
        else{
            
            hand = "Left";
            
        }
        
        pid = GlobalControl.Instance.participantID + "_" + now.Month.ToString() + "_" + now.Day.ToString() + "_" + now.Hour + "_" + now.Minute;
        
        if(GlobalControl.Instance.recordingData){
            
            WriteContinuousFile();
            
        }
        
    }
    
    public void recordHeaderInfo(string hand, string targetobject){
        
        headerData = new HeaderData(hand, targetobject);
        
    }
    
    public void recordContinuous(float time, int submitting, int checking, int cubenumber, string dragging, string placing){
        
        continuousDatas.Add(new ContinuousData(time, submitting, checking, cubenumber, dragging, placing));
        
    }
    
    private void WriteHeaderInfo(CsvFileWriter writer){
        
        CsvRow hand = new CsvRow();
        hand.Add("Hand:");
        hand.Add(headerData.hand.ToString());
        
        CsvRow targetobject = new CsvRow();
        targetobject.Add("");
        targetobject.Add(headerData.targetobject.ToString());

        writer.WriteRow(hand);
        writer.WriteRow(targetobject);
        
    }
    
    private void WriteContinuousFile(){
        
        string directory = "Data/" + pid;
        Directory.CreateDirectory(@directory);
        
        using(CsvFileWriter writer = new CsvFileWriter(@directory + "/" + pid + "_Continuous.csv")){
            
            Debug.Log("Writing continuous data to file");
            
            WriteHeaderInfo(writer);
            
            CsvRow header = new CsvRow();
            header.Add("ID");
            header.Add("Timestamp");
            header.Add("isSubmitting");
            header.Add("isCheckingCube");
            header.Add("CubesInShape");
            header.Add("CurrentDragging");
            header.Add("CurrentPlacing");

            writer.WriteRow(header);
            
            // write each line of data
            foreach(ContinuousData c in continuousDatas){
                
                CsvRow row = new CsvRow();
                row.Add(GlobalControl.Instance.participantID);
                row.Add(c.time.ToString());
                row.Add(c.submitting.ToString());
                row.Add(c.checking.ToString());
                row.Add(c.cubenumber.ToString());
                row.Add(c.dragging.ToString());
                row.Add(c.placing.ToString());
                
                writer.WriteRow(row);

            }
            
            
        }
        
    }
}
