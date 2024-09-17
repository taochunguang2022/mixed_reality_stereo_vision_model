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
            SceneManager.LoadScene(1);//跳转请准备场景
        }
    }
    public void myPrepareScene()
    {
        duringTime += Time.deltaTime;
        if (duringTime >= 2.0f)
        {
            SceneManager.LoadScene(2);//跳转运动场景
        }
    }

    public void myRestScene()
    {
        duringTime += Time.deltaTime;
        if(duringTime >= 300.0f)
        {
            
            
            SceneManager.LoadScene(1);//跳转至请准备
           
        }

    }

    public void myMidScene()
    {
        duringTime += Time.deltaTime;
        if(duringTime >= 4.0f)
        {
            Application.Quit(); // 正式使用  
        }

    }

    public void End()
    {
        duringTime += Time.deltaTime;
        if(duringTime >= 1.0f)
        {
            // 程序强制退出
            //UnityEditor.EditorApplication.isPlaying = false; // 测试时使用
            Application.Quit(); // 正式使用   
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
