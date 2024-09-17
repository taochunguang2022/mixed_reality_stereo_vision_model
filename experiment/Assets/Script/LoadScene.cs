using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    float duringTime = 0;
    //private static string path = @"D:\test\EndtimeLog.txt";
    //private static string path = @"C:\Data\Users\liqi\AppData\Local\Packages\HoloLens2-MRTK-Getting-Started-Test23_4d4kmw1bzqv36\LocalState\EndtimeLog.txt";


    public void myStartScene()
    {
        
        duringTime += Time.deltaTime;
        if (duringTime >= 4.0f)
        {
            SceneManager.LoadScene(1);//��ת��׼������
        }
    }
    public void myPrepareScene()
    {
        duringTime += Time.deltaTime;
        if (duringTime >= 2.0f)
        {
            SceneManager.LoadScene(2);//��ת�˶�����
        }
    }

    public void myRestScene()
    {
        duringTime += Time.deltaTime;
        if(duringTime >= 300.0f)
        {
            
            
            SceneManager.LoadScene(1);//��ת����׼��
           
        }

    }

    public void myMidScene()
    {
        duringTime += Time.deltaTime;
        if(duringTime >= 4.0f)
        {
            Application.Quit(); // ��ʽʹ��  
        }

    }

    public void End()
    {
        duringTime += Time.deltaTime;
        if(duringTime >= 1.0f)
        {
            // ����ǿ���˳�
            //UnityEditor.EditorApplication.isPlaying = false; // ����ʱʹ��
            Application.Quit(); // ��ʽʹ��   
        }

    }

    public void EndScene()
    {
        duringTime += Time.deltaTime;
        if(duringTime >= 300.0f)
        {
            
            SceneManager.LoadScene(5);

        }
    }

  

   






}
