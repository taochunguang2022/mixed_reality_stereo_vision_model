using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TMPro;
using UnityEngine;
using SpeedRecordnamespace;
using UnityEngine.Networking;
using Microsoft.MixedReality.Toolkit;

public class TimeAdd : MonoBehaviour
{
    // Start is called before the first frame update
    private float duringTime = 0;
    public TextMeshPro timeTMP;
    private bool? prevCalibrationStatus = null;
    //private string dataFilePath = @"D:\Downloads\eye-tracking\fixationData.txt"; // �޸�Ϊ�µ�·��
    //string path = Path.Combine(Application.persistentDataPath + @"/FIXATION", "fixation.txt");
  private string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test23_4d4kmw1bzqv36\LocalState\fixationData.txt";
   // private string path = @"D:\test\fixationData.txt";
    private static int trail = 0;

   // private bool separatorAdded = false; // Add this variable
    
    private void Start()
    {
        trail++;
    }

    void Update()
    {
        // Get the latest calibration state from the EyeGazeProvider
        bool? calibrationStatus =
        CoreServices.InputSystem?.EyeGazeProvider?.IsEyeCalibrationValid;

        if (calibrationStatus != null)
        {
            if (prevCalibrationStatus != calibrationStatus)
            {
                if (calibrationStatus == false)
                {
                    timeTMP.SetText("У׼ʧ��");
                }
                else
                {
                   //TimeTextSet();

                   

                }
                prevCalibrationStatus = calibrationStatus;
            }
        }
        else
        {
            timeTMP.SetText("NULL");
        }
    }


   public void TimeTextSet()
    {
       
        duringTime += Time.deltaTime;
        //timeTMP.SetText("ע��:" + duringTime.ToString() + "s");
     if (IsValidPath(path))
      {
           RecordData(DateTime.Now, duringTime,trail); // ��¼���ݵ��ı��ļ�
      }
            
        
    }

   




    private void RecordData(DateTime timestamp, float time,int trail)
    {
        // ��ʱ�����ʱ�����ݼ�¼���ı��ļ�
        using (StreamWriter sw = new StreamWriter(path, true))
         {
        sw.WriteLine(timestamp.ToString("yyyy/MM/dd HH:mm:ss") + "," + time.ToString()+","+trail);
         }

       // using (StreamWriter sw = File.AppendText(path))
      // {
         //  sw.WriteLine(timestamp.ToString("yyyy/MM/dd HH:mm:ss") + "," + time.ToString());
       // }


    }

    private bool IsValidPath(string filePath)
    {
        try
        {
            // ���Դ����ļ���������ܴ����ɹ�����˵��·����Ч
            using (FileStream fs = File.Create(filePath))
            {
                fs.Close();
            }
            File.Delete(filePath); // ɾ�����Դ������ļ�
            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }
}





