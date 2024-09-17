using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Examples.Demos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class TestTime : MonoBehaviour
{
    private float duringTime = 0;
    public TextMeshPro timeTMP;
    private bool? prevCalibrationStatus = null;
    private string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test23_4d4kmw1bzqv36\LocalState\fixationData.txt";
    //private string path = @"D:\test\test.txt";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
                    timeTMP.SetText("校准失败");
                }
                else
                {
                    TimeTextSet();
                   
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
        timeTMP.SetText("注视:" + duringTime.ToString() + "s");
       // RecordData(DateTime.Now, duringTime); // 记录数据到文本文件
    

    }

    private void RecordData(DateTime timestamp, float time)
    {

        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(timestamp.ToString("yyyy/MM/dd HH:mm:ss") + "," + time.ToString());
        }


    }

 




}
