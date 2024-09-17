using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PreDoing : MonoBehaviour
{
    // Start is called before the first frame update
    //private static string path = @"D:\test\timeLog.txt";
    private static string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test23_4d4kmw1bzqv36\LocalState\timeLog.txt";
    void Start()
    {
        if (IsValidPath(path))
        {
            
            DateTime timestamp = DateTime.Now;
            LogStartTime(timestamp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void LogStartTime(DateTime timestamp)
    {
        using (StreamWriter writer = new StreamWriter(path, true))
        {

            writer.WriteLine("�̼�ǰ���ɿ�ʼʱ�䣺" + timestamp.ToString());

            writer.WriteLine("------------");
        }
    }

    private static bool IsValidPath(string filePath)
    {
        try
        {
            // ����ļ��Ƿ���ڣ������Ǵ������ļ�
            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                // ����ļ������ڣ����Դ����ļ�������·���Ƿ���Ч
                using (FileStream fs = File.Create(filePath))
                {
                    fs.Close();
                }
                return true;
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
